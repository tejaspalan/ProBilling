using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Models
{
    public class SprintReport : IProBillingEntity
    {
        public int SprintReportId { get; set; }
        public int SprintId { get; set; }
        public Sprint Sprint { get; set; }
        public double TotalBillableHours { get; set; }
        public double TotalBackupHours { get; set; }
        public double TotalNonBillableHours { get; set; }
        public bool ApprovedByClient { get; set; }
        public DateTime TimeOfClientResponse { get; set; }
        public string CustomerRemarks { get; set; }
    }
}
