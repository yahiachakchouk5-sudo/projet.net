
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetApp.Models;
using ProjetApp.Models.Data;
using System.Threading.Tasks;

namespace ProjetApp.Controllers
{
    [Authorize]
    public class FormateurController : Controller
    {
        private readonly AppDbContext _context;

        public FormateurController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Formateur
        public async Task<IActionResult> Index()
        {
            var formateurs = await _context.Formateurs
                .Include(f => f.Formations)
                .ToListAsync();
            return View(formateurs);
        }

        // GET: /Formateur/Create
        public IActionResult Create() => View();

        // POST: /Formateur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Formateur formateur)
        {
            if (ModelState.IsValid)
            {
                _context.Formateurs.Add(formateur);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Formateur ajouté avec succès !";
                return RedirectToAction(nameof(Index));
            }
            return View(formateur);
        }

        // GET: /Formateur/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var formateur = await _context.Formateurs.FindAsync(id);
            if (formateur == null) return NotFound();
            return View(formateur);
        }

        // POST: /Formateur/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Formateur formateur)
        {
            if (id != formateur.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(formateur);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Formateur modifié avec succès !";
                return RedirectToAction(nameof(Index));
            }
            return View(formateur);
        }

        // GET: /Formateur/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var formateur = await _context.Formateurs
                .Include(f => f.Formations)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (formateur == null) return NotFound();
            return View(formateur);
        }

        // POST: /Formateur/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formateur = await _context.Formateurs.FindAsync(id);
            if (formateur != null)
            {
                _context.Formateurs.Remove(formateur);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Formateur supprimé.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}