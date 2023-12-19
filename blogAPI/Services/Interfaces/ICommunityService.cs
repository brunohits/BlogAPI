using blogAPI.Data.Entities;
using blogAPI.Data.Entities.Enum;
using blogAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Services.Interfaces
{
    public interface ICommunityService
    {
        Task<List<CommunityDto>> AllComunities();
        Task<CommunityInfoDto> GetInfo(Guid communityId);
        Task<List<UserCommunityRole>> RolesUserCommunities(Guid userId);
        Task<PostListDto> GetPosts([FromQuery] GetCommunityPostsDto request);
        Task<Role> GreatestRole(Guid userId, [FromQuery] Guid communityId);
        Task Subscribe(Guid userId, Guid communityId);
        Task UnSubscribe(Guid userId, Guid communityId);
        Task<Guid> CreateCommunityPost(Guid userId, [FromQuery] Guid CommunityId, [FromBody] CommunityCreateDto request);
    }
}
