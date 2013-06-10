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
        public Album(String nom, Utilisateur user)
            : this(-1, nom, user.Id)
        { }

        /// <summary>
        /// Création d'un album
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="userId"></param>
        public Album(String nom, int userId)
            : this(-1, nom, userId)
        { }

        public Album(int id, String nom)
            : this(id, nom, -1)
        { }

        public Album(int id, String nom, int userId)
        {
            this.Id = id;
            this.Nom = nom;
            this.UserId = userId;
        }
    }
}
