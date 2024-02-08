using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using daw_en_2324.Models;

namespace daw_en_2324.Controllers
{
    public class CurricularProgramController : Controller
    {
        private readonly PSAContext _context;

        public CurricularProgramController(PSAContext context)
        {
            _context = context;
        }

        // GET: CurricularProgram
        public async Task<IActionResult> Index()
        {
              return _context.CurricularPrograms != null ? 
                          View(await _context.CurricularPrograms.ToListAsync()) :
                          Problem("Entity set 'PSAContext.CurricularPrograms'  is null.");
        }

        // GET: CurricularProgram/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CurricularPrograms == null)
            {
                return NotFound();
            }

            var curricularProgram = await _context.CurricularPrograms
                .FirstOrDefaultAsync(m => m.CurricularProgramId == id);
            if (curricularProgram == null)
            {
                return NotFound();
            }

            return View(curricularProgram);
        }

        // GET: CurricularProgram/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CurricularProgram/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurricularProgramId,Name")] CurricularProgram curricularProgram)
        {
            ModelState.Remove("Ucs");
            if (ModelState.IsValid)
            {
                _context.Add(curricularProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curricularProgram);
        }

        // GET: CurricularProgram/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CurricularPrograms == null)
            {
                return NotFound();
            }

            var curricularProgram = await _context.CurricularPrograms.FindAsync(id);
            if (curricularProgram == null)
            {
                return NotFound();
            }
            return View(curricularProgram);
        }

        // POST: CurricularProgram/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurricularProgramId,Name")] CurricularProgram curricularProgram)
        {
            if (id != curricularProgram.CurricularProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curricularProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurricularProgramExists(curricularProgram.CurricularProgramId))
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
            return View(curricularProgram);
        }

        // GET: CurricularProgram/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CurricularPrograms == null)
            {
                return NotFound();
            }

            var curricularProgram = await _context.CurricularPrograms
                .FirstOrDefaultAsync(m => m.CurricularProgramId == id);
            if (curricularProgram == null)
            {
                return NotFound();
            }

            return View(curricularProgram);
        }

        // POST: CurricularProgram/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CurricularPrograms == null)
            {
                return Problem("Entity set 'PSAContext.CurricularPrograms'  is null.");
            }
            var curricularProgram = await _context.CurricularPrograms.FindAsync(id);
            if (curricularProgram != null)
            {
                _context.CurricularPrograms.Remove(curricularProgram);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurricularProgramExists(int id)
        {
          return (_context.CurricularPrograms?.Any(e => e.CurricularProgramId == id)).GetValueOrDefault();
        }
    }
}
