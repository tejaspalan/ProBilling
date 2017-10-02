using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProBilling.Authentication;
using ProBilling.Data;
using ProBilling.Models;

namespace ProBilling.Controllers
{
	public class SprintsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public SprintsController(ApplicationDbContext context, UserManager<ApplicationUser> userManger)
		{
			_context = context;
			_userManager = userManger;
		}

		// GET: Sprints
		public async Task<IActionResult> Index(int teamId)
		{
			var applicationDbContext = _context.Sprint.Where(item => item.TeamId == teamId).Include(s => s.Team);
			return View(await applicationDbContext.ToListAsync());
		}

		public async Task<IActionResult> ViewAllSprintsForTeam()
		{
			await LoadTeamsForUser();
			return View();
		}

		public async Task<IActionResult> ViewCurrentSprint()
		{
			await LoadTeamsForUser();
			var sprintDates = new List<SelectListItem>();
			sprintDates.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});
			ViewBag.Sprints = sprintDates;

			return View();
		}

		public async Task<IActionResult> LoadSprintDates(int teamId)
		{
			DateTime currentDateTime = DateTime.Now;
			var sprints = await _context.Sprint.Where(item => item.TeamId == teamId && item.SprintStart <= currentDateTime &&
														item.SprintEnd >= currentDateTime).ToListAsync();
			var currentSprint = sprints.FirstOrDefault();
			var sprintDates = new List<SelectListItem>();
			if (currentSprint != null)
			{
				List<DateTime> allDates = GetDatesBetween(currentSprint.SprintStart, currentSprint.SprintEnd);
				
				foreach (DateTime date in allDates)
				{
					sprintDates.Add(new SelectListItem { Text = date.DayOfWeek + " " + date.Date.ToString("dd-MM-yyyy"), Value = date.ToString("dd-MM-yyyy",CultureInfo.InvariantCulture) });
				}
			}

			return Json(sprintDates);
		}

		public async Task<IActionResult> LoadReportForSprintDate(int teamId, string date)
		{
			DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
			var sprintList = await _context.Sprint
				.Where(item => item.TeamId == teamId && item.SprintStart <= parsedDate && item.SprintEnd >= parsedDate).ToListAsync();

			IList<SprintActivity> sprintActivities = new List<SprintActivity>();

			Sprint sprint = sprintList.FirstOrDefault();
			if (sprint != null)
				 sprintActivities = await _context.SprintActivity
					.Where(item => item.SprintId == sprint.SprintId && item.ActivityDate == parsedDate).ToListAsync();

			List<DailyReportViewModel> dailyReportViewModels = new List<DailyReportViewModel>();
			if (!sprintActivities.Any())
			{
				var userList = await _context.Users.Where(item => item.TeamUserMapping.Any(x => x.TeamId == teamId && x.UserId == item.Id) && item.Designation != 1000 && item.Designation != 300 && item.Designation!= 200).ToListAsync();
				foreach (ApplicationUser user in userList)
				{
					DailyReportViewModel dailyReportViewModel = new DailyReportViewModel
					{
						UserId = user.Id,
						UserName = user.Name,
						Designation = ((DesignationEnum)user.Designation).ToString(),
						IsBillable = true,
						TeamId = teamId,
						ActivityDate = parsedDate
					};
					dailyReportViewModels.Add(dailyReportViewModel);
				}
			}
			LoadViewBags();
			return PartialView("DailySprintReport", dailyReportViewModels);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SaveSprintActivity(IEnumerable<DailyReportViewModel> dailyReportViewModels)
		{
			return RedirectToAction(nameof(ViewCurrentSprint));
		}


		// GET: Sprints/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sprint = await _context.Sprint
				.Include(s => s.Team)
				.SingleOrDefaultAsync(m => m.SprintId == id);
			if (sprint == null)
			{
				return NotFound();
			}

			return View(sprint);
		}

		[Authorize(PolicyConstants.AdminSmCdlPolicy)]
		public async Task<IActionResult> Create()
		{
			await LoadTeamsForUser();
			return View();
		}

		// POST: Sprints/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("SprintId,SprintNumber,SprintStart,SprintEnd,TeamId")] Sprint sprint)
		{
			var sprintList = await _context.Sprint.Where(item => item.TeamId == sprint.TeamId).ToListAsync();
			foreach (Sprint s in sprintList)
			{
				if ((sprint.SprintStart >= s.SprintStart && sprint.SprintStart <= s.SprintEnd)
					|| (sprint.SprintEnd >= s.SprintStart && sprint.SprintEnd <= s.SprintEnd))
					ModelState.AddModelError("", "You cannot add the sprint for the selected dates");
			}
			if (ModelState.IsValid)
			{
				_context.Add(sprint);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(ViewAllSprintsForTeam));
			}
			await LoadTeamsForUser();
			return View(sprint);
		}

		// GET: Sprints/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sprint = await _context.Sprint.SingleOrDefaultAsync(m => m.SprintId == id);
			if (sprint == null)
			{
				return NotFound();
			}
			await LoadTeamsForUser();
			return View(sprint);
		}

		// POST: Sprints/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("SprintId,SprintNumber,SprintStart,SprintEnd,TeamId")] Sprint sprint)
		{
			if (id != sprint.SprintId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(sprint);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SprintExists(sprint.SprintId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", sprint.TeamId);
			return View(sprint);
		}

		// GET: Sprints/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sprint = await _context.Sprint
				.Include(s => s.Team)
				.SingleOrDefaultAsync(m => m.SprintId == id);
			if (sprint == null)
			{
				return NotFound();
			}

			return View(sprint);
		}

		// POST: Sprints/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var sprint = await _context.Sprint.SingleOrDefaultAsync(m => m.SprintId == id);
			_context.Sprint.Remove(sprint);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SprintExists(int id)
		{
			return _context.Sprint.Any(e => e.SprintId == id);
		}

		private async Task LoadTeamsForUser()
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
		}

		private List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
		{
			List<DateTime> allDates = new List<DateTime>();

			for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
				if (date.DayOfWeek == DayOfWeek.Monday || 
					date.DayOfWeek == DayOfWeek.Tuesday ||
					date.DayOfWeek == DayOfWeek.Wednesday ||
					date.DayOfWeek == DayOfWeek.Thursday ||
					date.DayOfWeek == DayOfWeek.Friday)
					allDates.Add(date);

			return allDates;

		}

		private void LoadViewBags()
		{
			var availableFor = new List<SelectListItem>();

			availableFor.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});

			foreach (AvailableFor eVal in Enum.GetValues(typeof(AvailableFor)))
			{
				availableFor.Add(
					new SelectListItem { Text = Enum.GetName(typeof(AvailableFor), eVal), Value = ((int)eVal).ToString() });
			}

			ViewBag.AvailableFor = availableFor;

			var companyMeeting = new List<SelectListItem>();

			companyMeeting.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});

			foreach (CompanyMeeting eVal in Enum.GetValues(typeof(CompanyMeeting)))
			{
				companyMeeting.Add(new SelectListItem
				{
					Text = Enum.GetName(typeof(CompanyMeeting), eVal),
					Value = ((int)eVal).ToString()
				});
			}

			ViewBag.CompanyMeeting = companyMeeting;
		}
	}
}
