
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ProjetApp.Models
{
    [Table("Formations")]
    public class Formation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [MaxLength(150)]
        [Display(Name = "Titre")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "La description est obligatoire")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La date de début est obligatoire")]
        [Display(Name = "Date de début")]
        [DataType(DataType.Date)]
        public DateTime DateDebut { get; set; }

        [Required(ErrorMessage = "La date de fin est obligatoire")]
        [Display(Name = "Date de fin")]
        [DataType(DataType.Date)]
        public DateTime DateFin { get; set; }

        [Required(ErrorMessage = "La durée est obligatoire")]
        [Display(Name = "Durée (heures)")]
        [Range(1, 1000, ErrorMessage = "La durée doit être entre 1 et 1000 heures")]
        public int Duree { get; set; }

        [Required(ErrorMessage = "Le nombre de places est obligatoire")]
        [Display(Name = "Nombre de places")]
        [Range(1, 500)]
        public int NombrePlaces { get; set; }

        [Required(ErrorMessage = "La salle est obligatoire")]
        [Display(Name = "Salle")]
        public string Salle { get; set; }

        // Clé étrangère → Formateur
        [Display(Name = "Formateur")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un formateur")]
        public int FormateurId { get; set; }  

        [ForeignKey("FormateurId")]
        public virtual Formateur? Formateur { get; set; }

        // Navigation → Inscriptions
        public virtual ICollection<Inscription>? Inscriptions { get; set; } = new List<Inscription>();

        
    }
}
