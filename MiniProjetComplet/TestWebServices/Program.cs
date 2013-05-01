using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestWebServices
{
    class Program
    {
        static void Main(string[] args)
        {
            WebService.ServiceClient sc = new WebService.ServiceClient();

            // test inscription OK
            WebService.Utilisateur ut = new WebService.Utilisateur();
            ut.Nom = "Gorrieri";
            ut.Prenom = "Cyril";
            ut.Mdp = "cyril";
            ut = sc.instription(ut);
            if(ut == null)
                Console.WriteLine("Erreur à l'inscription");

            // test Connexion OK
            ut = sc.connexion("Gorrieri", "cyril");
            if (ut == null)
                Console.WriteLine("Erreur de connexion");
            else
                Console.WriteLine("Utilisateur: id:{0}, Nom:{1}, Prenom:{2}, Mdp:{3}", ut.Id, ut.Nom, ut.Prenom, ut.Mdp);

            Console.ReadLine();

        }
    }
}
