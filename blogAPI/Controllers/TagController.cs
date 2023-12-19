using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace blogAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<List<TagDto>> GetTags()
        {
           return await _tagService.GetTags();
                
        }
    }
}
