using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPicasaLike
{
    class Photo
    {
        public int id;
        public String nom;
        public byte[] blob;
        public int size;
        public Album alb;

        Photo(String nom, byte[] blob, int size, Album alb)
        {
            this.id = 0;
            this.nom = nom;
            this.blob = blob;
            this.size = size;
            this.alb = alb;
        }
    }
}