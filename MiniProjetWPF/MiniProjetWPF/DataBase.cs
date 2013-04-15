using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PicasaLike
{
    class DataBase
    {

        String connexionStr;
        public SqlConnection oConnection;

        public DataBase()
        {
            this.connexionStr = "Server=GORRIERI;Database=Miniprojet;Integrated Security=true;";
        }

        // Constructeur normal
        public DataBase(String user, String data)
        {
            this.connexionStr = "Server=" + user + ";Database=" + data + ";Integrated Security=true;";
        }

        // Constructeur normal
        public DataBase(String connexion_str)
        {
            this.connexionStr = connexion_str;
        }

        public void connexion()
        {
            try
            {
                oConnection = new SqlConnection(connexionStr);
                oConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Impossible de se connecter à la base de donnée : " + e.Message);
            }
        }

        public void deconnect()
        {
            try
            {
                oConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Impossible de se deconnecter à la base de donnée : " + e.Message);
            }
        }

        public SqlCommand executeSQL(string requete)
        {
            return new SqlCommand(requete, oConnection);
        }

        // Exemple de lecture de données un DataReader
        static void Main_old(string[] args)
        {
            DataBase db = new DataBase();
            db.connexion();
            SqlCommand oCommand = db.executeSQL("SELECT * from Etudiant");
            SqlDataReader oReader = null;
            try
            {
                oReader = oCommand.ExecuteReader();
                Console.WriteLine("\t{0}\t{1}\t{2}", oReader.GetName(0
                       ), oReader.GetName(1), oReader.GetName(2));
                // lecture de chaque ligne du DataReader et écriture sur la console
                while (oReader.Read())
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}", oReader.GetInt32(0), oReader.GetString(1), oReader.GetString(2));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            db.deconnect();
        }
    }
}
