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

        public Photo(String nom, byte[] image) : this(nom,image,-1)
        {}

        public Photo(String nom, byte[] image, int album)
        {
            this.Id = -1;
            this.Nom = nom;
            this.Image = image;
            this.Album = album;
        }
    }

    public class PhotoCollection : ObservableCollection<Photo>
    { }
}