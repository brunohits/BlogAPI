using System.ComponentModel.DataAnnotations;

namespace blogAPI.Data.Entities
{
    public class AddressElementModel
    {
        [Required]
        public long? Id { get; set; }
        [Required]
        public Guid? ObjectGuid { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public string? Level { get; set; }
    }
}