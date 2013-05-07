using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;

using ObjetDefinition;

namespace LibrairieServeur
{
    public class GestionBDD
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

        #region GestionAlbum

        /// <summary>
        /// Ajouter un album
        /// </summary>
        /// <param name="nom">Nom de l'album à ajouter</param>
        /// <param name="numUtilisateur">Numero de l'utilisateur, doit exister dans la base de donnée Utilisateur</param>
        /// <returns>Numero d'ajout de l'album</returns>
        public int addAlbum(String nom, int numUtilisateur)
        {
            int res = -1;
            try
            {
                bdd.connexion();
                String sql = "INSERT INTO Album (nom, utilisater) VALUES(@nom, @utilisateur)";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@nom", SqlDbType.VarChar, nom.Length).Value = nom;
                oCommand.Parameters.Add("@utilisateur", SqlDbType.Int).Value = numUtilisateur;
                oCommand.ExecuteNonQuery();

                res = getIdentCurrent("Album");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally {
                bdd.deconnect();
            }

            return res;
        }



        /// <summary>
        /// Ajouter un album a la base de donnée, le numero de l'abum est automatiquement modifié
        /// </summary>
        /// <param name="alb"></param>
        /// <returns></returns>
        public Album addAlbum(Album alb)
        {
            alb.Id = addAlbum(alb.Nom, alb.UserId);
            return alb;
        }

        /// <summary>
        /// Afficher tous les albums present dans la base de donnée
        /// </summary>
        public List<Album> getAllAlbum()
        {
            List<Album> listAlb = new List<Album>();
            try
            {
                bdd.connexion();
                String sql = "SELECT * FROM Album";
                SqlCommand oCommand = bdd.executeSQL(sql);

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    int id = (int) myReader.GetInt32(0);
                    String nom = myReader.GetString(1);
                    int ut = (int) myReader.GetInt32(2);

                    Album alb = new Album(id, nom, ut);
                    listAlb.Add(alb);
                }

                myReader.Close();
            }
            catch (Exception e)
            {
                
                Console.WriteLine("Impossible d'afficher tous les albums : " + e.Message);
            }
            finally
            {
                bdd.deconnect();
            }
            return listAlb;
        }

        /// <summary>
        /// Suppresion de l'album
        /// </summary>
        /// <param name="id">Numero de l'album à supprimer, Attention cela supprime toutes les photos qui s'y trouve</param>
        public void delAlbum(int id)
        {
            bdd.connexion();
            String sql = "DELETE FROM Album WHERE id=@id";
            SqlCommand oCommand = bdd.executeSQL(sql);
            oCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
            oCommand.ExecuteNonQuery();
            Console.WriteLine("Album supprimé de la base");
            bdd.deconnect();
        }


        /// <summary>
        /// Supprimer un album de la base de donnée
        /// </summary>
        /// <param name="a">Album a supprimer</param>
        public void delAlbum(Album a)
        {
            delAlbum(a.Id);
        }

        /// <summary>
        /// Retourne les albums de l'utilisateur
        /// </summary>
        /// <param name="idUser">Utilisateur connecté auquel on cherche l'album</param>
        /// <returns>Liste des albums de cet utilisateur</returns>
        public List<Album> getAlbums(int idUser)
        {
            List<Album> albums = new List<Album>();
            try
            {
                bdd.connexion();

                String sql = "SELECT * FROM Album WHERE utilisater = @utilisateur";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@utilisateur", SqlDbType.Int).Value = idUser;

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    albums.Add(new Album(myReader.GetInt32(0), myReader.GetString(1).Trim(), myReader.GetInt32(2)));
                }

                myReader.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                bdd.deconnect();
            }

            return albums;
        }

        /// <summary>
        /// Recuperer l'album d'id idAlbum
        /// </summary>
        /// <param name="idAlbum"></param>
        /// <returns></returns>
        public Album getAlbumID(int idAlbum)
        {
            String nom = "";
            int user = 0;

            List<int> tableID = new List<int>();
            try
            {
                bdd.connexion();

                String sql = "SELECT * FROM Album WHERE id = @alb";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@alb", SqlDbType.Int).Value = idAlbum;

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    nom = myReader.GetString(1).Trim();
                    user = myReader.GetInt32(2);
                }

                myReader.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                bdd.deconnect();
            }

            return new Album(idAlbum, nom);
        }

        #endregion

        #region Utilitaire
        /// <summary>
        ///  récupération d'une image de la base à l'aide d'un DataReader
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        public byte[] getImageByte(int imageID)
        {
            byte[] blob = null;
            try
            {
                // connexion au serveur
                bdd.connexion();

                // construit la requête
                SqlCommand getImage = new SqlCommand("SELECT id, size, blob " + "FROM Image " + "WHERE id = @id", bdd.oConnection);
                getImage.Parameters.Add("@id", SqlDbType.VarChar).Value = imageID;

                // exécution de la requête et création du reader
                SqlDataReader myReader = getImage.ExecuteReader(CommandBehavior.SequentialAccess);
                if (myReader.Read())
                {
                    // lit la taille du blob
                    int size = myReader.GetInt32(1);
                    blob = new byte[size];
                    // récupére le blob de la BDD et le copie dans la variable blob
                    myReader.GetBytes(2, 0, blob, 0, size);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                // dans tous les cas on ferme la connexion
                bdd.deconnect();
            }
            return blob;
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

        #endregion

        #region GestionUtilisateur

        /// <summary>
        /// Méthode de connexion a la base
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="mdp">Mot de passe</param>
        /// <returns></returns>
        public Utilisateur getUser(String login, String mdp)
        {
            Utilisateur u = null;
            try
            {
                bdd.connexion();

                // construit la requête
                SqlCommand getImage = new SqlCommand("SELECT * " + "FROM Utilisateur " + "WHERE nom = @name AND mdp= @mdp", bdd.oConnection);
                getImage.Parameters.Add("@name", SqlDbType.VarChar, login.Length).Value = login;
                getImage.Parameters.Add("@mdp", SqlDbType.VarChar, mdp.Length).Value = mdp;

                // exécution de la requête et création du reader
                SqlDataReader myReader = getImage.ExecuteReader(CommandBehavior.SequentialAccess);

                if (myReader.HasRows) {
                    myReader.Read();
                    u = new Utilisateur(myReader.GetInt32(0), myReader.GetString(1).Trim(), myReader.GetString(2).Trim(), myReader.GetString(3).Trim());
                }

                bdd.deconnect();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return u;
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
        /// Ajouter un utilisateur à la base de donnée
        /// </summary>
        /// <param name="user">Utilisateur a ajouter</param>
        /// <returns>Utilisateur modifié avec un identifiant correct</returns>
        public Utilisateur addUser(Utilisateur user) 
        {
            int idUser = addUser(user.Nom, user.Prenom, user.Mdp);
            user.Id = idUser;
            return user;
        }

        /// <summary>
        /// Supprimer l'utilsiateur de la base de donnée
        /// Cette suppression entraine la suppression de tous ses albums
        /// </summary>
        /// <param name="id"></param>
        public void delUser(int id)
        {
            bdd.connexion();
            String sql = "DELETE FROM Utilisateur WHERE id=@id";
            SqlCommand oCommand = bdd.executeSQL(sql);
            oCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            oCommand.ExecuteNonQuery();

            Console.WriteLine("Utilisateur supprimé de la base");
            bdd.deconnect();
        }

        /// <summary>
        /// Suppression de l'utilisateur de la base
        /// </summary>
        /// <param name="user">Utilisateur a supprimer</param>
        public void delUser(Utilisateur user)
        {
            delUser(user.Id);
        }

        /// <summary>
        /// Afficher tous les utilisateur present dans la base de donnée
        /// </summary>
        public List<Utilisateur> getAllUser()
        {
            List<Utilisateur> listUser = new List<Utilisateur>();
            try
            {
                bdd.connexion();
                String sql = "SELECT * FROM Utilisateur";
                SqlCommand oCommand = bdd.executeSQL(sql);

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    int id = (int)myReader.GetInt32(0);
                    String nom = myReader.GetString(1);
                    String prenom = myReader.GetString(2);
                    String mdp = myReader.GetString(3);

                    Utilisateur ut = new Utilisateur(nom, prenom, mdp);
                    ut.Id = id;
                    listUser.Add(ut);
                }

                myReader.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine("Impossible d'afficher tous les albums : " + e.Message);
            }
            finally
            {
                bdd.deconnect();
            }
            return listUser;
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

        #endregion

        #region GestionImages

        /// <summary>
        /// Ajouter une image dans la base de donnée
        /// </summary>
        /// <param name="imageID"></param>
        /// <param name="image"></param>
        public int addImage(String nom, byte[] image, int numAlbum)
        {
            try
            {
                // connexion au serveur
                bdd.connexion();

                // construit la requête
                SqlCommand ajoutImage = new SqlCommand("INSERT INTO Image (nom, blob, size, album) " + "VALUES(@id, @blob, @size, @album)", bdd.oConnection);
                ajoutImage.Parameters.Add("@id", SqlDbType.VarChar, nom.Length).Value = nom;
                ajoutImage.Parameters.Add("@blob", SqlDbType.Image, image.Length).Value = image;
                ajoutImage.Parameters.Add("@size", SqlDbType.Int).Value = image.Length;
                ajoutImage.Parameters.Add("@album", SqlDbType.Int).Value = numAlbum;

                // execution de la requête
                ajoutImage.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
            finally
            {
                // dans tous les cas on ferme la connexion
                bdd.deconnect();
            }

            return getIdentCurrent("Image");
        }

        /// <summary>
        /// Ajouter une image a la base de donnée
        /// </summary>
        /// <param name="p">Image à ajouter</param>
        /// <returns></returns>
        public Photo addImage(Photo p)
        {
            int id = addImage(p.Nom, p.Image, p.Album);
            p.Id = id;
            return p;
        }

        /// <summary>
        /// Retourne les photos ayant l'id = imageID
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        private Photo getPhotoID(int imageID)
        {
            String nom = "";
            byte[] blob = { 0, 1 };
            int size = 0;
            int album = 0;

            try
            {
                // connexion au serveur
                bdd.connexion();

                // construit la requête
                SqlCommand getImage = new SqlCommand("SELECT id, nom, size, blob, album " + "FROM Image " + "WHERE id = @id", bdd.oConnection);
                getImage.Parameters.Add("@id", SqlDbType.Int).Value = imageID;

                // exécution de la requête et création du reader
                //Attention à l'acces sequential il faut lire les données dans l'ordre
                SqlDataReader myReader = getImage.ExecuteReader(CommandBehavior.SequentialAccess);
                if (myReader.Read())
                {
                    nom = myReader.GetString(1);

                    // lit la taille du blob
                    size = myReader.GetInt32(2);

                    blob = new byte[size];

                    // récupére le blob de la BDD et le copie dans la variable blob
                    myReader.GetBytes(3, 0, blob, 0, size);

                    album = myReader.GetInt32(4);
                }
            } catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
            finally
            {
                // dans tous les cas on ferme la connexion
                bdd.deconnect();
            }

            Photo o = new Photo(nom.Trim(), blob, album);
            o.Id = imageID;

            return o;
        }

        /// <summary>
        /// Supprimer une image de la base de donnée
        /// </summary>
        /// <param name="ID"></param>
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

        /// <summary>
        /// Convertir un un tableau de Byte en image BMP
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static Bitmap BytesToBitmapPhoto(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Bitmap img = (Bitmap)Image.FromStream(ms);
                return img;
            }
        }

        /// <summary>
        /// Afficher tous les utilisateur present dans la base de donnée
        /// </summary>
        public List<Photo> getAllPhoto()
        {
            List<Photo> listPhoto = new List<Photo>();
            try
            {
                bdd.connexion();
                String sql = "SELECT * FROM Photo";
                SqlCommand oCommand = bdd.executeSQL(sql);

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    int id = (int)myReader.GetInt32(0);

                    listPhoto.Add(getPhotoID(id));
                }

                myReader.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine("Impossible d'afficher tous les albums : " + e.Message);
            }
            finally
            {
                bdd.deconnect();
            }
            return listPhoto;
        }

        /// <summary>
        /// Retourne les identifiants des images des utilisateurs
        /// </summary>
        /// <param name="idUser">Utilisateur connecté auquel on cherche l'album</param>
        /// <returns>Liste des albums de cet utilisateur</returns>
        public List<int> getImageIDUser(int idUser)
        {
            List<int> tableID = new List<int>();

            try
            {
                bdd.connexion();

                String sql = "SELECT Image.id FROM Image, Album WHERE Image.album = Album.id AND Album.utilisater = @utilisateur";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@utilisateur", SqlDbType.Int).Value = idUser;

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    int res = myReader.GetInt32(0);
                    tableID.Add(res);
                    Console.WriteLine(res);
                }

                myReader.Close();

                oCommand.ExecuteNonQuery();
                bdd.deconnect();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            return tableID;
        }

        /// <summary>
        /// Retourne les identifiants des images des utilisateurs
        /// </summary>
        /// <param name="idUser">Utilisateur connecté auquel on cherche l'album</param>
        /// <returns>Liste des albums de cet utilisateur</returns>
        public List<int> getImageIDUser(int idUser, int idAlbum)
        {
            List<int> tableID = new List<int>();

            try
            {
                bdd.connexion();

                String sql = "SELECT Image.id FROM Image, Album WHERE Image.album = Album.id AND Album.utilisater = @utilisateur AND Album.id =@album";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@utilisateur", SqlDbType.Int).Value = idUser;
                oCommand.Parameters.Add("@album", SqlDbType.Int).Value = idAlbum;

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    int res = myReader.GetInt32(0);
                    tableID.Add(res);
                    Console.WriteLine(res);
                }

                myReader.Close();

                oCommand.ExecuteNonQuery();
                bdd.deconnect();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            return tableID;
        }

        public List<Photo> getPhotoUserAlbum(int idUser, int album)
        {
            List<Photo> photos = new List<Photo>();

            try
            {
                bdd.connexion();

                String sql = "SELECT Image.size, Image.blob, Image.id, Image.nom, Image.album FROM Image, Album WHERE Image.album = Album.id AND Album.utilisater = @utilisateur AND Album.id =@album";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@utilisateur", SqlDbType.Int).Value = idUser;
                oCommand.Parameters.Add("@album", SqlDbType.Int).Value = album;

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    int size = myReader.GetInt32(0);
                    byte[] blob = new byte[size];
                    // récupére le blob de la BDD et le copie dans la variable blob
                    myReader.GetBytes(1, 0, blob, 0, size);
                    photos.Add(new Photo(myReader.GetInt32(2), myReader.GetString(3).Trim(), blob, myReader.GetInt32(4)));
                }

                myReader.Close();

                //oCommand.ExecuteNonQuery();
                bdd.deconnect();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            return photos;
        }

        /// <summary>
        /// Recuperer une photo à partir de son id
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public Photo getPhoto(int photo)
        {

            String nom = "";
            byte[] blob = { 0, 1 };
            int album = 0;

            try
            {
                bdd.connexion();

                String sql = "SELECT id, nom, size, blob, album FROM Image WHERE id=@id";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@id", SqlDbType.Int).Value = photo;

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    nom = myReader.GetString(1);

                    // lit la taille du blob
                    int size = myReader.GetInt32(2);

                    blob = new byte[size];
                    // récupére le blob de la BDD et le copie dans la variable blob
                    myReader.GetBytes(3, 0, blob, 0, size);

                    album = myReader.GetInt32(4);
                }

                myReader.Close();

                //oCommand.ExecuteNonQuery();
                bdd.deconnect();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            return new Photo(photo,nom,blob,album);
        }

        /// <summary>
        /// Recuperer les images d'un utilisateur sous forme d'un tableau de Byte
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public List<byte[]> getImagesUserByte(int idUser)
        {
            List<int> idImages = getImageIDUser(idUser);
            List<byte[]> retour = new List<byte[]>();

            foreach (int id in idImages)
            {
                retour.Add(getImageByte(id));
            }

            return retour;
        }

        #endregion
    }
}