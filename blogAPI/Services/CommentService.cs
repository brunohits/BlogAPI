using AutoMapper;
using blogAPI.Data;
using blogAPI.Data.Entities;
using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace blogAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        private readonly JWTSettings _options;
        private readonly IMapper _mapper;
        public CommentService(AppDbContext context, IOptions<JWTSettings> optAccess, IMapper mapper)
        {
            _context = context;
            _options = optAccess.Value;
            _mapper = mapper;
        }
        public async Task<Guid> Comment(Guid userId,[FromQuery] Guid postId, [FromBody] PostCommentDto request)
        {
            var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.id == userId);
            var parentComment = await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == request.parentId);
            if (request == null)
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                    "Parent object is not found"
                );
                throw ex;
            }

            var comment = new CommentModel
            {
                createDate = DateTime.UtcNow,
                content = request.content,
                modifiedDate = DateTime.UtcNow,
                authorId = userId,
                postId = postId,

            };
            if (request.parentId != null)
            {
                comment.parentCommentId = parentComment.Id;
                parentComment.subComments++;
            }
            await _context.Comments.AddAsync(comment);

            await _context.SaveChangesAsync();
            return comment.Id;
        }

        public async Task<string> DeleteComment(Guid userId, [FromQuery] Guid commentId)
        {
            var user = await _context
          .Users
          .FirstOrDefaultAsync(x => x.id == userId);
            var currentComment = _context.Comments.FirstOrDefault(comment => comment.Id == commentId);

            if (userId != currentComment.authorId)
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                    "Unauthorized"
                );
            }
            else { _context.Comments.Remove(currentComment); }
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK.ToString();
        }
    

        public async Task<string> EditComment(Guid userId, [FromQuery] Guid commentId, [FromBody] string newContent)
        {
            var user = await _context
           .Users
           .FirstOrDefaultAsync(x => x.id == userId);
            var currentComment = _context.Comments.FirstOrDefault(comment => comment.Id == commentId);
            if (userId != currentComment.authorId)
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                    "Unauthorized"
                );
            }
            else { currentComment.content = newContent; }
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK.ToString();
        }

        public async Task<List<CommentDto>> GetReplies(Guid commentId)
        {
            var parentComment = await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == commentId);

            if (parentComment == null)
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                    "Parent comment not found"
                );
            }
            var replies = await _context.Comments
                .Where(comment => comment.parentCommentId == commentId)
                .ToListAsync();
            var replyDtos = _mapper.Map<List<CommentDto>>(replies);
            return replyDtos;
        }
    }
}
