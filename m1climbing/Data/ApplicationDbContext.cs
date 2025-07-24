using m1climbing.Areas.Climbing.Models;
using m1climbing.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static m1climbing.Areas.Identity.Data.ApplicationUser;

namespace m1climbing.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<m1climbing.Areas.Climbing.Models.Crag> Crag { get; set; } = default!;
        public DbSet<m1climbing.Areas.Climbing.Models.Sector> Sector { get; set; } = default!;
        public DbSet<m1climbing.Areas.Climbing.Models.Route> Route { get; set; } = default!;
        public DbSet<m1climbing.Areas.Climbing.Models.UserCompletedRoute> UserCompletedRoutes { get; set; } = default!;

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
