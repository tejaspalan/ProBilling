using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Models
{
    public class SprintReportViewModel
    {
	    public string UserName;
	    public string Designation;
	    public int TotalLeaves;
	    public string LeaveDates;
	    public double BillableHours;
	    public double BackupHours;
	    public double CompanyMeetingHours;
	    public double NonBillableHours;
	    public double TotalBillableHours;
	    public double TotalNonBillableHours;
	    public double TotalBackupHours;
	    public string StatusOfSprint;
	    public string ApprovedByCustomer;
    }
}
