using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using Microsoft.Data.SqlClient;


namespace GestionScolarite.DataAccessLayer.DAO.Implémentations
{
    internal class InscriptionDAO : IInscriptionDAO
    {
        private readonly SqlConnection connection;
        private readonly IDao<Etudiant> etudiantDAO;
        private readonly IDao<Cours> coursDAO;

        //injection des dépendances
        public InscriptionDAO(SqlConnection connection, IDao<Etudiant> etudiantDAO, IDao<Cours> coursDAO)
        {
            this.connection = connection;
            this.etudiantDAO = etudiantDAO;
            this.coursDAO = coursDAO;
        }

        public void Ajouter(Inscription inscription)
        {
            var requetteAjout = "INSERT INTO Inscriptions (EtudiantId, CoursId, Session, Note) VALUES (@EtudiantId, @CoursId, @Session, @Note)";
            using (var cmd = new SqlCommand(requetteAjout, connection))
            {
                cmd.Parameters.AddWithValue("@EtudiantId", inscription.Etudiant.Id);
                cmd.Parameters.AddWithValue("@CoursId", inscription.Cours.Id);
                cmd.Parameters.AddWithValue("@Session", inscription.Session);
                // Si Note est null, on envoie DBNull.Value pour SQL Server
                cmd.Parameters.AddWithValue("@Note", inscription.Note.HasValue ? (object)inscription.Note.Value : DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void Supprimer(Inscription inscription)
        {
            Supprimer(inscription.Etudiant.Id, inscription.Cours.Id, inscription.Session);
        }

        public List<Inscription> GetInscriptionsParEtudiant(int etudiantId)
        {
            var liste = new List<Inscription>();

            var requette = "SELECT EtudiantId, CoursId, Session, Note FROM Inscriptions WHERE EtudiantId = @EtudiantId";
            using (var cmd = new SqlCommand(requette, connection))
            {
                cmd.Parameters.AddWithValue("@EtudiantId", etudiantId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Récupérer les objets métier via les DAO injectés
                        Etudiant? etudiant = etudiantDAO.GetById((int)reader["EtudiantId"]);
                        Cours? cours = coursDAO.GetById((int)reader["CoursId"]);
                        string session = (string)reader["Session"];
                        int? note = reader.IsDBNull(reader.GetOrdinal("Note")) ? null : (int?)Convert.ToInt32(reader["Note"]);

                        if (etudiant != null && cours != null)
                        {
                            liste.Add(new Inscription(etudiant, cours, session, note));
                        }
                    }
                }
            }

            return liste;
        }

        public List<Inscription> GetInscriptionsParCours(int coursId)
        {
            var liste = new List<Inscription>();

            var requette = "SELECT EtudiantId, CoursId, Session, Note FROM Inscriptions WHERE CoursId = @CoursId";
            using (var cmd = new SqlCommand(requette, connection))
            {
                cmd.Parameters.AddWithValue("@CoursId", coursId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Etudiant? etudiant = etudiantDAO.GetById((int)reader["EtudiantId"]);
                        Cours? cours = coursDAO.GetById((int)reader["CoursId"]);
                        string session = (string)reader["Session"];
                        int? note = reader.IsDBNull(reader.GetOrdinal("Note")) ? null : (int?)Convert.ToInt32(reader["Note"]);

                        if (etudiant != null && cours != null)
                        {
                            liste.Add(new Inscription(etudiant, cours, session, note));
                        }
                    }
                }
            }

            return liste;
        }

        public void Supprimer(int etudiantId, int coursId, string session)
        {
            var requetteSupp = "DELETE FROM Inscriptions WHERE EtudiantId = @EtudiantId AND CoursId = @CoursId AND Session = @Session";
            using (var cmd = new SqlCommand(requetteSupp, connection))
            {
                cmd.Parameters.AddWithValue("@EtudiantId", etudiantId);
                cmd.Parameters.AddWithValue("@CoursId", coursId);
                cmd.Parameters.AddWithValue("@Session", session);
                cmd.ExecuteNonQuery();
            }
        }

    }

}
