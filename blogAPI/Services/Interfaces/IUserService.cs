using blogAPI.Dto;
using System.Security.Claims;

namespace blogAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<TokenDto> Register( UserRegistrationDto request);
        Task<TokenDto> Login(UserLoginDto request);
        Task Logout(string token);
        Task<UserDto> GetProfile(Guid userId);
        Task EditProfile(Guid userId, EditProfileDto request);
    }
}
