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
    public class EnrollmentController : Controller
    {
        private readonly PSAContext _context;

        public EnrollmentController(PSAContext context)
        {
            _context = context;
        }

        // GET: Enrollment
        public async Task<IActionResult> Index(int? ucId)
        {
            if (_context.Enrollments == null) return Problem("Entity set 'PSAContext.Enrollments' is null.");
            
            ViewBag.Ucs = from uc in _context.Ucs
                select new SelectListItem { Value = uc.UcId.ToString(), Text = uc.Name };

            if (ucId.HasValue)
            {
                return View(from enrollment in _context.Enrollments
                        .Include(x => x.Student)
                        .Include(x => x.Uc)
                    where enrollment.Uc.UcId == ucId.Value
                    select enrollment);
            }

            return View(await _context.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Uc).ToListAsync());
        }

        // GET: Enrollment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollment/Create
        public IActionResult Create()
        {
            ViewBag.Students = from student in _context.Students
                select new SelectListItem { Value = student.StudentId.ToString(), Text = student.Name };
            ViewBag.Ucs = from uc in _context.Ucs
                select new SelectListItem { Value = uc.UcId.ToString(), Text = uc.Name };

            return View();
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId")] Enrollment enrollment)
        {
            var form = await Request.ReadFormAsync();
            int ucId, studentId;
            int.TryParse(form["Uc"], out ucId);
            int.TryParse(form["Student"], out studentId);
            
            enrollment.Uc = await (from uc in _context.Ucs 
                where uc.UcId == ucId select uc).FirstAsync();
            enrollment.Student = await (from student in _context.Students 
                where student.StudentId == studentId select student).FirstAsync();

            ModelState.Remove("Student");
            ModelState.Remove("Uc");
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await (from e in _context.Enrollments.Include(x => x.Student)
                    .Include(x => x.Uc).AsQueryable()
                where e.EnrollmentId == id
                select e).FirstAsync();
            if (enrollment == null)
            {
                return NotFound();
            }
            
            ViewBag.Students = from student in _context.Students
                select new SelectListItem { Value = student.StudentId.ToString(), Text = student.Name };
            ViewBag.Ucs = from uc in _context.Ucs
                select new SelectListItem { Value = uc.UcId.ToString(), Text = uc.Name };

            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }
            
            var form = await Request.ReadFormAsync();
            int ucId, studentId;
            int.TryParse(form["Uc"], out ucId);
            int.TryParse(form["Student"], out studentId);
            
            enrollment.Uc = await (from uc in _context.Ucs 
                where uc.UcId == ucId select uc).FirstAsync();
            enrollment.Student = await (from student in _context.Students 
                where student.StudentId == studentId select student).FirstAsync();

            ModelState.Remove("Uc");
            ModelState.Remove("Student");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
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

            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollments == null)
            {
                return Problem("Entity set 'PSAContext.Enrollments'  is null.");
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return (_context.Enrollments?.Any(e => e.EnrollmentId == id)).GetValueOrDefault();
        }
    }
}