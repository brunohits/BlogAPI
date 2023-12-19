namespace blogAPI.Dto
{
    public class PostCommentDto
    {
        public string content { get; set; }
        public Guid? parentId { get; set; }
    }
}
