
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetApp.Models
{
    [Table("Formateurs")]
    public class Formateur
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

        [Required(ErrorMessage = "La spécialité est obligatoire")]
        [Display(Name = "Spécialité")]
        public string Specialite { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // Navigation → Formations
        public virtual ICollection<Formation>? Formations { get; set; }
    }
}