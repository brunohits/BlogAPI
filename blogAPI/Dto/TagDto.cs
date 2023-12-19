
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace blogAPI.Dto
{
    public class TagDto
    {
        [Key]
        public Guid id { get; set; }
        public DateOnly createTime { get; set; }
        public string name { get; set; }
    }
}
