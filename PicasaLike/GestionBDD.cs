using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace PicasaLike
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
        /// Ajouter un album à la liste des album present dans la base de donnée
        /// </summary>
        /// <param name="nom"></param>
        public void addAlbum(String nom)
        {
            bdd.connexion();
            String sql = "INSERT INTO Album (id, nom, utilisateur) " + "VALUES(null, @nom, @utilisateur)";
            SqlCommand oCommand = bdd.executeSQL(sql);
            oCommand.Parameters.Add("@nom", SqlDbType.VarChar, nom.Length).Value = nom;
            bdd.deconnect();
        }

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
        public void addUser(String nom, String prenom, String mdp)
        {
            bdd.connexion();
            String sql = "INSERT INTO user (id, nom, prenom, mdp) " + "VALUES(null, @nom, @prenom, @mdp)";
            SqlCommand oCommand = bdd.executeSQL(sql);
            oCommand.Parameters.Add("@nom", SqlDbType.VarChar, nom.Length).Value = nom;
            oCommand.Parameters.Add("@prenom", SqlDbType.VarChar, nom.Length).Value = prenom;
            oCommand.Parameters.Add("@mdp", SqlDbType.VarChar, nom.Length).Value = mdp;

            oCommand.ExecuteNonQuery();
            Console.WriteLine("Utilisateur ajouter a la base");
            bdd.deconnect();
        }

        /// <summary>
        /// Supprimer l'utilsiateur de la base de donnée
        /// Cette suppression entraine la suppression de tous ses albums
        /// </summary>
        /// <param name="id"></param>
        public void delUser(String id)
        {
            bdd.connexion();
            String sql = "DELETE FROM user WHERE id=@id";
            SqlCommand oCommand = bdd.executeSQL(sql);
            oCommand.Parameters.Add("@id", SqlDbType.VarChar, id.Length).Value = id;

            oCommand.ExecuteNonQuery();
            Console.WriteLine("Utilisateur supprimé de la base");
            bdd.deconnect();
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
            bdd.connexion();

            // construit la requête
            SqlCommand getImage = new SqlCommand("SELECT id " + "FROM user " + "WHERE nom = @name AND prenom= @prenom", bdd.oConnection);
            getImage.Parameters.Add("@name", SqlDbType.VarChar, nom.Length).Value = nom;
            getImage.Parameters.Add("@prenom", SqlDbType.VarChar, prenom.Length).Value = prenom;

            // exécution de la requête et création du reader
            SqlDataReader myReader = getImage.ExecuteReader(CommandBehavior.SequentialAccess);
            if (myReader.Read())
            {
                id = (int)myReader.GetInt64(1);
            }

            bdd.deconnect();

            return id;
        }

        public void delImage(String ID)
        {
            bdd.connexion();
            String sql = "DELETE FROM Image WHERE id=@id";
            SqlCommand oCommand = bdd.executeSQL(sql);
            oCommand.Parameters.Add("@id", SqlDbType.VarChar, ID.Length).Value = ID;

            oCommand.ExecuteNonQuery();
            Console.WriteLine("Image supprimée de la base");
            bdd.deconnect();
        }
    }
}
