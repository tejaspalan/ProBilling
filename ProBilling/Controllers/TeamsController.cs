using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProBilling.Authentication;
using ProBilling.Class;
using ProBilling.Data;
using ProBilling.Models;

namespace ProBilling.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

	    [Authorize(PolicyConstants.AdminOnlyPolicy)]
		// GET: Teams
		public async Task<IActionResult> Index()
        {
            return View(await _context.Team.ToListAsync());
        }

	    [Authorize(PolicyConstants.AdminOnlyPolicy)]
		// GET: Teams/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

	    [Authorize(PolicyConstants.AdminOnlyPolicy)]
		// GET: Teams/Create
		public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(PolicyConstants.AdminOnlyPolicy)]
        public async Task<IActionResult> Create([Bind("TeamId,TeamName,CustomerName")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

		// GET: Teams/Edit/5
		[Authorize(PolicyConstants.AdminOnlyPolicy)]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(PolicyConstants.AdminOnlyPolicy)]
		public async Task<IActionResult> Edit(int id, [Bind("TeamId,TeamName,CustomerName")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
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
            return View(team);
        }

		// GET: Teams/Delete/5
		[Authorize(PolicyConstants.AdminOnlyPolicy)]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .SingleOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(PolicyConstants.AdminOnlyPolicy)]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Team.SingleOrDefaultAsync(m => m.TeamId == id);
            _context.Team.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

	    [Authorize(PolicyConstants.AdminOnlyPolicy)]
		public async Task<IActionResult> AddUserToTeam()
		{
			var listOfTeams = await _context.Team.ToListAsync();

			var teams = new List<SelectListItem>();
			teams.Add(new SelectListItem
			{
				Text = "Select",
				Value = ""
			});

			foreach (Team team in listOfTeams)
			{
				teams.Add(new SelectListItem { Text = team.TeamName, Value = team.TeamId.ToString()});
			}

			ViewBag.Teams = teams;
			
			return View();
		}

	    [Authorize(PolicyConstants.AdminOnlyPolicy)]
		public async Task<IActionResult> GetAllAvailableUsers()
	    {
		    var userList = await _context.Users.Where(item =>  item.TeamUserMapping.All(x => x.UserId != item.Id) && item.Designation != 1000 || item.Designation == 100 || item.Designation ==200).ToListAsync();
		    List<TeamTableViewModel> teamTableViewModels = new List<TeamTableViewModel>();
			foreach (ApplicationUser appUser in userList)
			{
				TeamTableViewModel teamTableViewModel = new TeamTableViewModel
				{
					UserName = appUser.Name,
					Designation = ((DesignationEnum)appUser.Designation).ToString(),
					Email = appUser.Email,
					UserId = appUser.Id
				};

				teamTableViewModels.Add(teamTableViewModel);

			}
		    ViewBag.ShowAdd = true;
			return PartialView("TeamTable", teamTableViewModels);

		}

	    public async Task<IActionResult> GetUsersForTheTeam(int teamId)
	    {
			var userList = await _context.Users.Where(item => item.TeamUserMapping.Any(x => x.TeamId == teamId && x.UserId == item.Id) && item.Designation != 1000).ToListAsync();
		    List<TeamTableViewModel> teamTableViewModels = new List<TeamTableViewModel>();
		    foreach (ApplicationUser appUser in userList)
		    {
			    TeamTableViewModel teamTableViewModel = new TeamTableViewModel
			    {
				    UserName = appUser.Name,
				    Designation = ((DesignationEnum)appUser.Designation).ToString(),
				    Email = appUser.Email,
				    UserId = appUser.Id
			    };

			    teamTableViewModels.Add(teamTableViewModel);

		    }
		    ViewBag.ShowAdd = false;
		    ViewBag.ShowRemove = true;
		
			return PartialView("TeamTable", teamTableViewModels);
		}

	    [Authorize(PolicyConstants.AdminOnlyPolicy)]
		public async Task<IActionResult> InsertUserToTeam(int teamId, string userId)
	    {
		    IQueryable<TeamUserMapping> teamUserMappings = _context.TeamUserMapping.Where(item => item.UserId == userId && item.TeamId == teamId);
		    if (!teamUserMappings.Any())
		    {
			    TeamUserMapping teamUserMapping = new TeamUserMapping {TeamId = teamId, UserId = userId};
			    _context.Add(teamUserMapping);
			    await _context.SaveChangesAsync();
		    }
		    return RedirectToAction(nameof(GetAllAvailableUsers));
	    }

	    [Authorize(PolicyConstants.AdminOnlyPolicy)]
	    public async Task<IActionResult> RemoveUserFromTeam(int teamId, string userId)
	    {
			var teamUserMapping = await _context.TeamUserMapping.SingleOrDefaultAsync(m => m.TeamId == teamId && m.UserId == userId);
		    _context.TeamUserMapping.Remove(teamUserMapping);
		    await _context.SaveChangesAsync();
		    return RedirectToAction(nameof(GetAllAvailableUsers));
		}

		private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.TeamId == id);
        }
    }
}
