using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etudiants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroEtudiant = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Specialite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duree = table.Column<int>(type: "int", nullable: false),
                    NombrePlaces = table.Column<int>(type: "int", nullable: false),
                    Salle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormateurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formations_Formateurs_FormateurId",
                        column: x => x.FormateurId,
                        principalTable: "Formateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EtudiantId = table.Column<int>(type: "int", nullable: false),
                    FormationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscriptions_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscriptions_Formations_FormationId",
                        column: x => x.FormationId,
                        principalTable: "Formations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Etudiants",
                columns: new[] { "Id", "Email", "Nom", "NumeroEtudiant", "Prenom", "Telephone" },
                values: new object[,]
                {
                    { 1, "ahmed@mail.tn", "Mnejja", "ETU001", "Ahmed", "98001122" },
                    { 2, "rania@mail.tn", "Chaabane", "ETU002", "Rania", "98003344" },
                    { 3, "youssef@mail.tn", "Hamdi", "ETU003", "Youssef", "98005566" }
                });

            migrationBuilder.InsertData(
                table: "Formateurs",
                columns: new[] { "Id", "Email", "Nom", "Prenom", "Specialite" },
                values: new object[,]
                {
                    { 1, "benali@etraining.tn", "Ben Ali", "Mohamed", "Développement .NET" },
                    { 2, "trabelsi@etraining.tn", "Trabelsi", "Sonia", "Base de données" },
                    { 3, "gharbi@etraining.tn", "Gharbi", "Karim", "Intelligence Artificielle" }
                });

            migrationBuilder.InsertData(
                table: "Formations",
                columns: new[] { "Id", "DateDebut", "DateFin", "Description", "Duree", "FormateurId", "NombrePlaces", "Salle", "Titre" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Développement web avec ASP.NET Core MVC", 30, 1, 15, "Salle A1", "ASP.NET MVC Avancé" },
                    { 2, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gestion des BDs avec EF Core", 20, 2, 10, "Salle B2", "SQL Server & Entity Framework" },
                    { 3, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bases du machine learning et deep learning", 25, 3, 12, "Salle C3", "Introduction à l'IA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Formations_FormateurId",
                table: "Formations",
                column: "FormateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_EtudiantId",
                table: "Inscriptions",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_FormationId",
                table: "Inscriptions",
                column: "FormationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscriptions");

            migrationBuilder.DropTable(
                name: "Etudiants");

            migrationBuilder.DropTable(
                name: "Formations");

            migrationBuilder.DropTable(
                name: "Formateurs");
        }
    }
}
