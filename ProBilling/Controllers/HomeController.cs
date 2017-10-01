using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProBilling.Authentication;
using ProBilling.Class;
using ProBilling.Data;
using ProBilling.Models;

namespace ProBilling.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public HomeController(ApplicationDbContext context,UserManager<ApplicationUser> userManger)
		{
			_context = context;
			_userManager = userManger;
		}

		public async Task<IActionResult> Index()
		{
			ActionResult result = View();
			var currectUserDesignation =
				(DesignationEnum) (int.Parse(User.Claims.First(c => c.Type == ClaimsConstants.Designation).Value));

			if (currectUserDesignation == DesignationEnum.ScrumMaster)
				result = RedirectToAction("ScrumMasterIndex");
			if (currectUserDesignation == DesignationEnum.Admin)
				result = RedirectToAction("AdminIndex");

			var user = await _userManager.GetUserAsync(HttpContext.User);

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

		public IActionResult AdminIndex()
		{
			return View();
		}

		[Authorize(PolicyConstants.AdminSmCdlPolicy)]
		public async Task<IActionResult> ScrumMasterIndex()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			var listOfTeams = await _context.Team.Where(item => item.TeamUserMapping.Any(x => x.UserId == user.Id)).ToListAsync();

			var teams = new List<SelectListItem>();
			teams.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});
			foreach (Team team in listOfTeams)
			{
				teams.Add(new SelectListItem { Text = team.TeamName, Value = team.TeamId.ToString() });
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
	}
}
