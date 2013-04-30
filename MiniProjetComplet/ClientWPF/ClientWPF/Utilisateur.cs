using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPicasaLike
{
    public class Utilisateur
    {
        public int id { get; set; }
        public String nom { get; set; }
        public String prenom { get; set; }

        public String mdp { get; set; }


        public Utilisateur(String nom, String prenom, String mdp)
        {
            this.id = 0;
            this.nom = nom;
            this.prenom = prenom;
            this.mdp = mdp;
        }
    }
}
