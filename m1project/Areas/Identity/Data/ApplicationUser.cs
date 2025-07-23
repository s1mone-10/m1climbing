using m1project.Areas.Climbing.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace m1project.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public ICollection<UserCompletedRoute> CompletedRoutes { get; set; } = new List<UserCompletedRoute>();
    }
}