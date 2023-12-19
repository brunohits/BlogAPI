using System.ComponentModel.DataAnnotations;

namespace blogAPI.Data.Entities
{
    public class TagModel
    {
        [Key]
        public Guid id { get; set; }
        public DateOnly createTime { get; set; }
        public string name { get; set; }
    }
}
