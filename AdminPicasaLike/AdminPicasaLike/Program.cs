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
            gest.addUser("fabien1", "fab", "1234");
            gest.addUser("fabien2", "fab", "1234");

            gest.displayAllUser();

            Console.WriteLine(gest.addAlbum("un album", "45"));

            gest.delUser("44");

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
