using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AdminPicasaLike
{
    public class Album
    {
        public int id { get; set; }
        public String nom { get; set; }
        public Utilisateur user { get; set; }

        /// <summary>
        /// Création d'un utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nom"></param>
        /// <param name="user"></param>
        public Album(String nom, Utilisateur user)
        {
            this.id = 0;
            this.nom = nom;
            this.user = user;
        }

        public Album(int id, String nom)
        {
            this.id = id;
            this.nom = nom;
            this.user = null;
        }
    }

    public class AlbumCollection : ObservableCollection<Album>
    { }
}
