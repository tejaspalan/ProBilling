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
    public class SprintActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SprintActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SprintActivities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SprintActivity.Include(s => s.Sprint).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SprintActivities/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprintActivity = await _context.SprintActivity
                .Include(s => s.Sprint)
                .Include(s => s.User)
                .SingleOrDefaultAsync(m => m.ActivityId == id);
            if (sprintActivity == null)
            {
                return NotFound();
            }

            return View(sprintActivity);
        }

        // GET: SprintActivities/Create
        public IActionResult Create()
        {
            ViewData["SprintId"] = new SelectList(_context.Sprint, "SprintId", "SprintId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: SprintActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,SprintId,UserId,AvailableHours,BackupHours,CompanyMeetingHours,OnsiteHours,OvertimeHours,HolidayOvertimeHours,Remarks")] SprintActivity sprintActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sprintActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SprintId"] = new SelectList(_context.Sprint, "SprintId", "SprintId", sprintActivity.SprintId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sprintActivity.UserId);
            return View(sprintActivity);
        }

        // GET: SprintActivities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprintActivity = await _context.SprintActivity.SingleOrDefaultAsync(m => m.ActivityId == id);
            if (sprintActivity == null)
            {
                return NotFound();
            }
            ViewData["SprintId"] = new SelectList(_context.Sprint, "SprintId", "SprintId", sprintActivity.SprintId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sprintActivity.UserId);
            return View(sprintActivity);
        }

        // POST: SprintActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ActivityId,SprintId,UserId,AvailableHours,BackupHours,CompanyMeetingHours,OnsiteHours,OvertimeHours,HolidayOvertimeHours,Remarks")] SprintActivity sprintActivity)
        {
            if (id != sprintActivity.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sprintActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SprintActivityExists(sprintActivity.ActivityId))
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
            ViewData["SprintId"] = new SelectList(_context.Sprint, "SprintId", "SprintId", sprintActivity.SprintId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sprintActivity.UserId);
            return View(sprintActivity);
        }

        // GET: SprintActivities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprintActivity = await _context.SprintActivity
                .Include(s => s.Sprint)
                .Include(s => s.User)
                .SingleOrDefaultAsync(m => m.ActivityId == id);
            if (sprintActivity == null)
            {
                return NotFound();
            }

            return View(sprintActivity);
        }

        // POST: SprintActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sprintActivity = await _context.SprintActivity.SingleOrDefaultAsync(m => m.ActivityId == id);
            _context.SprintActivity.Remove(sprintActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SprintActivityExists(long id)
        {
            return _context.SprintActivity.Any(e => e.ActivityId == id);
        }
    }
}
