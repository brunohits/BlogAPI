using System.ComponentModel.DataAnnotations;

namespace blogAPI.Data.Entities
{
    public class UserPostLikes
    {
        [Key]
        public Guid UserId { get; set; }
        [Key]
        public Guid PostId { get; set; }
        public bool Like { get; set; }

    }
}
