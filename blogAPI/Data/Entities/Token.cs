using System.ComponentModel.DataAnnotations;

namespace blogAPI.Data.Entities
{
    public class Token
    {
        [Required]
        public string InvalidToken { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
    }
}
