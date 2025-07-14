using Microsoft.EntityFrameworkCore;
using m1project.Data;
using m1project.Areas.Climbing.Models;
using Route = m1project.Areas.Climbing.Models.Route;

namespace m1project.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
        {
            // Look for any crags.
            if (context.Crag.Any())
            {
                return;   // DB has been seeded
            }

            var crag = new Crag
            {
                Name = "Sunny Rock",
                Region = "South Valley"
            };
            crag.Sectors = new List<Sector>
            {
                new Sector
                {
                    Name = "Main Wall",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Sunshine", Grade = "6a", Crag = crag },
                        new Route { Name = "Heatwave", Grade = "6b+", Crag = crag }
                    }
                },
                new Sector
                {
                    Name = "Shady Side",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Cool Breeze", Grade = "5c", Crag = crag }
                    }
                }
            };

            var crag2 = new Crag
            {
                Name = "Montovolo",
                Region = "Bologna"
            };
            crag2.Sectors = new List<Sector>
            {
                new Sector
                {
                    Name = "Centrale",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Passenger", Grade = "7a", Crag = crag2 },
                        new Route { Name = "WhatsApp", Grade = "6c+", Crag = crag2 }
                    }
                },
                new Sector
                {
                    Name = "Cavina",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Brutta", Grade = "5c", Crag = crag2 }
                    }
                }
            };

            var crag3 = new Crag
            {
                Name = "Granite Peak",
                Region = "North Ridge"
            };
            crag3.Sectors = new List<Sector>
            {
                new Sector
                {
                    Name = "Eagle's Nest",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Skyline", Grade = "7b", Crag = crag3 },
                        new Route { Name = "Hawk's Flight", Grade = "6c", Crag = crag3 }
                    }
                },
                new Sector
                {
                    Name = "Boulder Field",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Stone Dance", Grade = "6a+", Crag = crag3 }
                    }
                }
            };

            var crag4 = new Crag
            {
                Name = "Blue Lake",
                Region = "East Valley"
            };
            crag4.Sectors = new List<Sector>
            {
                new Sector
                {
                    Name = "Waterfall",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Cascade", Grade = "6b", Crag = crag4 },
                        new Route { Name = "Mist Trail", Grade = "6a", Crag = crag4 }
                    }
                },
                new Sector
                {
                    Name = "Pine Forest",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Needle Path", Grade = "5b", Crag = crag4 }
                    }
                }
            };

            context.Crag.AddRange(crag, crag2, crag3, crag4);
            context.SaveChanges();
        }
    }
}
