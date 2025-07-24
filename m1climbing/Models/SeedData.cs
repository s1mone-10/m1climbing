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
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            SeedCrags(context);

            // Define the admin user details
            var adminEmail = "admin@m1project.com";
            var adminPassword = "Admin@123";

            var adminID = await EnsureUser(serviceProvider, adminPassword, adminEmail, Roles.Admin);

            // Define the user details
            var climberEmail = "climber1@m1project.com";
            var climberPassword = "Climber1@123";

            // allowed user can create and edit contacts that they create
            var managerID = await EnsureUser(serviceProvider, climberPassword, climberEmail, Roles.Climber);
        }
    }

    private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string email, string role)
    {
        var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, testUserPw);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception("Failed to create the climber user: " + string.Join(", ", result.Errors));
            }
        }

        if (user == null)
        {
            throw new Exception("The password is probably not strong enough!");
        }

        return user.Id;
    }

    private static void SeedCrags(ApplicationDbContext context)
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

        return;
    }
}
