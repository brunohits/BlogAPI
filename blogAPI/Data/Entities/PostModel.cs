using blogAPI.Dto;
using System.ComponentModel.DataAnnotations;

namespace blogAPI.Data.Entities
{
    public class PostModel
    {
        [Key]
        public Guid id { get; set; }
        public DateTime createTime { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int readingTime { get; set; }
        public string image { get; set; }
        public Guid authorId { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string authorName { get; set; }
        public Guid communityId { get; set; }
        public string communityName { get; set; }
        public Guid addressId { get; set; }
        public int likes { get; set; }
        public bool hasLikes { get; set; }
        public int commentsCount { get; set; }
        public List<TagModel> tags { get; set; }
        public List<CommentModel> comments { get; set; }

    }
}
