using System.ComponentModel.DataAnnotations;

namespace m1project.Areas.Climbing.Models
{
    public class Sector
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int CragId { get; set; }

        public Crag Crag { get; set; }

        public ICollection<Route> Routes { get; set; } = new List<Route>();
    }

}
