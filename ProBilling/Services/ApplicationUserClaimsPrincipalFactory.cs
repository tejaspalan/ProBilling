using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProBilling.Services
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var claimsPrincipal = await base.CreateAsync(user);
            var identity = claimsPrincipal.Identities.First();

            identity.AddClaim(new Claim("Designation", user.Designation.ToString()));

            return claimsPrincipal;
        }
    }
}
