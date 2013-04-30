using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ObjetDefinition
{
    public class Photo
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public byte[] Image { get; set; }
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