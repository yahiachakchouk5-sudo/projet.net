
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetApp.Models;
using ProjetApp.Models.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetApp.Controllers
{
    [Authorize]
    public class FormationController : Controller
    {
        private readonly AppDbContext _context;

        public FormationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Formation – Formations du mois courant
        public async Task<IActionResult> Index()
        {
            var moisCourant = DateTime.Now.Month;
            var annee = DateTime.Now.Year;

            var formations = await _context.Formations
                .Include(f => f.Formateur)
                .Include(f => f.Inscriptions)
                .Where(f => !(f.DateDebut.Month != moisCourant ||
                            f.DateDebut.Year != annee))
                .OrderBy(f => f.DateDebut)
                .ToListAsync();

            return View(formations);
        }

        // GET: /Formation/Toutes – Toutes les formations avec filtre
        public async Task<IActionResult> Toutes(int? mois, int? annee)
        {
            var query = _context.Formations
                .Include(f => f.Formateur)
                .Include(f => f.Inscriptions)
                .AsQueryable();

            if (mois.HasValue && annee.HasValue)
            {
                query = query.Where(f => f.DateDebut.Month == mois &&
                                          f.DateDebut.Year == annee);
            }

            ViewData["MoisSelectionne"] = mois ?? DateTime.Now.Month;
            ViewData["AnneeSelectionnee"] = annee ?? DateTime.Now.Year;

            var formations = await query.OrderBy(f => f.DateDebut).ToListAsync();
            return View("Index", formations);
        }

        // GET: /Formation/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var formation = await _context.Formations
                .Include(f => f.Formateur)
                .Include(f => f.Inscriptions)
                    .ThenInclude(i => i.Etudiant)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (formation == null) return NotFound();
            return View(formation);
        }

        // GET: /Formation/Create
        public IActionResult Create()
        {
            ViewData["FormateurId"] = new SelectList(_context.Formateurs, "Id", "Nom");
            return View();
        }

        // POST: /Formation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Formation formation)
        {
            if (ModelState.IsValid)
            {
                _context.Formations.Add(formation);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Formation créée avec succès !";
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

            ViewData["FormateurId"] = new SelectList(
                _context.Formateurs, "Id", "Nom", formation.FormateurId);
            return View(formation);
        }

        // GET: /Formation/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var formation = await _context.Formations.FindAsync(id);
            if (formation == null) return NotFound();

            ViewData["FormateurId"] = new SelectList(_context.Formateurs, "Id", "Nom", formation.FormateurId);
            return View(formation);
        }

        // POST: /Formation/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Formation formation)
        {
            if (id != formation.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(formation);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Formation modifiée avec succès !";
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormateurId"] = new SelectList(_context.Formateurs, "Id", "Nom", formation.FormateurId);
            return View(formation);
        }

        // GET: /Formation/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var formation = await _context.Formations
                .Include(f => f.Formateur)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (formation == null) return NotFound();
            return View(formation);
        }

        // POST: /Formation/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formation = await _context.Formations.FindAsync(id);
            if (formation != null)
            {
                _context.Formations.Remove(formation);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Formation supprimée.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}