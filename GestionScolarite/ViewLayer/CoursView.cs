using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionScolarite.ViewLayer
{
    internal class CoursView
    {
        public int AfficherMenuCours()
        {
            Console.WriteLine("\n--- GESTION DES COURS ---");
            Console.WriteLine("1. Lister les cours");
            Console.WriteLine("2. Ajouter un cours");
            Console.WriteLine("3. Modifier un cours");
            Console.WriteLine("4. Supprimer un cours");
            Console.WriteLine("0. Retour au menu principal");
            Console.Write("Choix : ");
            int.TryParse(Console.ReadLine(), out int choix);
            return choix;
        }

        public (string code, string titre) SaisirInfosCours()
        {
            Console.Write("Code du cours : ");
            string code = Console.ReadLine();
            Console.Write("Titre du cours : ");
            string titre = Console.ReadLine();
            return (code, titre);
        }

        public int DemanderIdCours()
        {
            Console.Write("ID du cours : ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public void AfficherListe(List<(int id, string code, string titre)> cours)
        {
            Console.WriteLine("\nListe des cours :");
            foreach (var c in cours)
            {
                Console.WriteLine($"[{c.id}] {c.code} - {c.titre}");
            }
        }

        public void AfficherMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
