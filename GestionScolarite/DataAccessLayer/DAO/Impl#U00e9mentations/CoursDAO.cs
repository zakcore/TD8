using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using Microsoft.Data.SqlClient;
namespace GestionScolarite.DataAccessLayer.DAO.Implémentations;

internal class CoursDAO : ICoursDAO
{

    private readonly SqlConnection connection;

    //injecter la connection pour le DAO Cours
    public CoursDAO(SqlConnection connection)
    {
        this.connection = connection;
    }

    public void Ajouter(Cours cours)
    {
        var requetteAjout = "INSERT INTO Cours (Titre, Code) VALUES (@Titre, @Code)";
        using (var cmd = new SqlCommand(requetteAjout, connection))
        {
            cmd.Parameters.AddWithValue("@Titre", cours.Titre);
            cmd.Parameters.AddWithValue("@Code", cours.Code);
            cmd.ExecuteNonQuery();
        }
    }

    public void Modifier(Cours cours)
    {
        var requetteMaj = "UPDATE Cours SET Titre = @Titre, Code = @Code WHERE Id = @Id";
        using (var cmd = new SqlCommand(requetteMaj, connection))
        {
            cmd.Parameters.AddWithValue("@Titre", cours.Titre);
            cmd.Parameters.AddWithValue("@Code", cours.Code);
            cmd.Parameters.AddWithValue("@Id", cours.Id);
            cmd.ExecuteNonQuery();
        }
    }

    public void Supprimer(int id)
    {
        var requetteSupp = "DELETE FROM Cours WHERE Id = @Id";
        using (var cmd = new SqlCommand(requetteSupp, connection))
        {
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }

    public Cours? GetById(int id)
    {
        Cours? cours = null;
        var requetteLectureParId = "SELECT * FROM Cours WHERE Id = @Id";
        using (var cmd = new SqlCommand(requetteLectureParId, connection))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    cours = new Cours(
                        (int)reader["Id"],
                        (string)reader["Titre"],
                        (string)reader["Code"]
                    );
                }
            }
        }

        return cours;
    }

    public List<Cours> GetAll()
    {
        var liste = new List<Cours>();

        var requetteGetAll = "SELECT * FROM Cours";
        using (var cmd = new SqlCommand(requetteGetAll, connection))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                liste.Add(new Cours(
                    (int)reader["Id"],
                    (string)reader["Titre"],
                    (string)reader["Code"]
                ));
            }
        }

        return liste;
    }

    public Cours? GetByCode(string code)
    {
        Cours? cours = null;
        var requette = "SELECT * FROM Cours WHERE Code = @Code";
        using (var cmd = new SqlCommand(requette, connection))
        {
            cmd.Parameters.AddWithValue("@Code", code);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    cours = new Cours(
                        (int)reader["Id"],
                        (string)reader["Titre"],
                        (string)reader["Code"]
                    );
                }
            }
        }

        return cours;
    }

}
