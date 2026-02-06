using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using GestionScolarite.ViewLayer;
using System.Collections.Generic;

namespace GestionScolarite.ControlLayer
{
    internal class ProfesseurController
    {
        private readonly IProfesseurDAO professeurDAO;
        private readonly ProfesseurView view;

        public ProfesseurController(IProfesseurDAO professeurDAO, ProfesseurView view)
        {
            this.professeurDAO = professeurDAO;
            this.view = view;
        }

        public void GererMenuProfesseur()
        {
            bool continuer = true;

            while (continuer)
            {
                int choix = view.AfficherMenuProfesseur();

                switch (choix)
                {
                    case 1:
                        ListerProfesseurs();
                        break;
                    case 2:
                        AjouterProfesseur();
                        break;
                    case 3:
                        ModifierProfesseur();
                        break;
                    case 4:
                        SupprimerProfesseur();
                        break;
                    case 0:
                        continuer = false;
                        break;
                    default:
                        view.AfficherMessage("Choix invalide.");
                        break;
                }
            }
        }

     
        public void ListerProfesseurs()
        {
            List<Professeur> professeurs = professeurDAO.GetAll();

            List<(int id, string prenom, string nom)> liste = new List<(int, string, string)>();

            foreach (var p in professeurs)
            {
                liste.Add((p.Id, p.Prenom, p.Nom));
            }

            view.AfficherListe(liste);
        }

      
        public Professeur? RecupererProfesseur(int id)
        {
            Professeur professeur = professeurDAO.GetById(id);

            if (professeur == null)
            {
                view.AfficherMessage("Professeur introuvable.");
                return null;
            }

            return professeur;
        }

        private void AjouterProfesseur()
        {
            (string prenom, string nom) = view.SaisirInfosProfesseur();

            if (string.IsNullOrWhiteSpace(prenom) || string.IsNullOrWhiteSpace(nom))
            {
                view.AfficherMessage("Prénom et nom requis.");
                return;
            }

            Professeur nouveauProfesseur = new Professeur(nom, prenom);
            professeurDAO.Ajouter(nouveauProfesseur);

            view.AfficherMessage("Professeur ajouté.");
        }

        private void ModifierProfesseur()
        {
            int id = view.DemanderIdProfesseur();
            Professeur professeur = professeurDAO.GetById(id);

            if (professeur == null)
            {
                view.AfficherMessage("Professeur introuvable.");
                return;
            }

            (string prenom, string nom) = view.SaisirInfosProfesseur();

            if (string.IsNullOrWhiteSpace(prenom) || string.IsNullOrWhiteSpace(nom))
            {
                view.AfficherMessage("Champs invalides.");
                return;
            }

            professeur.Prenom = prenom;
            professeur.Nom = nom;
            professeurDAO.Modifier(professeur);
            view.AfficherMessage("Professeur modifié.");
        }

        private void SupprimerProfesseur()
        {
            int id = view.DemanderIdProfesseur();
            Professeur professeur = professeurDAO.GetById(id);

            if (professeur == null)
            {
                view.AfficherMessage("Professeur introuvable.");
                return;
            }

            professeurDAO.Supprimer(id);
            view.AfficherMessage("Professeur supprimé.");
        }
    }
}
