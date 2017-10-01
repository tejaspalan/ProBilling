using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Models
{
    public class Team : IProBillingEntity
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public List<Sprint> Sprints { get; set; }
        public List<TeamUserMapping> TeamUserMapping { get; set; }
    }
}
