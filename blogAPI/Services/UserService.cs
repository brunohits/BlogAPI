using AutoMapper;
using blogAPI.Data;
using blogAPI.Data.Entities;
using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace blogAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly JWTSettings _options;
        private readonly IMapper _mapper;
        public UserService(AppDbContext context, IOptions<JWTSettings> optAccess, IMapper mapper)
        {
            _context = context;
            _options = optAccess.Value;
            _mapper = mapper;
        }

        public async Task<TokenDto> Register(UserRegistrationDto request)
        {

            await UniqueCheck(request);

            byte[] salt;
            RandomNumberGenerator.Create().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(request.Password, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            var hashPassword = Convert.ToBase64String(hashBytes);

            var user = new UserModel
            {
                fullName = request.FullName,
                password = hashPassword,
                email = request.Email,
                birthDate = request.BirthDate,
                gender = request.Gender,
                phoneNumber = request.PhoneNumber,
                createTime = DateOnly.FromDateTime(DateTime.Today)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var loginModel = new UserLoginDto
            {
                Email = request.Email,
                Password = request.Password
            };

            return await Login(loginModel);
        }

        public async Task<TokenDto> Login(UserLoginDto request)
        {
            var claimsIdentity = await GetIdentity(request.Email, request.Password);
            var token = GenerateJwtToken(claimsIdentity);
            var result = new TokenDto()
            {
                Token = token
            };
            return result;
        }


        public async Task Logout(string token)
        {
            var alreadyExistsToken = await _context.Tokens.FirstOrDefaultAsync(x => x.InvalidToken == token);

            if (alreadyExistsToken == null)
            {
                var handler = new JwtSecurityTokenHandler();
                var expiredDate = handler.ReadJwtToken(token).ValidTo;
                _context.Tokens.Add(new Data.Entities.Token
                {
                    InvalidToken = token,
                    ExpiredDate = expiredDate
                });
                await _context.SaveChangesAsync();
            }
            else
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                    "Token is already invalid"
                );
                throw ex;
            }

        }

        public async Task<UserDto> GetProfile(Guid userId)
        {
            var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.id == userId);

            if (user != null)
                return _mapper.Map<UserDto>(user);

            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                "User not exists"
            );
            throw ex;
        }

        public async Task EditProfile(Guid userId, EditProfileDto request)
        {
            var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.id == userId);

            if (user == null)
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                    "User not exists"
                );
                throw ex;
            }
            user.email = request.Email;
            user.fullName = request.FullName;
            user.birthDate = request.BirthDate;
            user.gender = request.Gender;
            user.phoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync();
        }

        private string GenerateJwtToken(ClaimsIdentity claimsIdentity)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claimsIdentity.Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(x => x.email == email);

            if (userEntity == null)
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                    "User not exists"
                );
                throw ex;
            }

            if (!CheckHashPassword(userEntity.password, password))
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                    "Wrong password"
                );
                throw ex;
            }

            var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, userEntity.id.ToString())
        };

            var claimsIdentity = new ClaimsIdentity
            (
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            return claimsIdentity;
        }

        private static bool CheckHashPassword(string savedPasswordHash, string password)
        {
            var hashBytes = Convert.FromBase64String(savedPasswordHash);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            for (var i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }
        private async Task UniqueCheck(UserRegistrationDto userRegisterModel)
        {
            var email = await _context
                .Users
                .Where(x => userRegisterModel.Email == x.email)
                .FirstOrDefaultAsync();

            if (email != null)
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status409Conflict.ToString(),
                    $"Account with email '{userRegisterModel.Email}' already exists"
                );
                throw ex;
            }
        }
    }
}
