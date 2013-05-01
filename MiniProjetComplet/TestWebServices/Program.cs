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
            WebService.Utilisateur ut = new WebService.Utilisateur();
            ut.Nom = "Gorrieri";
            ut.Prenom = "Cyril";
            ut.Mdp = "cyril";
            sc.instription(ut);
        }
    }
}
