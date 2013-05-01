using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace ObjetDefinition
{
    [DataContractAttribute]
    public class Photo
    {
        [DataMemberAttribute]
        public int Id { get; set; }
        [DataMemberAttribute]
        public String Nom { get; set; }
        [DataMemberAttribute]
        public byte[] Image { get; set; }
        [DataMemberAttribute]
        public int Album { get; set; }

        public Photo(String nom, byte[] image)
            : this(-1, nom, image, -1)
        { }

        public Photo(String nom, byte[] image, int album)
            : this(-1, nom, image, album)
        { }

        public Photo(int id, String nom, byte[] image, int albumId)
        {
            this.Id = id;
            this.Nom = nom;
            this.Image = image;
            this.Album = albumId;
        }
    }
}