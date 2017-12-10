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
    public class SacramentProgramsController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public SacramentProgramsController(SacramentPlannerContext context)
        {
            _context = context;
        }

        // GET: SacramentPrograms
        public async Task<IActionResult> Index()
        {
            return View(await _context.SacramentProgram.ToListAsync());
        }

        // GET: SacramentPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacramentProgram = await _context.SacramentProgram
                .SingleOrDefaultAsync(m => m.SacramentProgramId == id);
            if (sacramentProgram == null)
            {
                return NotFound();
            }

            return View(sacramentProgram);
        }

        // GET: SacramentPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SacramentPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SacramentProgramId,MeetingDate,Conducting,OpeningSong,OpeningPrayer,SacramentSong,IntermediateSong,ClosingSong,ClosingPrayer")] SacramentProgram sacramentProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sacramentProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create/"+sacramentProgram.SacramentProgramId, "Speakers",sacramentProgram.SacramentProgramId);
                }
            return View(sacramentProgram);
        }

        // GET: SacramentPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacramentProgram = await _context.SacramentProgram.SingleOrDefaultAsync(m => m.SacramentProgramId == id);
            if (sacramentProgram == null)
            {
                return NotFound();
            }
            return View(sacramentProgram);
        }

        // POST: SacramentPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SacramentProgramId,MeetingDate,Conducting,OpeningSong,OpeningPrayer,SacramentSong,IntermediateSong,ClosingSong,ClosingPrayer")] SacramentProgram sacramentProgram)
        {
            if (id != sacramentProgram.SacramentProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sacramentProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SacramentProgramExists(sacramentProgram.SacramentProgramId))
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
            return View(sacramentProgram);
        }

        // GET: SacramentPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacramentProgram = await _context.SacramentProgram
                .SingleOrDefaultAsync(m => m.SacramentProgramId == id);
            if (sacramentProgram == null)
            {
                return NotFound();
            }

            return View(sacramentProgram);
        }

        // POST: SacramentPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sacramentProgram = await _context.SacramentProgram.SingleOrDefaultAsync(m => m.SacramentProgramId == id);
            _context.SacramentProgram.Remove(sacramentProgram);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SacramentProgramExists(int id)
        {
            return _context.SacramentProgram.Any(e => e.SacramentProgramId == id);
        }
    }
}