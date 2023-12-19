using AutoMapper;
using blogAPI.Data;
using blogAPI.Data.Entities;
using blogAPI.Data.Entities.Enum;
using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogAPI.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CommunityService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CommunityDto>> AllComunities()
        {
            var communitiesList = await _context.Communities
            .Select(community => new CommunityDto
            {
                Id = community.id,
                CreateTime = community.createTime,
                Name = community.name,
                Description = community.description,
                isClosed = community.isClosed,
                SubscribersCount = community.subscribersCount
            })
            .ToListAsync();
            return communitiesList;
        }

        public async Task<Guid> CreateCommunityPost(Guid userId, [FromQuery] Guid CommunityId, [FromBody] CommunityCreateDto request)
        {
            var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.id == userId);
            var tagsList = _context.Tags.Where(tag => request.tags.Contains(tag.id)).ToList();
            var post = new PostModel
            {
                createTime = DateTime.UtcNow,
                title = request.title,
                description = request.description,
                image = request.image,
                authorId = userId,
                addressId = request.addressId,
                authorName = user.fullName,
                readingTime = request.readingTime,
                communityName = _context.Communities.FirstOrDefault(community => community.id == CommunityId).name,
                communityId = CommunityId,
                tags = tagsList,
            };

            {
                await _context.Posts.AddAsync(post);
                foreach (var tag in request.tags)
                {
                    var postTag = new PostTags { postId = post.id, tagId = tag };
                    await _context.PostTags.AddAsync(postTag);
                }
                await _context.SaveChangesAsync();

                return post.id;
            }
        }

        public async Task<CommunityInfoDto> GetInfo(Guid communityId)
        {
            var adminList = _context.UserCommunityRoles
                .Where(userCommunityRole => userCommunityRole.CommunityId == communityId && userCommunityRole.Role == 0)
                .Select(admin => new UserDto
                {
                    id = admin.UserId,
                    fullName = _context.Users.FirstOrDefault(user => user.id == admin.UserId).fullName,
                    birthDate = _context.Users.FirstOrDefault(user => user.id == admin.UserId).birthDate,
                    gender = _context.Users.FirstOrDefault(user => user.id == admin.UserId).gender,
                    email = _context.Users.FirstOrDefault(user => user.id == admin.UserId).email,
                    phoneNumber = _context.Users.FirstOrDefault(user => user.id == admin.UserId).phoneNumber,
                })
                .ToList();
            var currentCommunity = await _context.Communities
            .Where(community => community.id == communityId)
            .Select(community => new CommunityInfoDto
            {
                id = community.id,
                createTime = community.createTime,
                name = community.name,
                description = community.description,
                isClosed = community.isClosed,
                subscribersCount = community.subscribersCount,
                administrators = adminList
            })
            .FirstOrDefaultAsync();
            if (currentCommunity == null)
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                    "Community not found"
                );
                throw ex;
            }

            return currentCommunity;
        }

        public async Task<PostListDto> GetPosts([FromQuery] GetCommunityPostsDto request)
        {
            var query = _context.Posts.AsQueryable();

            if (request.Tags != null && request.Tags.Any())
            {
                query = query.Where(post => post.tags.Any(tag => request.Tags.Contains(tag.id)));
            }

            if (request.Id != null)
            {
                query = query.Where(post => post.communityId == request.Id);
            }

            switch (request.Sorting)
            {
                case Sorting.CreateDesc:
                    query = query.OrderByDescending(d => d.createTime);
                    break;
                case Sorting.CreateAsc:
                    query = query.OrderBy(d => d.createTime);
                    break;
                case Sorting.LikeAsc:
                    query = query.OrderBy(d => d.likes);
                    break;
                case Sorting.LikeDesc:
                    query = query.OrderByDescending(d => d.likes);
                    break;
                default: break;
            }

            query = query.Skip((request.Page - 1) * request.ElemsOnPage)
                  .Take(request.ElemsOnPage);

            int start = (request.Page - 1) * request.ElemsOnPage;
            int end = Math.Min(request.ElemsOnPage, query.ToList().Count - start);
            double count = Double.Ceiling((double)query.ToList().Count / request.ElemsOnPage);
            var pagination = new PageInfo
            {
                Start = start,
                End = end,
                Count = count
            };
            var result = new PostListDto { Posts = query.ToList(), Pagination = pagination };

            return result;
        }

        public async Task<Role> GreatestRole(Guid userId, [FromQuery] Guid communityId)
        {
            var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.id == userId);

            var greatestRole = _context.UserCommunityRoles
            .Where(u => u.UserId == userId && u.CommunityId == communityId)
            .GroupBy(u => u.CommunityId)
            .Select(g => g.OrderByDescending(r => r.Role).First().Role).First();

            return greatestRole;
        }

        public async Task<List<UserCommunityRole>> RolesUserCommunities(Guid userId)
        {
            var user = await _context
           .Users
           .FirstOrDefaultAsync(x => x.id == userId);

            var userCommunities = _context.UserCommunityRoles
            .Where(u => u.UserId == userId)
            .GroupBy(u => u.CommunityId)
            .Select(g => g.OrderByDescending(r => r.Role).First())
            .ToList();

            var result = userCommunities.Select(u => new UserCommunityRole
            {
                UserId = userId,
                CommunityId = u.CommunityId,
                Role = u.Role,
            }).ToList();

            return result;
        }

        public async Task Subscribe(Guid userId, Guid communityId)
        {
            var user = await _context
           .Users
           .FirstOrDefaultAsync(x => x.id == userId);

            var UserCommunity = new UsersCommunitiesModel
            {
                UserId = userId,
                CommunityId = communityId
            };

            await _context.UsersSubscriptions.AddAsync(UserCommunity);
            await _context.SaveChangesAsync();

        }

        public async Task UnSubscribe(Guid userId, Guid communityId)
        {
            var user = await _context
           .Users
           .FirstOrDefaultAsync(x => x.id == userId);
            var UserCommunity = await _context.UsersSubscriptions.FirstOrDefaultAsync(userCommunity => userCommunity.UserId == userId && userCommunity.CommunityId == communityId);
            _context.UsersSubscriptions.Remove(UserCommunity);
            await _context.SaveChangesAsync();

        }
    }
}
