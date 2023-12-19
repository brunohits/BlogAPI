using blogAPI.Data.Entities;

namespace blogAPI.Dto
{
    public class CreatePostDto
    {
        public string title { get; set; }
        public string description { get; set; }
        public int readingTime { get; set; }
        public string image { get; set; }
        public Guid addressId { get; set; }
        public List<TagModel> tags { get; set; }
    }

}
