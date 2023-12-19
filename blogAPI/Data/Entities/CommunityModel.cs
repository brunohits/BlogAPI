using blogAPI.Dto;

namespace blogAPI.Data.Entities
{
    public class CommunityModel
    {
        public Guid id { get; set; }
        public DateTime createTime { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isClosed { get; set; }
        public int subscribersCount { get; set; }
        public List<UserDto> administrators { get; set; }

    }
}
