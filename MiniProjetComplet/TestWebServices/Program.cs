using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWebServices.WebService;

namespace TestWebServices
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceClient sc = new ServiceClient();

            Utilisateur ut;

            // test inscription OK
            //ut = new Utilisateur();
            //ut.Nom = "Gorrieri";
            //ut.Prenom = "Cyril";
            //ut.Mdp = "cyril";
            //ut = sc.Instription(ut);
            //if(ut == null)
            //    Console.WriteLine("Erreur à l'inscription");

            // test Connexion OK
            ut = sc.Connexion("Gorrieri", "cyril");
            if (ut == null)
                Console.WriteLine("Erreur de connexion");
            else
                Console.WriteLine("Utilisateur: Id:{0}, Nom:{1}, Prenom:{2}, Mdp:{3}", ut.Id, ut.Nom, ut.Prenom, ut.Mdp);

            // Ajout albums
            //Album a = new Album();
            //a.Nom = "album1";
            //a.UserId = ut.Id;
            //a = sc.AddAlbum(a);
            //Console.WriteLine("Album: Id:{0}, UserId:{1}, Nom:{2}", a.Id, a.UserId, a.Nom);

            //a.Nom = "album2";
            //a.UserId = ut.Id;
            //a = sc.AddAlbum(a);
            //Console.WriteLine("Album: Id:{0}, UserId:{1}, Nom:{2}", a.Id, a.UserId, a.Nom);

            Album[] alC = sc.GetAlbumCollection(ut.Id);
            Console.WriteLine("Album de {0}", ut.Nom);
            foreach (Album alb in alC)
            {
                Console.Write("{0}({1})", alb.Nom, alb.Id);
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
