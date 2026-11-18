using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetApp.Models
{
    [Table("Etudiants")]
    public class Etudiant
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [MaxLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [MaxLength(100)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Format téléphone invalide")]
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Le numéro étudiant est obligatoire")]
        [Display(Name = "Numéro étudiant")]
        [MaxLength(20)]
        public string NumeroEtudiant { get; set; }

        // Navigation → Inscriptions
        public virtual ICollection<Inscription>? Inscriptions { get; set; }
    }
}