using blogAPI.Controllers;
using blogAPI.Data.Entities.Enum;

namespace blogAPI.Dto
{
    public class GetCommunityPostsDto
    { 
        public Guid Id { get; set; }
        public List<Guid> Tags { get; set; }
        public Sorting Sorting { get; set; }
        public int Page { get; set; } = 1;
        public int ElemsOnPage { get; set; } = 5;
    }
}
