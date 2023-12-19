using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<List<AuthorDto>> GetList()
        {
            return await _authorService.GetList();

        }
    }
}
