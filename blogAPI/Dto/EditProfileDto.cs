using System.ComponentModel.DataAnnotations;

namespace blogAPI.Dto
{
    public class EditProfileDto
    {
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public string FullName { get; set; }
        public DateOnly BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }
        
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
