using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetApp.Models;

namespace ProjetApp.Models.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<Formation> Formations { get; set; }    
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Formateur> Formateurs { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data – Formateurs
            modelBuilder.Entity<Formateur>().HasData(
                new Formateur { Id = 1, Nom = "Kalell", Prenom = "Fahmi", Specialite = "Développement .NET", Email = "kalell@etraining.tn" },
                new Formateur { Id = 2, Nom = "Kharrt", Prenom = "Ahmad", Specialite = "Base de données", Email = "kharrt@etraining.tn" },
                new Formateur { Id = 3, Nom = "Gharbi", Prenom = "Karim", Specialite = "Intelligence Artificielle", Email = "gharbi@etraining.tn" }
            );

            // Seed data – Etudiants
            modelBuilder.Entity<Etudiant>().HasData(
                new Etudiant { Id = 1, Nom = "Djmal", Prenom = "Waleddine", Email = "waleddine@mail.tn", Telephone = "98001122", NumeroEtudiant = "ETU001" },
                new Etudiant { Id = 2, Nom = "Benzaid", Prenom = "Islem", Email = "islem@mail.tn", Telephone = "98003344", NumeroEtudiant = "ETU002" },
                new Etudiant { Id = 3, Nom = "Belgacem", Prenom = "Rania", Email = "rania@mail.tn", Telephone = "98005566", NumeroEtudiant = "ETU003" }
            );

            // Seed data – Formations du mois courant
            var now = System.DateTime.Now;
             modelBuilder.Entity<Formation>().HasData(
               new Formation
               {
                   Id = 1,
                   Titre = "ASP.NET MVC Avancé",
                   Description = "Développement web avec ASP.NET Core MVC",
                   DateDebut = new System.DateTime(now.Year, now.Month, 5),
                   DateFin = new System.DateTime(now.Year, now.Month, 10),
                   Duree = 30,
                   NombrePlaces = 15,
                   Salle = "Salle A1",
                   FormateurId = 1
               },
               new Formation
               {
                   Id = 2,
                   Titre = "SQL Server & Entity Framework",
                   Description = "Gestion des BDs avec EF Core",
                   DateDebut = new System.DateTime(now.Year, now.Month, 12),
                   DateFin = new System.DateTime(now.Year, now.Month, 15),
                   Duree = 20,
                   NombrePlaces = 10,
                   Salle = "Salle B2",
                   FormateurId = 2
               },
               new Formation
               {
                   Id = 3,
                   Titre = "Introduction à l'IA",
                   Description = "Bases du machine learning et deep learning",
                   DateDebut = new System.DateTime(now.Year, now.Month, 18),
                   DateFin = new System.DateTime(now.Year, now.Month, 22),
                   Duree = 25,
                   NombrePlaces = 12,
                   Salle = "Salle C3",
                   FormateurId = 3
               }
           );
        }
    }
}