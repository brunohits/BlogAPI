namespace blogAPI.Dto
{
    public class CommunityCreateDto
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int readingTime { get; set; }
        public string image {  get; set; }
        public Guid addressId { get; set; }
        public List<Guid> tags { get; set; }
    }
}
