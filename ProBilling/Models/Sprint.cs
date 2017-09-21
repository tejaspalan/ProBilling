using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Models
{
    public class Sprint
    {
        public int SprintId { get; set; }
        public int SprintNumber { get; set; }
        public DateTime SprintStart { get; set; }
        public DateTime SprintEnd { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public List<SprintActivity> SprintActivities { get; set; }
    }
}
