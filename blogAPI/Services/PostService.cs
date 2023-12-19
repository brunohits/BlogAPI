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
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PostModel> GetInfo(Guid PostId)
        {
            var currentPost = await _context.Posts
            .Where(post => post.id == PostId)
            .Select(post => new PostModel
            {
           id = post.id,
           createTime = post.createTime,
           title = post.title,
           description = post.description,
           readingTime = post.readingTime,
           image = post.image,
           authorId = post.authorId,
           authorName = post.authorName,
           communityId = post.communityId,
           communityName = post.communityName,
           addressId = post.addressId,
           likes = post.likes,
           hasLikes = post.hasLikes,
           commentsCount = post.commentsCount,
           tags = post.tags,
           comments = post.comments,
       })
       .FirstOrDefaultAsync();
            return currentPost;
        }

        public async Task<PostListDto> GetPosts([FromQuery] QueryDto request)
        {
            var query = _context.Posts.AsQueryable();

            if (request.Tags != null && request.Tags.Any())
            {
                query = query.Where(post => post.tags.Any(tag => request.Tags.Contains(tag.id)));
            }

            if (!string.IsNullOrEmpty(request.Author))
            {
                query = query.Where(post => post.authorName == request.Author);

                query = query.OrderBy(post => post.authorName);
            }
            else { }

            if (request.MinReadTime > 0)
            {
                query = query.Where(post => post.readingTime >= request.MinReadTime);
            }

            if (request.MaxReadTime > 0)
            {
                query = query.Where(post => post.readingTime <= request.MaxReadTime);
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

        public async Task Like(Guid userId, Guid postId)
        {
            var user = await _context
           .Users
           .FirstOrDefaultAsync(x => x.id == userId);

            var UserLike = new UserPostLikes
            {
                UserId = user.id,
                PostId = postId,
                Like = true
            };

            await _context.Likes.AddAsync(UserLike);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Guid> Post(Guid userId,[FromBody] CreatePostDto request)
        {
            var user = await _context
           .Users
           .FirstOrDefaultAsync(x => x.id == userId);
            var post = new PostModel
            {
                createTime = DateTime.UtcNow,
                title = request.title,
                description = request.description,
                image = request.image,
                authorId = userId,
                addressId = request.addressId,
                authorName = user.fullName,
                readingTime = 0,
                communityName = "",
                tags = request.tags,
            };

            await _context.Posts.AddAsync(post);
            foreach (var tag in request.tags)
            {
                var postTag = new PostTags { postId = post.id, tagId = tag.id };
                await _context.PostTags.AddAsync(postTag);
            }
            await _context.SaveChangesAsync();
            return (post.id);
        }

        public async Task UnLike(Guid userId,Guid postId)
        {
            var user = await _context
           .Users
           .FirstOrDefaultAsync(x => x.id == userId);
            var userPostRelation = _context.Likes.SingleOrDefault(upl => upl.UserId == user.id && upl.PostId == postId);
            userPostRelation.Like = false;

            await _context.SaveChangesAsync();
        }
    }
}
