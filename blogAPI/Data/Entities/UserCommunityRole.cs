using blogAPI.Data.Entities.Enum;
using System.Text.Json.Serialization;

namespace blogAPI.Data.Entities
{
    public class UserCommunityRole
    {
        public Guid CommunityId { get; set; }
        public Guid UserId { get; set; }
        public Role Role { get; set; }
    }

}
