using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AdminPicasaLike
{
    class GestionBDD
    {
        
        public DataBase bdd;

        /// <summary>
        /// Constructeur normal
        /// </summary>
        /// <param name="db"></param>
        public GestionBDD(DataBase db)
        {
            this.bdd = db;
        }

        /// <summary>
        /// Ajouter un album
        /// </summary>
        /// <param name="nom">Nom de l'album à ajouter</param>
        /// <param name="numUtilisateur">Numero de l'utilisateur, doit exister dans la base de donnée Utilisateur</param>
        /// <returns>Numero d'ajout de l'album</returns>
        public int addAlbum(String nom, String numUtilisateur)
        {
            try
            {
                bdd.connexion();
                String sql = "INSERT INTO Album (nom, utilisater) " + "VALUES(@nom, @utilisateur)";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@nom", SqlDbType.VarChar, nom.Length).Value = nom;
                oCommand.Parameters.Add("@utilisateur", SqlDbType.Int, numUtilisateur.Length).Value = numUtilisateur;
                oCommand.ExecuteNonQuery();

                bdd.deconnect();

                return getIdentCurrent("Album");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                bdd.deconnect();
                return 0;
            }
        }

        /// <summary>
        /// Recupère la valeur de l'identifiant automatique de table
        /// </summary>
        /// <param name="table">Table a laquelle on recupere l'identifiant</param>
        /// <returns></returns>
        private int getIdentCurrent(String table)
        {
            bdd.connexion();

            String sql2 = "SELECT IDENT_CURRENT('"+table+"');";
            SqlCommand oCommand2 = bdd.executeSQL(sql2);
            oCommand2.ExecuteNonQuery();
            SqlDataReader myReader = oCommand2.ExecuteReader(CommandBehavior.SequentialAccess);
            myReader.Read();
            int id = (int)myReader.GetDecimal(0);

            bdd.deconnect();
            return id;
        }

        /// <summary>
        /// Afficher tous les albums present dans la base de donnée
        /// </summary>
        public void displayAllAlbum()
        {
            try
            {
                bdd.connexion();
                String sql = "SELECT * FROM Album";
                SqlCommand oCommand = bdd.executeSQL(sql);

                Console.WriteLine("****** Album de la base de données ********");
                Console.WriteLine("ID" + "\t" + "Nom " + "\t" + "Utilisateur" + "\t");

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    Console.Write(myReader.GetValue(0) + "\t");
                    Console.Write(myReader.GetValue(1) + "\t");
                    Console.WriteLine(myReader.GetValue(2));
                }

                myReader.Close();

                oCommand.ExecuteNonQuery();
                bdd.deconnect();
            }
            catch (Exception e)
            {
                bdd.deconnect();
                Console.WriteLine("Impossible d'afficher tous les albums : " + e.Message);
            }
        }

        /// <summary>
        /// Suppresion de l'album
        /// </summary>
        /// <param name="id">Numero de l'album à supprimer, Attention cela supprime toutes les photos qui s'y trouve</param>
        public void delAlbum(String id)
        {
            bdd.connexion();
            String sql = "DELETE FROM Album WHERE id=@id";
            SqlCommand oCommand = bdd.executeSQL(sql);
            oCommand.Parameters.Add("@id", SqlDbType.VarChar, id.Length).Value = id;

            oCommand.ExecuteNonQuery();
            Console.WriteLine("Album supprimé de la base");
            bdd.deconnect();
        }

        /// <summary>
        /// Ajouter un utilisateur dans la base de donnée
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="mdp"></param>
        /// <returns>Numero d'ajout de l'utilisateur</returns>
        public int addUser(String nom, String prenom, String mdp)
        {
            try
            {
                bdd.connexion();

                String sql = "INSERT INTO Utilisateur (nom, prenom, mdp) VALUES(@nom, @prenom, @mdp)";
                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@nom", SqlDbType.VarChar, nom.Length).Value = nom;
                oCommand.Parameters.Add("@prenom", SqlDbType.VarChar, nom.Length).Value = prenom;
                oCommand.Parameters.Add("@mdp", SqlDbType.VarChar, nom.Length).Value = mdp;

                oCommand.ExecuteNonQuery();
                Console.WriteLine("Utilisateur ajouter a la base");
                bdd.deconnect();

                return getIdentCurrent("Utilisateur");
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Impossible d'ajouter un utilisateur : " + e.Message);
                return 0;
            }
        }

        /// <summary>
        /// Supprimer l'utilsiateur de la base de donnée
        /// Cette suppression entraine la suppression de tous ses albums
        /// </summary>
        /// <param name="id"></param>
        public void delUser(String id)
        {
            bdd.connexion();
            String sql = "DELETE FROM Utilisateur WHERE id=@id";
            SqlCommand oCommand = bdd.executeSQL(sql);
            oCommand.Parameters.Add("@id", SqlDbType.Int, id.Length).Value = id;

            oCommand.ExecuteNonQuery();

            Console.WriteLine("Utilisateur supprimé de la base");
            bdd.deconnect();
        }

        /// <summary>
        /// Optenez la liste de tous les utilisateurs de la base de donnée
        /// </summary>
        public void displayAllUser()
        {
            try
            {
                bdd.connexion();
                String sql = "SELECT * FROM Utilisateur";
                SqlCommand oCommand = bdd.executeSQL(sql);

                Console.WriteLine("****** Utilisateurs de la base de données ********");
                Console.WriteLine("ID" + "\t" + "Nom " + "\t" + "Prenom" + "\t");

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    Console.Write(myReader.GetValue(0) + "\t");
                    Console.Write(myReader.GetValue(1) + "\t");
                    Console.WriteLine(myReader.GetValue(2));
                }

                myReader.Close();

                oCommand.ExecuteNonQuery();
                bdd.deconnect();
            }
            catch (Exception e)
            {
                bdd.deconnect();
                Console.WriteLine("Impossible d'afficher tous les utilisateurs : " + e.Message);
            }
        }

        /// <summary>
        /// Retourne l'identifiant de la personne se trouvant dans la base de donnée
        /// Attention le coucle nom/prenom doit être unique
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int getUserID(String nom, String prenom)
        {
            int id = 0;

            try
            {
                bdd.connexion();

                // construit la requête
                SqlCommand getImage = new SqlCommand("SELECT * " + "FROM Utilisateur " + "WHERE nom = @name AND prenom= @prenom", bdd.oConnection);
                getImage.Parameters.Add("@name", SqlDbType.VarChar, nom.Length).Value = nom;
                getImage.Parameters.Add("@prenom", SqlDbType.VarChar, prenom.Length).Value = prenom;

                // exécution de la requête et création du reader
                SqlDataReader myReader = getImage.ExecuteReader(CommandBehavior.SequentialAccess);
                if (myReader.Read())
                {
                    id = (int)myReader.GetSqlValue(0);
                }

                bdd.deconnect();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return id;
        }
    }
}
