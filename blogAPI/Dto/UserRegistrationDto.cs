using System.ComponentModel.DataAnnotations;

namespace blogAPI.Dto
{
    public class UserRegistrationDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsValid()
        {
            return FullName != null &&
                Password != null &&
                Email != null &&
                BirthDate != null &&
                Gender != null &&
                PhoneNumber != null;
        }
    }
}
