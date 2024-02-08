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
    public class UcController : Controller
    {
        private readonly PSAContext _context;

        public UcController(PSAContext context)
        {
            _context = context;
        }

        // GET: Uc
        public async Task<IActionResult> Index()
        {
              return _context.Ucs != null ? 
                          View(await _context.Ucs.ToListAsync()) :
                          Problem("Entity set 'PSAContext.Ucs'  is null.");
        }

        // GET: Uc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ucs == null)
            {
                return NotFound();
            }

            var uc = await _context.Ucs
                .FirstOrDefaultAsync(m => m.UcId == id);
            if (uc == null)
            {
                return NotFound();
            }

            return View(uc);
        }

        // GET: Uc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UcId,Name")] Uc uc)
        {
            // INÍCIO DE CÓDIGO FORA DO TESTE
            ModelState.Remove("Enrollments");
            // FIM DE CÓDIGO FORA DO TESTE
            
            if (ModelState.IsValid)
            {
                _context.Add(uc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uc);
        }

        // GET: Uc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ucs == null)
            {
                return NotFound();
            }

            var uc = await _context.Ucs.FindAsync(id);
            if (uc == null)
            {
                return NotFound();
            }
            return View(uc);
        }

        // POST: Uc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UcId,Name")] Uc uc)
        {
            if (id != uc.UcId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UcExists(uc.UcId))
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
            return View(uc);
        }

        // GET: Uc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ucs == null)
            {
                return NotFound();
            }

            var uc = await _context.Ucs
                .FirstOrDefaultAsync(m => m.UcId == id);
            if (uc == null)
            {
                return NotFound();
            }

            return View(uc);
        }

        // POST: Uc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ucs == null)
            {
                return Problem("Entity set 'PSAContext.Ucs'  is null.");
            }
            var uc = await _context.Ucs.FindAsync(id);
            if (uc != null)
            {
                _context.Ucs.Remove(uc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UcExists(int id)
        {
          return (_context.Ucs?.Any(e => e.UcId == id)).GetValueOrDefault();
        }
    }
}
