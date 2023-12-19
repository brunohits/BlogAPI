using blogAPI.Dto;

namespace blogAPI.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDto>> GetTags();
    }
}
