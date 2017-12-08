using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentProject.Models;

namespace SacramentProject.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public SpeakersController(SacramentPlannerContext context)
        {
            _context = context;
        }

        // GET: Speakers
        public async Task<IActionResult> Index()
        {
            var sacramentPlannerContext = _context.Speakers.Include(s => s.SacramentProgram);
            return View(await sacramentPlannerContext.ToListAsync());
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers
                .Include(s => s.SacramentProgram)
                .SingleOrDefaultAsync(m => m.SpeakerProgramId == id);
            if (speakers == null)
            {
                return NotFound();
            }

            return View(speakers);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            ViewData["SacramentProgramId"] = new SelectList(_context.SacramentProgram, "SacramentProgramId", "MeetingDate");
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakerProgramId,SacramentProgramId,Name,Topic")] Speakers speakers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speakers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SacramentProgramId"] = new SelectList(_context.SacramentProgram, "SacramentProgramId", "MeetingDate", speakers.SacramentProgramId);
            return View(speakers);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers.SingleOrDefaultAsync(m => m.SpeakerProgramId == id);
            if (speakers == null)
            {
                return NotFound();
            }
            ViewData["SacramentProgramId"] = new SelectList(_context.SacramentProgram, "SacramentProgramId", "MeetingDate", speakers.SacramentProgramId);
            return View(speakers);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakerProgramId,SacramentProgramId,Name,Topic")] Speakers speakers)
        {
            if (id != speakers.SpeakerProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speakers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakersExists(speakers.SpeakerProgramId))
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
            ViewData["SacramentProgramId"] = new SelectList(_context.SacramentProgram, "SacramentProgramId", "MeetingDate", speakers.SacramentProgramId);
            return View(speakers);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers
                .Include(s => s.SacramentProgram)
                .SingleOrDefaultAsync(m => m.SpeakerProgramId == id);
            if (speakers == null)
            {
                return NotFound();
            }

            return View(speakers);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speakers = await _context.Speakers.SingleOrDefaultAsync(m => m.SpeakerProgramId == id);
            _context.Speakers.Remove(speakers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakersExists(int id)
        {
            return _context.Speakers.Any(e => e.SpeakerProgramId == id);
        }
    }
}
