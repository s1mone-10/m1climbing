using System.ComponentModel.DataAnnotations;

namespace m1project.Areas.Climbing.Models
{
    public class Crag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? Region { get; set; }

        public ICollection<Sector> Sectors { get; set; } = new List<Sector>();
    }
}
