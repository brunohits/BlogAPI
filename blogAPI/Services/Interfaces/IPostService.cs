using blogAPI.Data.Entities;
using blogAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostListDto> GetPosts([FromQuery] QueryDto request);
        Task<Guid> Post(Guid userId, [FromBody] CreatePostDto request);
        Task<PostModel> GetInfo(Guid PostId);
        Task Like(Guid userId,Guid postId);
        Task UnLike(Guid userId, Guid postId);
    }
}
