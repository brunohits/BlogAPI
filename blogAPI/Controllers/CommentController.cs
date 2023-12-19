using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
       
        [Authorize]
        [HttpGet]
        public async Task<List<CommentDto>> GetReplies(Guid commentId)
        {
            return await _commentService.GetReplies(commentId);
        }

        [Authorize]
        [HttpPost]
        public async Task<Guid> Comment([FromQuery] Guid postId, [FromBody] PostCommentDto request)
        {
            return await _commentService.Comment(Guid.Parse(User.Identity.Name), postId, request);
        }

        [Authorize]
        [HttpPut]
        public async Task<string> EditComment([FromQuery] Guid commentId, [FromBody] string newContent)
        {
            return await _commentService.EditComment(Guid.Parse(User.Identity.Name), commentId, newContent);
        }

        [Authorize]
        [HttpDelete]
        public async Task<string> DeleteComment([FromQuery] Guid commentId)
        {
            return await _commentService.DeleteComment(Guid.Parse(User.Identity.Name), commentId);
        }

    }
}

