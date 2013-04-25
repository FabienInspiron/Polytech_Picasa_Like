using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPicasaLike
{
    class Album
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
        private Utilisateur user;

        public Utilisateur User
        {
            get { return user; }
            set { user = value; }
        }

        /// <summary>
        /// Création d'un utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nom"></param>
        /// <param name="user"></param>
        Album(String nom, Utilisateur user)
        {
            this.id = 0;
            this.nom = nom;
            this.user = user;
        }
    }
}
