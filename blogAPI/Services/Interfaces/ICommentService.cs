using blogAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetReplies(Guid commentId);
        Task<Guid> Comment(Guid userId, [FromQuery] Guid postId, [FromBody] PostCommentDto request);
        Task<string> EditComment(Guid userId, [FromQuery] Guid commentId, [FromBody] string newContent);
        Task<string> DeleteComment(Guid userId, [FromQuery] Guid commentId);
    }
}
