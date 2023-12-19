namespace blogAPI.Data.Entities
{
    public class ParentnessTreeModel
    {
        public Guid commentId { get; set; }
        public Guid parentCommentId { get; set; }
    }
}
