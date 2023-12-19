using System.Security.Cryptography.X509Certificates;

namespace blogAPI.Data.Entities
{
    public class PostTags
    {
        public Guid postId { get; set; }
        public Guid tagId { get; set; }
    }
}
