using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace ClientWPF
{
    public class ImageObjet
    {
        public int id { get; set; }
        public String Nom { get; set; }
        public byte[] Image { get; set; }
        public int album;

        public ImageObjet(String Nom, byte[] Image)
        {
            id= 0;
            this.Nom = Nom;
            this.Image = Image;
            album = 0;
        }

        public ImageObjet(String Nom, byte[] Image, int album)
        {
            id = 0;
            this.Nom = Nom;
            this.Image = Image;
            this.album = album;
        }

        public void setId(int id)
        {
            this.id = id;
        }
    }

    public class ImageCollection : ObservableCollection<ImageObjet>
    { }
}