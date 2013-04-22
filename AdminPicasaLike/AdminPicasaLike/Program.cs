using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPicasaLike
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase db = new DataBase();
            GestionBDD gest = new GestionBDD(db);
            ImageBDD img = new ImageBDD(db);

            gest.addUser("fabien", "fab", "1234");
            int user2 = gest.addUser("fabien1", "fab", "1234");
            int user = gest.addUser("fabien2", "fab", "1234");

            //gest.addAlbum("un album", user.ToString());
            //int numAlb = gest.addAlbum("un second album", user2.ToString());
            //img.addImageBDD(@"D:\Audi_TT-RS N&B.bmp", "tutu", numAlb.ToString());
            //img.addImageBDD(@"D:\Audi_TT-RS N&B.bmp", "tutu2", numAlb.ToString());

            //Console.WriteLine(gest.getAlbums(user).Count);
            //Console.WriteLine(img.getImagesID(user).Count);

            Console.ReadLine();
        }

        public static void printStartScreen()
        {
            Console.WriteLine("Administration de Picasa Like");
            Console.WriteLine("Choisir l'une des options suivantes : ");
            Console.WriteLine("1 - Supprimer compte utilisateur");
            Console.WriteLine("2 - Supprimer album photo");
            Console.WriteLine("3 - Supprimer photo");
            Console.ReadLine();
        }
    }
}
