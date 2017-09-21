using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Models
{
    public class TeamUserMapping
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
