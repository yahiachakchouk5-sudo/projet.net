
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetApp.Models;
using ProjetApp.Models.Data;
using System.Threading.Tasks;

namespace ProjetApp.Controllers
{
    [Authorize]
    public class EtudiantController : Controller
    {
        private readonly AppDbContext _context;

        public EtudiantController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Etudiant
        public async Task<IActionResult> Index()
        {
            var etudiants = await _context.Etudiants
                .Include(e => e.Inscriptions!)
                    .ThenInclude(i => i.Formation!)
                .ToListAsync();
            return View(etudiants);
        }
        // GET: /Etudiant/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var etudiant = await _context.Etudiants
                .Include(e => e.Inscriptions!)
                    .ThenInclude(i => i.Formation!)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (etudiant == null) return NotFound();
            return View(etudiant);
        }
        // GET: /Etudiant/Create
        public IActionResult Create() => View();

        // POST: /Etudiant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                _context.Etudiants.Add(etudiant);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Étudiant ajouté avec succès !";
                return RedirectToAction(nameof(Index));
            }

            // Affiche les erreurs dans la page
            foreach (var key in ModelState.Keys)
            {
                var errors = ModelState[key].Errors;
                foreach (var error in errors)
                {
                    TempData["Error"] = key + " : " + error.ErrorMessage;
                }
            }

            return View(etudiant);
        }

        // GET: /Etudiant/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant == null) return NotFound();
            return View(etudiant);
        }

        // POST: /Etudiant/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Etudiant etudiant)
        {
            if (id != etudiant.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(etudiant);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Étudiant modifié avec succès !";
                return RedirectToAction(nameof(Index));
            }
            return View(etudiant);
        }

        // GET: /Etudiant/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var etudiant = await _context.Etudiants.FirstOrDefaultAsync(e => e.Id == id);
            if (etudiant == null) return NotFound();
            return View(etudiant);
        }

        // POST: /Etudiant/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant != null)
            {
                _context.Etudiants.Remove(etudiant);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Étudiant supprimé.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
