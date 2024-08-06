using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RateMyTechship.Data;
using RateMyTechship.Models;

namespace RateMyTechship.Controllers
{
    public class InternshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InternshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Internships
        public async Task<IActionResult> Index()
        {
            return View(await _context.Internships.OrderBy(i => i.CompanyName).ToListAsync());
        }

        public async Task<IActionResult> ShowInternships(string SearchPhrase)
        {
            var searchResults = await _context.Internships
                .Where(r => r.CompanyName.Contains(SearchPhrase) ||
                            r.Role.Contains(SearchPhrase) ||
                            r.Location.Contains(SearchPhrase) ||
                            r.Term.Contains(SearchPhrase)).OrderBy(r => r.CompanyName).ToListAsync();

            if (searchResults.Count == 0)
            {
                ViewData["Message"] = "No internships found.";
            }
            else
            {
                ViewData["Message"] = ""; // Clear the message if there are search results
            }

            return View("Index", searchResults);
        }

        [Authorize(Roles = "Admin")]
        // GET: Internships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internships = await _context.Internships
                .FirstOrDefaultAsync(m => m.ID == id);
            if (internships == null)
            {
                return NotFound();
            }

            return View(internships);
        }

        [Authorize(Roles = "Admin")]
        // GET: Internships/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Internships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CompanyName,Role,Location,Term,ApplicationLink,ApplicationDeadline")] Internships internships)
        {
            if (ModelState.IsValid)
            {
                _context.Add(internships);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(internships);
        }

        [Authorize(Roles = "Admin")]
        // GET: Internships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internships = await _context.Internships.FindAsync(id);
            if (internships == null)
            {
                return NotFound();
            }
            return View(internships);
        }

        [Authorize(Roles = "Admin")]
        // POST: Internships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CompanyName,Role,Location,Term,ApplicationLink,ApplicationDeadline")] Internships internships)
        {
            if (id != internships.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internships);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternshipsExists(internships.ID))
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
            return View(internships);
        }

        [Authorize(Roles = "Admin")]
        // GET: Internships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internships = await _context.Internships
                .FirstOrDefaultAsync(m => m.ID == id);
            if (internships == null)
            {
                return NotFound();
            }

            return View(internships);
        }

        [Authorize(Roles = "Admin")]
        // POST: Internships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var internships = await _context.Internships.FindAsync(id);
            if (internships != null)
            {
                _context.Internships.Remove(internships);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternshipsExists(int id)
        {
            return _context.Internships.Any(e => e.ID == id);
        }
    }
}
