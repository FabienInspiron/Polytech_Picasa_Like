using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ClientWPF
{
    public class AlbumCollection : ObservableCollection<ClientWPF.WebService.Album>
    { }

    public class ImageCollection : ObservableCollection<Photo>
    { }

    public class Photo
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public int Album { get; set; }
        public byte[] Image { get; set; }

        public Photo(String nom)
            : this(-1, nom, -1)
        { }

        public Photo(String nom, int album)
            : this(-1, nom, album)
        { }

        public Photo(int id, String nom, int albumId)
        {
            this.Id = id;
            this.Nom = nom;
            this.Album = albumId;
        }
    }
}
