using blogAPI.Dto;

namespace blogAPI.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetList(); 
    }
}
