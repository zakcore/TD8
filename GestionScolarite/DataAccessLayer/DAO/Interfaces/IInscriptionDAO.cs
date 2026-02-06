using GestionScolarite.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionScolarite.DataAccessLayer.DAO.Interfaces
{
    internal interface IInscriptionDAO {
        // Méthodes CRUD pour entités avec clés composées
        void Ajouter(Inscription inscription);
        void Supprimer(Inscription inscription);
        List<Inscription> GetInscriptionsParEtudiant(int etudiantId);
        List<Inscription> GetInscriptionsParCours(int coursId);
        void Supprimer(int etudiantId, int coursId, string session); // clé composée


    }
}
