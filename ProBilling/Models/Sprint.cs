using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Models
{
    public class Sprint : IProBillingEntity
    {
        public int SprintId { get; set; }
        public int SprintNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SprintStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SprintEnd { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public List<SprintActivity> SprintActivities { get; set; }
        public int SprintReportId { get; set; }
        public SprintReport SprintReport { get; set; }
        public bool IsSprintCompletedSuccessfully { get; set; }
    }
}
