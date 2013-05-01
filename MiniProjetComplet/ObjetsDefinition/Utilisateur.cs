using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ObjetDefinition
{
    [DataContractAttribute]
    public class Utilisateur
    {
        [DataMemberAttribute]
        public int Id { get; set; }
        [DataMemberAttribute]
        public String Nom { get; set; }
        [DataMemberAttribute]
        public String Prenom { get; set; }
        [DataMemberAttribute]
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
