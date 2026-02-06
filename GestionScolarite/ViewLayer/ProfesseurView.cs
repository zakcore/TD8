using System;
using System.Collections.Generic;

namespace GestionScolarite.ViewLayer
{
    public class ProfesseurView
    {
        public int AfficherMenuProfesseur()
        {
            Console.WriteLine("\n--- GESTION DES PROFESSEURS ---");
            Console.WriteLine("1. Lister les professeurs");
            Console.WriteLine("2. Ajouter un professeur");
            Console.WriteLine("3. Modifier un professeur");
            Console.WriteLine("4. Supprimer un professeur");
            Console.WriteLine("0. Retour au menu principal");
            Console.Write("Choix : ");
            int.TryParse(Console.ReadLine(), out int choix);
            return choix;
        }

        public (string prenom, string nom) SaisirInfosProfesseur()
        {
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine();
            Console.Write("Nom : ");
            string nom = Console.ReadLine();
            return (prenom, nom);
        }

        public int DemanderIdProfesseur()
        {
            Console.Write("ID du professeur : ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

      
        public virtual void AfficherListe(List<(int id, string prenom, string nom)> professeurs)
        {
            Console.WriteLine("\nListe des professeurs :");
            foreach (var p in professeurs)
            {
                Console.WriteLine($"[{p.id}] {p.prenom} {p.nom}");
            }
        }

        public virtual void AfficherMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
