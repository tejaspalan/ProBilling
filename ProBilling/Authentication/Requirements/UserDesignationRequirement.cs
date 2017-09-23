using Microsoft.AspNetCore.Authorization;
using ProBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Authentication
{
    public class UserDesignationRequirement : IAuthorizationRequirement
    {
        List<DesignationEnum> _allowedDesignations;
        public UserDesignationRequirement(List<DesignationEnum> designations)
        {
            _allowedDesignations = designations;
        }
        public bool IsAccessAllowedToUser(DesignationEnum userDesignation)
        {
            return _allowedDesignations.Any(designation => designation == userDesignation);
        }
    }
}
