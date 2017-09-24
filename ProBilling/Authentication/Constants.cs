using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Authentication
{
    public static class PolicyConstants
    {
        public const string AdminOnlyPolicy = "Admin";
        public const string AdminSmCdlPolicy = "Admin_ScrumMaster_Cdl";
    }

    public static class ClaimsConstants
    {
        public const string Designation = "Designation";
    }
}
