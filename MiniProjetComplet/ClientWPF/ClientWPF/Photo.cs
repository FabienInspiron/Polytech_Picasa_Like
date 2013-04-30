using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPicasaLike
{
    class Photo
    {
        public int id;
        public string nom;
        public byte[] blob;
        public int size;
        public Album alb;

        public Photo(string nom, byte[] blob, int size, Album alb)
        {
            this.id = 0;
            this.nom = nom;
            this.blob = blob;
            this.size = size;
            this.alb = alb;
        }

        public Photo(String nom, byte[] blob, int size)
        {
            this.id = 0;
            this.nom = nom;
            this.blob = blob;
            this.size = size;
            this.alb = null;
        }
    }
}