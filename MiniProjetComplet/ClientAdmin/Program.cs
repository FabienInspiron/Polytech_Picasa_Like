using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientAdmin
{
    class Program
    {
        static void Main(string[] args)
        {
            int action = getInt(afficherPromptNiveau1());
            int donnee = getInt(afficherPromptNiveau2());
        }

        public static string afficherPromptNiveau1()
        {
            String prompt = " ### Bienvenu dans le client admin ### \n";
            prompt += "Veuillez choisir une action : \n";
            prompt += "1 - Ajouter \n";
            prompt += "2 - Modifier \n";
            prompt += "3 - Supprimer \n";

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
            Console.WriteLine(message);

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
        /// Faire une action en fonction des données
        /// </summary>
        /// <param name="action"></param>
        /// <param name="donee"></param>
        public static void action(int action, int donnee)
        {
            ServiceReference1.ServiceClient service = new ServiceReference1.ServiceClient();

            switch (action)
            {
                case 1:
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
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                break;
            }
        }
    }
}
