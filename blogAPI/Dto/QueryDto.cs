using blogAPI.Controllers;
using blogAPI.Data.Entities.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace blogAPI.Dto
{
    public class QueryDto
    {
        public List<Guid>? Tags { get; set; }

        public string Author { get; set; } = string.Empty;

        public int? MinReadTime { get; set; }

        public int? MaxReadTime { get; set; }

        public Sorting Sorting { get; set; }

        public bool OnlyMyCommunities { get; set; }

        public int Page { get; set; } = 1;

        public int ElemsOnPage { get; set; } = 5;
    }
}
