using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Models
{
    public class DailyReportViewModel
    {
	    public string UserName { get; set; }
		public string UserId { get; set; }
		public string Designation { get; set; }
		[Required]
		public AvailableFor AvailableFor { get; set; }
	    [Required]
		public CompanyMeeting TechTalk { get; set; }
	    [Required]
		public CompanyMeeting BigSprintDay { get; set; }
	    [Required]
		public CompanyMeeting Igrow { get; set; }
	    [Required]
		public CompanyMeeting MeetingWithCdl { get; set; }
	    [Required]
		public CompanyMeeting Smf { get; set; }
	    [Required]
		public CompanyMeeting WoW { get; set; }
	    [Required]
		public CompanyMeeting SprintKpi { get; set; }
	    [Required]
		public CompanyMeeting ScrumOfScrum { get; set; }
		public bool IsBackup { get; set; }
		public int TeamId { get; set; }
		public DateTime ActivityDate { get; set; }
	}
}
