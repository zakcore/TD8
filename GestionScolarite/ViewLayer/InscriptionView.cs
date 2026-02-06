using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionScolarite.ViewLayer
{
    internal class InscriptionView
    {
        public int AfficherMenuInscription()
        {
            Console.WriteLine("\n--- GESTION DES INSCRIPTIONS ---");
            Console.WriteLine("1. Lister les inscriptions par étudiant");
            Console.WriteLine("2. Lister les inscriptions par cours");
            Console.WriteLine("3. Ajouter une inscription");
            Console.WriteLine("4. Supprimer une inscription");
            Console.WriteLine("0. Retour au menu principal");
            Console.Write("Choix : ");
            int.TryParse(Console.ReadLine(), out int choix);
            return choix;
        }

        public (int etudiantId, int coursId, string session, int? note) SaisirInfosInscription()
        {
            Console.Write("ID de l'étudiant : ");
            int etudiantId = Convert.ToInt32(Console.ReadLine());

            Console.Write("ID du cours : ");
            int coursId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Session (ex: H25, A24) : ");
            string session = Console.ReadLine();

            Console.Write("Note (sur 100, ou Entrée pour aucune) : ");
            string noteStr = Console.ReadLine();
            int? note = null;
            if (!string.IsNullOrWhiteSpace(noteStr))
            {
                if (int.TryParse(noteStr, out int noteVal))
                {
                    note = noteVal;
                }
            }

            return (etudiantId, coursId, session, note);
        }

        public void AfficherListeParEtudiant(int etudiantId, List<(string code, string titre, string session, int? note)> inscriptions)
        {
            Console.WriteLine($"\nInscriptions de l'étudiant (ID: {etudiantId}) :");
            if (inscriptions.Count == 0)
            {
                Console.WriteLine("  Aucune inscription trouvée.");
            }
            else
            {
                foreach (var ins in inscriptions)
                {
                    string noteAffichage = ins.note.HasValue ? ins.note.Value.ToString() : "N/A";
                    Console.WriteLine($"  {ins.code} - {ins.titre} | Session: {ins.session} | Note: {noteAffichage}");
                }
            }
        }

        public void AfficherListeParCours(int coursId, List<(string prenom, string nom, string session, int? note)> inscriptions)
        {
            Console.WriteLine($"\nInscriptions du cours (ID: {coursId}) :");
            if (inscriptions.Count == 0)
            {
                Console.WriteLine("  Aucune inscription trouvée.");
            }
            else
            {
                foreach (var ins in inscriptions)
                {
                    string noteAffichage = ins.note.HasValue ? ins.note.Value.ToString() : "N/A";
                    Console.WriteLine($"  {ins.prenom} {ins.nom} | Session: {ins.session} | Note: {noteAffichage}");
                }
            }
        }

        public (int etudiantId, int coursId, string session) DemanderCléInscription()
        {
            Console.Write("ID de l'étudiant : ");
            int etudiantId = Convert.ToInt32(Console.ReadLine());

            Console.Write("ID du cours : ");
            int coursId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Session (ex: H25, A24) : ");
            string session = Console.ReadLine();

            return (etudiantId, coursId, session);
        }
        public int DemanderIdEtudiant()
        {
            Console.Write("ID de l'étudiant : ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public int DemanderIdCours()
        {
            Console.Write("ID du cours : ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public void AfficherMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
