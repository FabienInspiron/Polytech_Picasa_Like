using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjetDefinition
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }

        public String Mdp { get; set; }


        public Utilisateur(String nom, String prenom, String mdp)
        {
            this.Id = -1;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Mdp = mdp;
        }
    }
}
