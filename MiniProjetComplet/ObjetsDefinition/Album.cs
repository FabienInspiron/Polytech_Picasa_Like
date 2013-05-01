using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace ObjetDefinition
{
    [DataContractAttribute]
    public class Album
    {
        [DataMemberAttribute]
        public int Id { get; set; }
        [DataMemberAttribute]
        public String Nom { get; set; }
        [DataMemberAttribute]
        public int UserId { get; set; }

        /// <summary>
        /// Création d'un album
        /// </summary>
        /// <param name="nom">Nom du doissier</param>
        /// <param name="user">Propriétaire du dossier</param>
        public Album(String nom, Utilisateur user) : this(nom, user.Id)
        { }

        /// <summary>
        /// Création d'un album
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="userId"></param>
        public Album(String nom, int userId)
        {
            this.Id = 0;
            this.Nom = nom;
            this.UserId = userId;
        }


        public Album(int id, String nom)
        {
            this.Id = id;
            this.Nom = nom;
            this.UserId = -1;
        }
    }

    public class AlbumCollection : ObservableCollection<Album>
    { }
}
