using Microsoft.AspNetCore.Mvc;
using blogAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using blogAPI.Services.Interfaces;
using blogAPI.Data.Entities;
using blogAPI.Data.Entities.Enum;

namespace blogAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _communityService;

        public CommunityController(ICommunityService communityService)
        {
            _communityService = communityService;
        }


        [Authorize]
        [HttpGet]
        public async Task<List<CommunityDto>> AllComunities()
        {
           return await _communityService.AllComunities();
        }


        [Authorize]
        [HttpGet]
        public async Task<CommunityInfoDto> GetInfo(Guid communityId)
        {
            return await _communityService.GetInfo(communityId);
        }


        
        [Authorize]
        [HttpGet]
        public async Task<List<UserCommunityRole>> RolesUserCommunities()
        {
            return await _communityService.RolesUserCommunities(Guid.Parse(User.Identity.Name));
        }

        [Authorize]
        [HttpGet]
        public async Task<PostListDto> GetPosts([FromQuery] GetCommunityPostsDto request)
        {
            return await _communityService.GetPosts(request);
        }


        [Authorize]
        [HttpGet]
        public async Task<Role> GreatestRole([FromQuery] Guid communityId)
        {
            return await _communityService.GreatestRole(Guid.Parse(User.Identity.Name), communityId);
        }


        [Authorize]
        [HttpPost]
        public async Task Subscribe(Guid communityId)
        {
            await _communityService.Subscribe(Guid.Parse(User.Identity.Name), communityId);
        }

        [Authorize]
        [HttpDelete]
        public async Task UnSubscribe(Guid communityId)
        {
            await _communityService.UnSubscribe(Guid.Parse(User.Identity.Name), communityId);
        }


        [Authorize]
        [HttpPost]
        public async Task<Guid> CreateCommunityPost([FromQuery] Guid CommunityId, [FromBody] CommunityCreateDto request)
        {
            return await _communityService.CreateCommunityPost(Guid.Parse(User.Identity.Name), CommunityId, request);
        }

    }
}
