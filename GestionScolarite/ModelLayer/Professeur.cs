using System;
using System.Collections.Generic;

namespace GestionScolarite.ModelLayer
{
    public class Professeur
    {
        private int id;
        private string nom;
        private string prenom;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Nom
        {
            get => nom;
            set => nom = value;
        }

        public string Prenom
        {
            get => prenom;
            set => prenom = value;
        }

        // Constructeur pour un professeur récupéré de la BD (ou créé fictivement)
        public Professeur(int id, string nom, string prenom)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
        }

        // Constructeur pour un professeur à insérer (Id géré par la BD)
        public Professeur(string nom, string prenom)
        {
            this.nom = nom;
            this.prenom = prenom;
        }
    }
}
