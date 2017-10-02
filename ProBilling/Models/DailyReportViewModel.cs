using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Models
{
    public class DailyReportViewModel
    {
	    public string UserName { get; set; }
		public string UserId { get; set; }
		public string Designation { get; set; }
		public AvailableFor AvailableFor { get; set; }
		public CompanyMeeting TechTalk { get; set; }
		public CompanyMeeting BigSprintDay { get; set; }
		public CompanyMeeting Igrow { get; set; }
		public CompanyMeeting MeetingWithCdl { get; set; }
		public CompanyMeeting Smf { get; set; }
		public CompanyMeeting WoW { get; set; }
		public CompanyMeeting SprintKpi { get; set; }
		public CompanyMeeting ScrumOfScrum { get; set; }
		public bool IsBillable { get; set; }
		public int TeamId { get; set; }
		public DateTime ActivityDate { get; set; }
	}
}
