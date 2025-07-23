using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace m1climbing.Areas.Climbing.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Crag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public string? Region { get; set; }

        public ICollection<Sector> Sectors { get; set; } = new List<Sector>();
    }
}
