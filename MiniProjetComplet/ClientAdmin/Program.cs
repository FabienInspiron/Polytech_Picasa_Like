using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientAdmin.ServiceReference1;
using ClientAdmin.ServiceReferenceAdmin;

namespace ClientAdmin
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean continuer = true;

            while (continuer)
            {
                int actio = getInt(afficherPromptNiveau1());
                int donnee = getInt(afficherPromptNiveau2());
                actions(actio, donnee);

                String Scontinuer = getString("Voulez vous continuer ? (o/n) : ");

                if (!Scontinuer.Equals("o"))
                    continuer = false;
            }
        }

        public static string afficherPromptNiveau1()
        {
            String prompt = " ### Bienvenu dans le client admin ### \n";
            prompt += "Veuillez choisir une action : \n";
            prompt += "1 - Ajouter \n";
            prompt += "2 - Modifier \n";
            prompt += "3 - Supprimer \n";
            prompt += "4 - Afficher \n";

            return prompt;
        }

        public static string afficherPromptNiveau2()
        {
            String prompt = "Sur quelle donnée : \n";
            prompt += "1 - Utilisateur \n";
            prompt += "2 - Album \n";
            prompt += "3 - Photo \n";

            return prompt;
        }

        public static string afficherPromptNiveau3()
        {
            String prompt = "donnée 1 : ";

            return prompt;
        }

        /// <summary>
        /// Recuperation d'un entier entré au clavier
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int getInt(String message)
        {
            Console.Write(message);

            Boolean repeat = true;
            int donnee = 0;
            while (repeat)
            {
                string Sdonnee = Console.ReadLine();
                try
                {
                    donnee = int.Parse(Sdonnee);
                    repeat = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Impossible de recuperer le numero de l'action \n");
                    Console.WriteLine(afficherPromptNiveau1());
                }
            }

            return donnee;
        }

        /// <summary>
        /// Recuperation de l'entrée clavier de l'utilisateur
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static String getString(String message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public static int toInt(String mes)
        {
            try
            {
                return int.Parse(mes);
            }
            catch (Exception e)
            {
                Console.WriteLine("Mauvaise entrée");
            }

            return -1;
        }

        /// <summary>
        /// Faire une action en fonction des données
        /// </summary>
        /// <param name="action"></param>
        /// <param name="donee"></param>
        public static void actions(int action, int donnee)
        {
            ServiceClient serviceClient = new ServiceClient();
            ServiceAdminClient serviceAdmin = new ServiceAdminClient();

            switch (action)
            {
                case 1:
                    switch (donnee)
                    {
                        case 1:
                            ClientAdmin.ServiceReference1.Utilisateur u = new ClientAdmin.ServiceReference1.Utilisateur();
                            u.Nom = getString("Nom : ");
                            u.Prenom = getString("Prenom : ");
                            u.Mdp = getString("Mot de passe : ");

                            ServiceReference1.Utilisateur ret = serviceClient.Inscription(u);

                            if (ret == null) Console.WriteLine("Inscription impossible");
                            else Console.WriteLine("Inscription reussite : " + ret.Id);
                            break;
                        case 2:
                            ServiceReference1.Album albu = new ServiceReference1.Album();
                            albu.Nom = getString("Nom : ");
                            albu.UserId = getInt("ID Utilisateur : ");

                            ServiceReference1.Album a = serviceClient.AddAlbum(albu);

                            if (a == null) Console.WriteLine("Ajout impossible");
                            else Console.WriteLine("Album ajouté : " + a.Id);
                            break;
                        case 3:
                            break;
                    }
                break;
                case 2:
                    switch (donnee)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                break;
                case 3:
                    switch (donnee)
                    {
                        case 1:
                            int idUserToDelete = getInt("Numero de l'utilisateur : ");
                            serviceAdmin.delUtilisateur(idUserToDelete);
                            break;
                        case 2:
                            int idUserToDelete2 = getInt("Numero de l'utilisateur : ");
                            int idAlbumToDelete = getInt("Numero de l'album : ");
                            serviceClient.RemoveAlbum(idUserToDelete2, idAlbumToDelete);
                            break;
                        case 3:
                            int idUserToDelete3 = getInt("Numero de l'utilisateur : ");
                            int idAlbumToDelete3 = getInt("Numero de l'album : ");
                            int idPhotoToDelete3 = getInt("Numero de la photo : ");
                            serviceClient.RemovePhoto(idUserToDelete3, idAlbumToDelete3, idPhotoToDelete3);
                            break;
                    }
                break;
                
            case 4:
                
            switch (donnee)
            {
                case 1:
                    ClientAdmin.ServiceReferenceAdmin.Utilisateur[] userList = serviceAdmin.getAllUsers();
                    foreach (ClientAdmin.ServiceReferenceAdmin.Utilisateur u in userList)
                        Console.WriteLine(u.Id + " - " + u.Nom);
                    break;
                case 2:
                    ClientAdmin.ServiceReferenceAdmin.Album[] albums = serviceAdmin.getAllAlbums();
                    foreach (ClientAdmin.ServiceReferenceAdmin.Album u in albums)
                        Console.WriteLine(u.Id + " - " + u.Nom);
                    break;
                case 3:
                    ClientAdmin.ServiceReferenceAdmin.Photo[] photos = serviceAdmin.getAllPhoto();
                    foreach (ClientAdmin.ServiceReferenceAdmin.Photo u in photos)
                        Console.WriteLine(u.Id + " - " + u.Nom);
                    break;
            }
            break;
            }
        }
    }
}
