using blogAPI.Data.Entities.Enum;

namespace blogAPI.Dto
{
    public class UserCommunityRoleDto
    {
        public Guid CommunityId { get; set; }
        public Guid UserId { get; set; }
        public Role Role { get; set; }
    }
}
