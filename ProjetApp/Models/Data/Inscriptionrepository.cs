using ProjetApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProjetApp.Models.Data
{
    /// <summary>
    /// Repository ADO.NET pour le CRUD des Inscriptions (mode connecté)
    /// Utilise SqlConnection, SqlCommand, SqlDataReader
    /// </summary>
    public class InscriptionRepository
    {
        private readonly string _connectionString;

        public InscriptionRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("G4_StockDBConnection");
        }

        // ─────────────────────────────────────────────────────────────
        // READ – SELECT avec SqlDataReader
        // ─────────────────────────────────────────────────────────────
        public List<Inscription> GetAll()
        {
            var inscriptions = new List<Inscription>();
            SqlConnection cnx = new SqlConnection(_connectionString);

            string strSQL = @"
                SELECT i.Id, i.DateInscription, i.Statut,
                       i.EtudiantId,  e.Nom  AS NomEtudiant,  e.Prenom  AS PrenomEtudiant,
                       i.FormationId, f.Titre AS TitreFormation
                FROM   Inscriptions i
                INNER JOIN Etudiants  e ON i.EtudiantId  = e.Id
                INNER JOIN Formations f ON i.FormationId = f.Id
                ORDER BY i.DateInscription DESC";
            SqlConnection G4_StockDBConnection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, G4_StockDBConnection);
            G4_StockDBConnection.Open();                              // ← une seule fois ici
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                inscriptions.Add(new Inscription
                {
                    Id = dr.GetInt32("Id"),
                    DateInscription = dr.GetDateTime("DateInscription"),
                    Statut = dr.GetString("Statut"),
                    EtudiantId = dr.GetInt32("EtudiantId"),
                    FormationId = dr.GetInt32("FormationId"),
                    Etudiant = new Etudiant { Nom = dr.GetString("NomEtudiant"), Prenom = dr.GetString("PrenomEtudiant") },
                    Formation = new Formation { Titre = dr.GetString("TitreFormation") }
                });
            }
            cnx.Close();
            return inscriptions;
        }

        // ─────────────────────────────────────────────────────────────
        // READ BY ID – SELECT par identifiant
        // ─────────────────────────────────────────────────────────────
        public Inscription GetById(int id)
        {
            Inscription inscription = null;
            SqlConnection cnx = new SqlConnection(_connectionString);

            string strSQL = @"
                SELECT i.Id, i.DateInscription, i.Statut, i.EtudiantId, i.FormationId
                FROM   Inscriptions i
                WHERE  i.Id = @id";

            SqlCommand cmd = new SqlCommand(strSQL, cnx);
            cmd.Parameters.AddWithValue("@id", id);
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                inscription = new Inscription
                {
                    Id = dr.GetInt32("Id"),
                    DateInscription = dr.GetDateTime("DateInscription"),
                    Statut = dr.GetString("Statut"),
                    EtudiantId = dr.GetInt32("EtudiantId"),
                    FormationId = dr.GetInt32("FormationId")
                };
            }
            cnx.Close();
            return inscription;
        }

        // ─────────────────────────────────────────────────────────────
        // CREATE – INSERT avec requête paramétrée
        // ─────────────────────────────────────────────────────────────
        public int Add(Inscription insc)
        {
            SqlConnection cnx = new SqlConnection(_connectionString);

            string strSQL = @"
                INSERT INTO Inscriptions (DateInscription, Statut, EtudiantId, FormationId)
                VALUES (@date, @statut, @etudiantId, @formationId)";

            SqlCommand cmd = new SqlCommand(strSQL, cnx);
            cmd.Parameters.AddWithValue("@date", insc.DateInscription);
            cmd.Parameters.AddWithValue("@statut", insc.Statut);
            cmd.Parameters.AddWithValue("@etudiantId", insc.EtudiantId);
            cmd.Parameters.AddWithValue("@formationId", insc.FormationId);

            cnx.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnx.Close();
            return rowsAffected;
        }

        // ─────────────────────────────────────────────────────────────
        // UPDATE – Modifier une inscription
        // ─────────────────────────────────────────────────────────────
        public int Update(Inscription insc)
        {
            SqlConnection cnx = new SqlConnection(_connectionString);

            string strSQL = @"
                UPDATE Inscriptions
                SET    DateInscription = @date,
                       Statut          = @statut,
                       EtudiantId      = @etudiantId,
                       FormationId     = @formationId
                WHERE  Id = @id";

            SqlCommand cmd = new SqlCommand(strSQL, cnx);
            cmd.Parameters.AddWithValue("@date", insc.DateInscription);
            cmd.Parameters.AddWithValue("@statut", insc.Statut);
            cmd.Parameters.AddWithValue("@etudiantId", insc.EtudiantId);
            cmd.Parameters.AddWithValue("@formationId", insc.FormationId);
            cmd.Parameters.AddWithValue("@id", insc.Id);

            cnx.Open();
            int n = cmd.ExecuteNonQuery();
            cnx.Close();
            return n;
        }

        // ─────────────────────────────────────────────────────────────
        // DELETE – Supprimer une inscription
        // ─────────────────────────────────────────────────────────────
        public int Delete(int id)
        {
            SqlConnection cnx = new SqlConnection(_connectionString);

            string strSQL = "DELETE FROM Inscriptions WHERE Id = @id";

            SqlCommand cmd = new SqlCommand(strSQL, cnx);
            cmd.Parameters.AddWithValue("@id", id);

            cnx.Open();
            int n = cmd.ExecuteNonQuery();
            cnx.Close();
            return n;
        }

        // ─────────────────────────────────────────────────────────────
        // CHECK – Vérifier si un étudiant est déjà inscrit
        // ─────────────────────────────────────────────────────────────
        public bool IsDejaInscrit(int etudiantId, int formationId)
        {
            SqlConnection cnx = new SqlConnection(_connectionString);

            string strSQL = @"
                SELECT COUNT(*) FROM Inscriptions
                WHERE EtudiantId = @etudiantId AND FormationId = @formationId";

            SqlCommand cmd = new SqlCommand(strSQL, cnx);
            cmd.Parameters.AddWithValue("@etudiantId", etudiantId);
            cmd.Parameters.AddWithValue("@formationId", formationId);

            cnx.Open();
            int count = (int)cmd.ExecuteScalar();
            cnx.Close();
            return count > 0;
        }
    }
}
