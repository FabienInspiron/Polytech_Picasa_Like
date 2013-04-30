using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPicasaLike
{
    class Utilisateur
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private String nom;

        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        private String prenom;

        public String Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }
        private String mdp;

        public String Mdp
        {
            get { return mdp; }
            set { mdp = value; }
        }

        Utilisateur(String nom, String prenom, String mdp)
        {
            this.id = 0;
            this.nom = nom;
            this.prenom = prenom;
            this.mdp = mdp;
        }
    }
}
