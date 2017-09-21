using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProBilling.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public bool IsBillable { get; set; }
        public string Designation { get; set; }
        public List<TeamUserMapping> TeamUserMapping { get; set; }
        public List<SprintActivity> UserActivities { get; set; }
    }
}
