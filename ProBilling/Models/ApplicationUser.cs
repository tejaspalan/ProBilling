using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProBilling.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser, IProBillingEntity
    {
        public string Name { get; set; }
        public int Designation { get; set; }
	    public bool IsBackup { get; set; }
		public List<TeamUserMapping> TeamUserMapping { get; set; }
        public List<SprintActivity> UserActivities { get; set; }
    }

    public enum DesignationEnum
    {
        TeamMember = 0,
        ScrumMaster = 100,
        CDL = 200,
        Customer = 300,
        Admin = 1000,
    }

	public enum AvailableFor
	{
		FullDay = 10,
		HalfDay = 11,
		Leave = 12,
	}

	public enum CompanyMeeting
	{
		Attended = 3,
		NotAttended = 4
	}
}
