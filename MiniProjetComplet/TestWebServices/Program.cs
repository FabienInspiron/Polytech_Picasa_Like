using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWebServices.WebService;
using System.IO;

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

            // Ajout de photos
            ImageInfo pi = new ImageInfo();
            pi.Album = alC[0].Id;
            pi.Nom = "phot1.jpg";
            sc.AddPicture(pi, new FileStream("image1.jpg", FileMode.Open));

            //p.Nom = "photo2.jpg";
            //p.Image = Util.lireFichier("image2.jpg");
            //sc.AddPhoto(p);

            ImageInfo[] photos = sc.GetPicturesFromUserAlbum(ut.Id, alC[0].Id);
            foreach (ImageInfo ph in photos)
            {
                Stream p = sc.GetPicture(ph);
                int b, i = 0;
                List<byte> bytes = new List<byte>();
                do
                {
                    b = p.ReadByte();
                    bytes.Add((byte)b); //read next byte from stream  
                    i++;
                } while (b != -1);
                Console.WriteLine("Photo {0}, length:{1}", ph.Nom, i);

                Util.ByteArrayToFile(ph.Nom, bytes.ToArray());
            }

            Console.ReadLine();
        }
    }
}
