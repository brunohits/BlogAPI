using blogAPI.Data.Entities;
using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace blogAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
       
        public PostController(IPostService postService)
        {
            
            _postService = postService;
        }

        [HttpGet]
        public async Task<PostListDto> GetPosts([FromQuery] QueryDto request)
        {
            return await _postService.GetPosts(request);
        }


        [Authorize]
        [HttpPost]
        public async Task<Guid> Post([FromBody] CreatePostDto request)
        {
            return await _postService.Post(Guid.Parse(User.Identity.Name), request);
        }

        [Authorize]
        [HttpGet]
        public async Task<PostModel> GetInfo(Guid PostId)
        {
          return await _postService.GetInfo(PostId);
        }


        [Authorize]
        [HttpPost]
        public async Task Like(Guid postId)
        {
            await _postService.Like(Guid.Parse(User.Identity.Name), postId);
        }

        [Authorize]
        [HttpDelete]
        public async Task UnLike(Guid postId)
        {
            await _postService.UnLike(Guid.Parse(User.Identity.Name), postId);
        }

    }
}
