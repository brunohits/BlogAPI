using System.ComponentModel.DataAnnotations;

namespace blogAPI.Data.Entities
{
    public class UserModel
    {
        [Key]
        public Guid id { get; set; }
        public string fullName { get; set; }
        public string password { get; set; }
        public DateOnly createTime { get; set; }
        public string email { get; set; }
        public DateOnly birthDate { get; set; }
        public string gender { get; set; }
        public string phoneNumber { get; set; }
    }
}
