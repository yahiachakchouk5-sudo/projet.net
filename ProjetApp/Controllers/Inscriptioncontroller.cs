
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetApp.Models;
using ProjetApp.Models.Data;
using System;
using System.Threading.Tasks;

namespace ProjetApp.Controllers
{
    [Authorize]
    /// <summary>
    /// CRUD Inscriptions via ADO.NET (InscriptionRepository)
    /// </summary>
    public class InscriptionController : Controller
    {
        private readonly InscriptionRepository _repo;
        private readonly AppDbContext _context; // pour les SelectList

        public InscriptionController(InscriptionRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        // GET: /Inscription – Liste via ADO.NET SqlDataReader
        public IActionResult Index()
        {
            var inscriptions = _repo.GetAll();
            return View(inscriptions);
        }

        // GET: /Inscription/Create
        public async Task<IActionResult> Create(int? formationId)
        {
            ViewData["EtudiantId"] = new SelectList(await _context.Etudiants.ToListAsync(), "Id", "Nom");
            ViewData["FormationId"] = new SelectList(await _context.Formations.ToListAsync(), "Id", "Titre", formationId);
            return View();
        }

        // POST: /Inscription/Create – INSERT via ADO.NET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inscription insc)
        {
            // On assigne avant la validation
            insc.DateInscription = DateTime.Now;
            insc.Statut = "Confirmée";

            // On retire Statut et DateInscription de la validation
            ModelState.Remove("Statut");
            ModelState.Remove("DateInscription");

            if (ModelState.IsValid)
            {
                _repo.Add(insc);
                TempData["Success"] = "Inscription enregistrée avec succès !";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EtudiantId"] = new SelectList(
                await _context.Etudiants.ToListAsync(), "Id", "Nom", insc.EtudiantId);
            ViewData["FormationId"] = new SelectList(
                await _context.Formations.ToListAsync(), "Id", "Titre", insc.FormationId);
            return View(insc);
        }

        // GET: /Inscription/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var insc = _repo.GetById(id);
            if (insc == null) return NotFound();

            ViewData["EtudiantId"] = new SelectList(await _context.Etudiants.ToListAsync(), "Id", "Nom", insc.EtudiantId);
            ViewData["FormationId"] = new SelectList(await _context.Formations.ToListAsync(), "Id", "Titre", insc.FormationId);
            ViewData["Statuts"] = new SelectList(new[] { "Confirmée", "En attente", "Annulée" }, insc.Statut);
            return View(insc);
        }

        // POST: /Inscription/Edit/{id} – UPDATE via ADO.NET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Inscription insc)
        {
            if (id != insc.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _repo.Update(insc);
                TempData["Success"] = "Inscription modifiée avec succès !";
                return RedirectToAction(nameof(Index));
            }
            ViewData["EtudiantId"] = new SelectList(await _context.Etudiants.ToListAsync(), "Id", "Nom", insc.EtudiantId);
            ViewData["FormationId"] = new SelectList(await _context.Formations.ToListAsync(), "Id", "Titre", insc.FormationId);
            ViewData["Statuts"] = new SelectList(new[] { "Confirmée", "En attente", "Annulée" }, insc.Statut);
            return View(insc);
        }

        // GET: /Inscription/Delete/{id}
        public IActionResult Delete(int id)
        {
            var insc = _repo.GetById(id);
            if (insc == null) return NotFound();
            return View(insc);
        }

        // POST: /Inscription/Delete/{id} – DELETE via ADO.NET
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Inscription supprimée.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Inscription/SeDesinscrire – Désinscription rapide depuis Details formation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SeDesinscrire(int inscriptionId, int formationId)
        {
            _repo.Delete(inscriptionId);
            TempData["Success"] = "Désinscription effectuée.";
            return RedirectToAction("Details", "Formation", new { id = formationId });
        }
    }
}