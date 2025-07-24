using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using m1climbing.Areas.Identity.Data;

namespace m1climbing.Areas.Climbing.Models
{
    public class UserCompletedRoute
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;

        [Required]
        public int RouteId { get; set; }
        public Route Route { get; set; } = default!;

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }

        [MaxLength(10)]
        public string? ProposedGrade { get; set; } // e.g. "6b+", "7a"

        [MaxLength(500)]
        public string? PrivateNote { get; set; }
    }
}