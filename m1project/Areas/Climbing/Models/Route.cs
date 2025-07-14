using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace m1project.Areas.Climbing.Models
{
    [Index(nameof(Name), nameof(CragId), IsUnique = true)]
    public class Route
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public required string Grade { get; set; } // e.g. "6b+", "7a"

        [Required]
        public int SectorId { get; set; }

        public Sector? Sector { get; set; }

        [Required]
        public int CragId { get; set; }

        public Crag? Crag { get; set; }
    }
}
