using blogAPI.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace blogAPI.Dto
{
    public class PostListDto
    {
        [Required]
        public List<PostModel> Posts { get; set; }
        [Required]
        public PageInfo Pagination { get; set; }

    }
}
