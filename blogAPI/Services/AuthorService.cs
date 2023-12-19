using blogAPI.Data;
using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace blogAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;    
        }

        public async Task<List<AuthorDto>> GetList()
        {
            var authors = await _context.Users

                .Select(user => new AuthorDto
                {
                    fullName = user.fullName,
                    birthDate = user.birthDate,
                    gender = user.gender,
                    posts = _context.Posts.Count(p => p.authorId == user.id),
                    likes = _context.Likes.Count(l =>
                           l.Like &&
                           l.PostId == _context.Posts
                               .Where(p => p.authorId == user.id)
                               .Select(p => p.id)
                               .FirstOrDefault()),
                created = user.createTime
            })
            .ToListAsync();
            return authors;
        }
    }
}
