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

        public ImageObjet(String Nom, byte[] Image)
        {
            id= 0;
            this.Nom = Nom;
            this.Image = Image;
        }

        public void setId(int id)
        {
            this.id = id;
        }
    }

    public class ImageCollection : ObservableCollection<ImageObjet>
    { }
}