using System.ComponentModel.DataAnnotations;

namespace m1project.Areas.Climbing.Models
{
    public class Route
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Grade { get; set; } // e.g. "6b+", "7a"

        [Required]
        public int SectorId { get; set; }

        public Sector Sector { get; set; }
    }

}
