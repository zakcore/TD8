using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using Microsoft.Data.SqlClient;

namespace GestionScolarite.DataAccessLayer.DAO.Implémentations
{
    internal class EtudiantDAO : IEtudiantDAO
    {
        
        private readonly SqlConnection connection;

        //injecter la connection pour le DAO Etudiant
        public EtudiantDAO(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Ajouter(Etudiant etudiant)
        {
            var requetteAjout = "INSERT INTO Etudiants (Nom, Prenom) VALUES (@Nom, @Prenom)";
            using (var cmd = new SqlCommand(requetteAjout, connection))
            {
                cmd.Parameters.AddWithValue("@Nom", etudiant.Nom);
                cmd.Parameters.AddWithValue("@Prenom", etudiant.Prenom);
                cmd.ExecuteNonQuery();
            }
        }

        public void Modifier(Etudiant etudiant)
        {
            var requetteMaj = "UPDATE Etudiants SET Nom = @Nom, Prenom = @Prenom WHERE Id = @Id";
            using (var cmd = new SqlCommand(requetteMaj, connection))
            {
                cmd.Parameters.AddWithValue("@Nom", etudiant.Nom);
                cmd.Parameters.AddWithValue("@Prenom", etudiant.Prenom);
                cmd.Parameters.AddWithValue("@Id", etudiant.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Supprimer(int id)
        {
            var requetteSupp = "DELETE FROM Etudiants WHERE Id = @Id";
            using (var cmd = new SqlCommand(requetteSupp, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Etudiant? GetById(int id)
        {
            Etudiant? etudiant = null;
            var requetteLectureParId = "SELECT * FROM Etudiants WHERE Id = @Id";
            using (var cmd = new SqlCommand(requetteLectureParId, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        etudiant = new Etudiant
                        (
                            (int)reader["Id"],
                            (string)reader["Nom"],
                            (string)reader["Prenom"]
                        );
                    }
                }
            }

            return etudiant;
        }

        public List<Etudiant> GetAll()
        {
            var liste = new List<Etudiant>();

            var requetteGetAll = "SELECT * FROM Etudiants";
            using (var cmd = new SqlCommand(requetteGetAll, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    liste.Add(new Etudiant
                    (
                        (int)reader["Id"],
                        (string)reader["Nom"],
                        (string)reader["Prenom"]
                    ));
                }
            }

            return liste;
        }

    }
}



