using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProBilling.Data;
using ProBilling.Models;

namespace ProBilling.Controllers
{
    public class TeamUserMappingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamUserMappingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamUserMappings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeamUserMapping.Include(t => t.Team).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeamUserMappings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamUserMapping = await _context.TeamUserMapping
                .Include(t => t.Team)
                .Include(t => t.User)
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (teamUserMapping == null)
            {
                return NotFound();
            }

            return View(teamUserMapping);
        }

        // GET: TeamUserMappings/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: TeamUserMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,UserId")] TeamUserMapping teamUserMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamUserMapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", teamUserMapping.TeamId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", teamUserMapping.UserId);
            return View(teamUserMapping);
        }

        // GET: TeamUserMappings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamUserMapping = await _context.TeamUserMapping.SingleOrDefaultAsync(m => m.UserId == id);
            if (teamUserMapping == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", teamUserMapping.TeamId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", teamUserMapping.UserId);
            return View(teamUserMapping);
        }

        // POST: TeamUserMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TeamId,UserId")] TeamUserMapping teamUserMapping)
        {
            if (id != teamUserMapping.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamUserMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamUserMappingExists(teamUserMapping.UserId))
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
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", teamUserMapping.TeamId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", teamUserMapping.UserId);
            return View(teamUserMapping);
        }

        // GET: TeamUserMappings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamUserMapping = await _context.TeamUserMapping
                .Include(t => t.Team)
                .Include(t => t.User)
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (teamUserMapping == null)
            {
                return NotFound();
            }

            return View(teamUserMapping);
        }

        // POST: TeamUserMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var teamUserMapping = await _context.TeamUserMapping.SingleOrDefaultAsync(m => m.UserId == id);
            _context.TeamUserMapping.Remove(teamUserMapping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamUserMappingExists(string id)
        {
            return _context.TeamUserMapping.Any(e => e.UserId == id);
        }
    }
}
