using System;

namespace ProBilling.Models
{
    public class SprintActivity : IProBillingEntity
    {
        //SprintId, Date, UserId, Available Hours (Present/Full Day/Half Day/ Absent), Backup Hours, Company meeting hours, Onsite hours, Onsite hours non bilable, Overtime hours, Holiday overtime hours, Remarks
        public long ActivityId { get; set; }
        public int SprintId { get; set; }
        public Sprint Sprint { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public double AvailableHours { get; set; }
        public double BackupHours { get; set; }
        public double CompanyMeetingHours { get; set; }
        public double OnsiteHours { get; set; }
        public double OvertimeHours { get; set; }
        public double HolidayOvertimeHours { get; set; }
        public string Remarks { get; set; }
		public DateTime ActivityDate { get; set; }
    }
}
