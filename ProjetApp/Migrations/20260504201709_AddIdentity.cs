using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetApp.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Inscriptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Etudiants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "waleddine@mail.tn", "Djmal", "Waleddine" });

            migrationBuilder.UpdateData(
                table: "Etudiants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "islem@mail.tn", "Benzaid", "Islem" });

            migrationBuilder.UpdateData(
                table: "Etudiants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "rania@mail.tn", "Belgacem", "Rania" });

            migrationBuilder.UpdateData(
                table: "Formateurs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "kalell@etraining.tn", "Kalell", "Fahmi" });

            migrationBuilder.UpdateData(
                table: "Formateurs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "kharrt@etraining.tn", "Kharrt", "Ahmad" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Inscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Etudiants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "ahmed@mail.tn", "Mnejja", "Ahmed" });

            migrationBuilder.UpdateData(
                table: "Etudiants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "rania@mail.tn", "Chaabane", "Rania" });

            migrationBuilder.UpdateData(
                table: "Etudiants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "youssef@mail.tn", "Hamdi", "Youssef" });

            migrationBuilder.UpdateData(
                table: "Formateurs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "benali@etraining.tn", "Ben Ali", "Mohamed" });

            migrationBuilder.UpdateData(
                table: "Formateurs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Nom", "Prenom" },
                values: new object[] { "trabelsi@etraining.tn", "Trabelsi", "Sonia" });
        }
    }
}
