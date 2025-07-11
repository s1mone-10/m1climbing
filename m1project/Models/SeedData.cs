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
                Region = "South Valley",
                Sectors = new List<Sector>
                {
                    new Sector
                    {
                        Name = "Main Wall",
                        Routes = new List<Route>
                        {
                            new Route { Name = "Sunshine", Grade = "6a" },
                            new Route { Name = "Heatwave", Grade = "6b+" }
                        }
                    },
                    new Sector
                    {
                        Name = "Shady Side",
                        Routes = new List<Route>
                        {
                            new Route { Name = "Cool Breeze", Grade = "5c" }
                        }
                    }
                }
            };

            var crag2 = new Crag
            {
                Name = "Montovolo",
                Region = "Bologna",
                Sectors = new List<Sector>
                {
                    new Sector
                    {
                        Name = "Centrale",
                        Routes = new List<Route>
                        {
                            new Route { Name = "Passenger", Grade = "7a" },
                            new Route { Name = "WhatsApp", Grade = "6c+" }
                        }
                    },
                    new Sector
                    {
                        Name = "Cavina",
                        Routes = new List<Route>
                        {
                            new Route { Name = "Brutta", Grade = "5c" }
                        }
                    }
                }
            };

            var crag3 = new Crag
            {
                Name = "Granite Peak",
                Region = "North Ridge",
                Sectors = new List<Sector>
                {
                    new Sector
                    {
                        Name = "Eagle's Nest",
                        Routes = new List<Route>
                        {
                            new Route { Name = "Skyline", Grade = "7b" },
                            new Route { Name = "Hawk's Flight", Grade = "6c" }
                        }
                    },
                    new Sector
                    {
                        Name = "Boulder Field",
                        Routes = new List<Route>
                        {
                            new Route { Name = "Stone Dance", Grade = "6a+" }
                        }
                    }
                }
            };

            var crag4 = new Crag
            {
                Name = "Blue Lake",
                Region = "East Valley",
                Sectors = new List<Sector>
                {
                    new Sector
                    {
                        Name = "Waterfall",
                        Routes = new List<Route>
                        {
                            new Route { Name = "Cascade", Grade = "6b" },
                            new Route { Name = "Mist Trail", Grade = "6a" }
                        }
                    },
                    new Sector
                    {
                        Name = "Pine Forest",
                        Routes = new List<Route>
                        {
                            new Route { Name = "Needle Path", Grade = "5b" }
                        }
                    }
                }
            };

            context.Crag.AddRange(crag, crag2, crag3, crag4);
            context.SaveChanges();
        }
    }
}
