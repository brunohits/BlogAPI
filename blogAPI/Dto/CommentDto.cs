using System.ComponentModel.DataAnnotations;

namespace blogAPI.Dto
{
    public class CommentDto
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime createDate { get; set; }
        public string content { get; set; }
        public DateTime modifiedDate { get; set; }
        public DateTime deleteDate { get; set; }
        public Guid authorId { get; set; }
        public string authorName { get; set; }
        public int subComments { get; set; }

        
    }
}
