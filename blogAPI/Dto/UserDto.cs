using System.ComponentModel.DataAnnotations;

namespace blogAPI.Dto
{
    public class UserDto
    {
        [Key]
        public Guid id { get; set; }
        public string fullName { get; set; }
        public DateOnly createTime { get; set; }
        public string email { get; set; }
        public DateOnly birthDate { get; set; }
        public string gender { get; set; }
        public string phoneNumber { get; set; }
    }
}
