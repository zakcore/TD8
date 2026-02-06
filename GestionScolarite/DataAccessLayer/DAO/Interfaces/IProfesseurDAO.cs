using GestionScolarite.ModelLayer;
using System.Collections.Generic;

namespace GestionScolarite.DataAccessLayer.DAO.Interfaces
{
    public interface IProfesseurDAO
    {
        Professeur? GetById(int id);
        List<Professeur> GetAll();
        void Ajouter(Professeur professeur);
        void Modifier(Professeur professeur);
        void Supprimer(int id);
    }
}
