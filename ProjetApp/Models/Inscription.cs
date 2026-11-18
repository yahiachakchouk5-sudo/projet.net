using ProjetApp.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetApp.Models
{
    [Table("Inscriptions")]
    public class Inscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date d'inscription")]
        [DataType(DataType.Date)]
        public DateTime DateInscription { get; set; }

        
        [Display(Name = "Statut")]
        public string? Statut { get; set; } // Confirmée / En attente / Annulée

        // Clé étrangère → Etudiant
        [Display(Name = "Étudiant")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un étudiant")]
        public int EtudiantId { get; set; }

        [ForeignKey("EtudiantId")]
        public virtual Etudiant? Etudiant { get; set; }

        // Clé étrangère → Formation
        [Display(Name = "Formation")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner une formation")]
        public int FormationId { get; set; }

        [ForeignKey("FormationId")]
        public virtual Formation? Formation { get; set; }
    }
}