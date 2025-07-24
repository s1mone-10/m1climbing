using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace m1climbing.Areas.Climbing.Models
{
    [Index(nameof(Name), nameof(CragId), IsUnique = true)]
    public class Sector
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        public int CragId { get; set; }

        public Crag? Crag { get; set; }

        public ICollection<Route> Routes { get; set; } = new List<Route>();
    }

}
