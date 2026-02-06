using GestionScolarite.ModelLayer;


namespace GestionScolarite.DataAccessLayer.DAO.Interfaces
{
    internal interface ICoursDAO : IDao<Cours>
    {
        Cours? GetByCode(string code); // Exemple
    }
}

