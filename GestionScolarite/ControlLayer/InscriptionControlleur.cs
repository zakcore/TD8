using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using GestionScolarite.ViewLayer;
using System;
using System.Collections.Generic;

namespace GestionScolarite.ControlLayer
{
    internal class InscriptionControlleur
    {
        private readonly IInscriptionDAO inscriptionDAO;
        private readonly InscriptionView view;
        private readonly IEtudiantDAO etudiantDAO;
        private readonly ICoursDAO coursDAO;

        public InscriptionControlleur(IInscriptionDAO inscriptionDAO, InscriptionView view, IEtudiantDAO etudiantDAO, ICoursDAO coursDAO)
        {
            this.inscriptionDAO = inscriptionDAO;
            this.view = view;
            this.etudiantDAO = etudiantDAO;
            this.coursDAO = coursDAO;
        }

        public void GererMenuInscription()
        {
            bool continuer = true;

            while (continuer)
            {
                int choix = view.AfficherMenuInscription();

                switch (choix)
                {
                    case 1:
                        ListerParEtudiant();
                        break;
                    case 2:
                        ListerParCours();
                        break;
                    case 3:
                        AjouterInscription();
                        break;
                    case 4:
                        SupprimerInscription();
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

        private void ListerParEtudiant()
        {
            int etudiantId = view.DemanderIdEtudiant();

            List<Inscription> inscriptions = inscriptionDAO.GetInscriptionsParEtudiant(etudiantId);

            // Convertir les objets domaine en tuples pour la vue
            var liste = new List<(string code, string titre, string session, int? note)>();
            foreach (var ins in inscriptions)
            {
                liste.Add((ins.Cours.Code, ins.Cours.Titre, ins.Session, ins.Note));
            }

            view.AfficherListeParEtudiant(etudiantId, liste);
        }

        private void ListerParCours()
        {
            int coursId = view.DemanderIdCours();

            List<Inscription> inscriptions = inscriptionDAO.GetInscriptionsParCours(coursId);

            // Convertir les objets domaine en tuples pour la vue
            var liste = new List<(string prenom, string nom, string session, int? note)>();
            foreach (var ins in inscriptions)
            {
                liste.Add((ins.Etudiant.Prenom, ins.Etudiant.Nom, ins.Session, ins.Note));
            }

            view.AfficherListeParCours(coursId, liste);
        }

        private void AjouterInscription()
        {
            (int etudiantId, int coursId, string session, int? note) = view.SaisirInfosInscription();

            // Vérifier que l'étudiant existe
            Etudiant etudiant = etudiantDAO.GetById(etudiantId);
            if (etudiant == null)
            {
                view.AfficherMessage("Étudiant introuvable.");
                return;
            }

            // Vérifier que le cours existe
            Cours cours = coursDAO.GetById(coursId);
            if (cours == null)
            {
                view.AfficherMessage("Cours introuvable.");
                return;
            }

            if (string.IsNullOrWhiteSpace(session))
            {
                view.AfficherMessage("Session requise.");
                return;
            }

            Inscription inscription = new Inscription(etudiant, cours, session, note);
            inscriptionDAO.Ajouter(inscription);

            view.AfficherMessage("Inscription ajoutée.");
        }

        private void SupprimerInscription()
        {
            (int etudiantId, int coursId, string session) = view.DemanderCléInscription();

            if (string.IsNullOrWhiteSpace(session))
            {
                view.AfficherMessage("Session requise.");
                return;
            }

            inscriptionDAO.Supprimer(etudiantId, coursId, session);
            view.AfficherMessage("Inscription supprimée.");
        }
    }
}
