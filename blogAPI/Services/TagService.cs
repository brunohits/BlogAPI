using AutoMapper;
using blogAPI.Data;
using blogAPI.Dto;
using blogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace blogAPI.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TagService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TagDto>> GetTags()
        {
            var tags = await _context.Tags.ToListAsync();
            var tagDtos = _mapper.Map<List<TagDto>>(tags);

            return tagDtos;
        }
    }
}
