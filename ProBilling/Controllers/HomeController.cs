using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProBilling.Authentication;
using ProBilling.Class;
using ProBilling.Models;

namespace ProBilling.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			ActionResult result = View();
			if (User.Identity.Name != null && User.Identity.Name.Equals("vibhavmaheshwari@gmail.com"))
			{
				result = RedirectToAction("ScrumMasterIndex");
			}
			return result;
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult ScrumMasterIndex()
		{
			var teams = new List<SelectListItem>();
			teams.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});

			foreach (Teams eVal in Enum.GetValues(typeof(Teams)))
			{
				teams.Add(new SelectListItem { Text = Enum.GetName(typeof(Teams), eVal), Value = eVal.ToString() });
			}

			ViewBag.Teams = teams;
			return View();
		}

		public IActionResult TeamTable(string teamId)
		{
			IList<TeamTableViewModel> teamTableViewModels = new List<TeamTableViewModel>
			{
				new TeamTableViewModel
				{
					UserName = "Vibhav Maheshwari",
					Designation = "Scrum Master",
					Email = "v.maheshwari@prowareness.nl"
				},
				new TeamTableViewModel
				{
					UserName = "Apresh Chandra",
					Designation = "Developer",
					Email = "a.chandra@prowareness.nl"
				},
				new TeamTableViewModel
				{
					UserName = "Samarth Radhakrishna",
					Designation = "Developer",
					Email = "s.redhakrishna@prowareness.nl"
				},
				new TeamTableViewModel
				{
					UserName = "Shankar Kantharaj",
					Designation = "Developer",
					Email = "s.kantharaj@prowareness.nl"
				},
				new TeamTableViewModel
				{
					UserName = "Rakesh Raghy Shetty",
					Designation = "Developer",
					Email = "r.shetty@prowareness.nl"
				},
				new TeamTableViewModel
				{
					UserName = "Rahul Oswal",
					Designation = "Developer",
					Email = "r.oswal@prowareness.nl"
				},
			};

			return PartialView("TeamTable", teamTableViewModels);
		}

		public IActionResult ViewPreviousSprints()
		{
			var teams = new List<SelectListItem>();
			teams.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});

			foreach (Teams eVal in Enum.GetValues(typeof(Teams)))
			{
				teams.Add(new SelectListItem { Text = Enum.GetName(typeof(Teams), eVal), Value = eVal.ToString() });
			}

			ViewBag.Teams = teams;

			var sprints = new List<SelectListItem>();
			sprints.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});
			sprints.Add(new SelectListItem
			{
				Text = "Sprint 1",
				Value = "1"
			});
			sprints.Add(new SelectListItem
			{
				Text = "Sprint 2",
				Value = "2"
			});
			sprints.Add(new SelectListItem
			{
				Text = "Sprint 3",
				Value = "3"
			});

			ViewBag.Sprints = sprints;

			return View();
		}

		public IActionResult ViewPreviousReport(string teamId, string sprintId)
		{
			IList<SprintReportViewModel> sprintReportViewModels = new List<SprintReportViewModel>
			{
				new SprintReportViewModel
				{
					UserName = "Vibhav Maheshwari",
					Designation = "Scrum Master",
					TotalLeaves = 1,
					LeaveDates = "28-07-2017",
					BillableHours = 67.75,
					BackupHours = 0.00,
					CompanyMeetingHours = 8.75,
					NonBillableHours = 0.00
				},
				new SprintReportViewModel
				{
					UserName = "Apresh Chandra",
					Designation = "Developer",
					TotalLeaves = 0,
					LeaveDates = "",
					BillableHours = 80.00,
					BackupHours = 0.00,
					CompanyMeetingHours =5.00,
					NonBillableHours = 0.00
				},
				new SprintReportViewModel
				{
					UserName = "Samarth Radhakrishna",
					Designation = "Developer",
					TotalLeaves = 4,
					LeaveDates = "Leaves on dd MMM YYYY , dd MMM YYYY",
					BillableHours = 46.00,
					BackupHours = 0.00,
					CompanyMeetingHours = 5.00,
					NonBillableHours = 0.00
				},
				new SprintReportViewModel
				{
					UserName = "Shankar Kantharaj",
					Designation = "Developer",
					TotalLeaves = 2,
					LeaveDates = "Leaves on dd MMM YYYY , dd MMM YYYY",
					BillableHours = 75.00,
					BackupHours = 0.00,
					CompanyMeetingHours = 5.00,
					NonBillableHours = 0.00
				},
				new SprintReportViewModel
				{
					UserName = "Rakesh Raghy Shetty",
					Designation = "Developer",
					TotalLeaves = 0,
					LeaveDates = "Leaves on dd MMM YYYY , dd MMM YYYY",
					BillableHours = 80.00,
					BackupHours = 0.00,
					CompanyMeetingHours = 5.00,
					NonBillableHours = 0.00
				},
				new SprintReportViewModel
				{
					UserName = "Rahul Oswal",
					Designation = "Backup Developer",
					TotalLeaves = 1,
					LeaveDates = "Leaves on dd MMM YYYY , dd MMM YYYY",
					BillableHours =0.00,
					BackupHours = 42.50,
					CompanyMeetingHours = 0.00,
					NonBillableHours = 34.00
				},
			};

			return PartialView("SprintReport", sprintReportViewModels);
		}

		public IActionResult ViewCurrentSprint()
		{
			var teams = new List<SelectListItem>();
			teams.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});

			foreach (Teams eVal in Enum.GetValues(typeof(Teams)))
			{
				teams.Add(new SelectListItem { Text = Enum.GetName(typeof(Teams), eVal), Value = eVal.ToString() });
			}

			ViewBag.Teams = teams;

			var sprintDates = new List<SelectListItem>();

			sprintDates.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});

			sprintDates.Add(new SelectListItem
			{
				Text = "Monday 01 Jan 2017",
				Value = "01-01-2017"
			});
			sprintDates.Add(new SelectListItem
			{
				Text = "Tuesday 02 Jan 2017",
				Value = "02-01-2017"
			});
			sprintDates.Add(new SelectListItem
			{
				Text = "Wednesday 03 Jan 2017",
				Value = "03-01-2017"
			});
			sprintDates.Add(new SelectListItem
			{
				Text = "Thrusday 04 Jan 2017",
				Value = "03-01-2017"
			});
			

			ViewBag.Sprints = sprintDates;

			return View();
		}
	}
}
