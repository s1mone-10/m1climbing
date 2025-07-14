using m1project.Areas.Climbing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace m1project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<m1project.Areas.Climbing.Models.Crag> Crag { get; set; } = default!;
        public DbSet<m1project.Areas.Climbing.Models.Sector> Sector { get; set; } = default!;
        public DbSet<m1project.Areas.Climbing.Models.Route> Route { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Prevent cascade delete from Crag to Route (via direct FK)
            builder.Entity<Areas.Climbing.Models.Route>()
                .HasOne(r => r.Crag)
                .WithMany()
                .HasForeignKey(r => r.CragId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
