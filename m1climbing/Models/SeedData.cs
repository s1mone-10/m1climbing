using m1climbing.Areas.Climbing.Models;
using m1climbing.Areas.Identity.Data;
using m1climbing.Constants;
using m1climbing.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;
using Route = m1climbing.Areas.Climbing.Models.Route;

namespace m1climbing.Models;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        SeedCrags(serviceProvider);
        
        // TODO: I can't login with users created like this
        //using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        //// Define the admin user details
        //var adminEmail = "admin@gmail.com";
        //var adminPassword = "Admin@123";

        //// Check if the admin user already exists
        //var userExist = await userManager.FindByEmailAsync(adminEmail);
        //if (userExist == null)
        //{
        //    var adminUser = new ApplicationUser
        //    {
        //        UserName = "admin",
        //        Email = adminEmail,
        //        EmailConfirmed = true
        //    };

        //    // Create the admin user
        //    var result = await userManager.CreateAsync(adminUser, adminPassword);
        //    if (result.Succeeded)
        //    {
        //        // Assign the Admin role to the user
        //        await userManager.AddToRoleAsync(adminUser, Roles.Admin);
        //    }
        //    else
        //    {
        //        throw new Exception("Failed to create the admin user: " + string.Join(", ", result.Errors));
        //    }
        //}

        //// Define the admin user details
        //var climberEmail = "climber1@gmail.com";
        //var climberPassword = "Climber1@123";

        //// Check if the admin user already exists
        //userExist = await userManager.FindByEmailAsync(climberEmail);
        //if (userExist == null)
        //{
        //    var climberUser = new ApplicationUser
        //    {
        //        UserName = "climber1",
        //        Email = climberEmail,
        //        EmailConfirmed = true
        //    };

        //    // Create the admin user
        //    var result = await userManager.CreateAsync(climberUser, climberPassword);
        //    if (result.Succeeded)
        //    {
        //        // Assign the Admin role to the user
        //        await userManager.AddToRoleAsync(climberUser, Roles.Climber);
        //    }
        //    else
        //    {
        //        throw new Exception("Failed to create the climber user: " + string.Join(", ", result.Errors));
        //    }
        //}
    }

    private static void SeedCrags(IServiceProvider serviceProvider)
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

        return;
    }
}
