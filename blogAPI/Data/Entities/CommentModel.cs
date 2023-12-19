using System.ComponentModel.DataAnnotations;

namespace blogAPI.Data.Entities
{
    public class CommentModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime createDate { get; set; }
        public string content { get; set; }
        public DateTime modifiedDate { get; set; }
        public DateTime deleteDate { get; set; }
        public Guid authorId { get; set; }
        public int subComments { get; set; }
        public Guid parentCommentId { get; set; }
        public Guid postId { get; set; }
    }
}