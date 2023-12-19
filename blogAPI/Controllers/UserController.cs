using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace App.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService usersService)
        {
            _userService = usersService;
        }

        /// <response code="201">Success</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<TokenDto> Register([FromBody] UserRegistrationDto request)
        {
            return await _userService.Register(request);
        }

        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        public async Task<TokenDto> Login([FromBody] UserLoginDto request)
        {
            return await _userService.Login(request);
        }

        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task Logout()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new Exception("Token not found");
            }
            await _userService.Logout(token);
        }

        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Authorize]
        public async Task<UserDto> GetProfile()
        {
            return await _userService.GetProfile(Guid.Parse(User.Identity.Name));
        }

        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut]
        [Authorize]
        public async Task EditProfile([FromBody] EditProfileDto request)
        {
            await _userService.EditProfile(Guid.Parse(User.Identity.Name), request);
        }

    }

}
