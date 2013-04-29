using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using ClientWPF;

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

        #region GestionAlbum

        /// <summary>
        /// Ajouter un album
        /// </summary>
        /// <param name="nom">Nom de l'album à ajouter</param>
        /// <param name="numUtilisateur">Numero de l'utilisateur, doit exister dans la base de donnée Utilisateur</param>
        /// <returns>Numero d'ajout de l'album</returns>
        public int addAlbum(String nom, int numUtilisateur)
        {
            try
            {
                bdd.connexion();
                String sql = "INSERT INTO Album (nom, utilisater) " + "VALUES(@nom, @utilisateur)";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@nom", SqlDbType.VarChar, nom.Length).Value = nom;
                oCommand.Parameters.Add("@utilisateur", SqlDbType.Int).Value = numUtilisateur;
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
        /// Ajouter un album a la base de donnée, le numero de l'abum est automatiquement modifié
        /// </summary>
        /// <param name="alb"></param>
        /// <returns></returns>
        public Album addAlbum(Album alb)
        {
            int id = addAlbum(alb.Nom, alb.User.Id);
            alb.Id = id;
            return alb;
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
        public List<int> getAlbums(int idUser)
        {
            List<int> tableID = new List<int>();
            try
            {
                bdd.connexion();

                String sql = "SELECT id FROM Album WHERE utilisater = @utilisateur";

                SqlCommand oCommand = bdd.executeSQL(sql);
                oCommand.Parameters.Add("@utilisateur", SqlDbType.Int).Value = idUser;

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    tableID.Add((int)myReader.GetInt32(0));
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

        #endregion

        #region Utilitaire
        /// <summary>
        /// Retourner l'image presente dans la base de donnée
        /// L'image est identifiée par son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Bitmap getImageBDD(int ID)
        {
            byte[] bimg = getImageByte(ID);
            return new Bitmap(BytesToBitmapPhoto(bimg));
        }

        /// <summary>
        /// Convertir un image en tableau de byte
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        /// <summary>
        /// Convertir un tableau de byte en image
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        /// <summary>
        /// Sauvegarder une image dans un fichier temporaire
        /// </summary>
        /// <param name="image"></param>
        /// <returns>Nom de l'image créer</returns>
        public static string saveImage(Bitmap image)
        {
            String name = Path.GetTempFileName();
            try
            {
                image.Save(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return name;
        }

        /// <summary>
        /// Sauvegarde l'image avec le nom path
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path">Chemin contenant le nom de l'image</param>
        public static void saveImage(Bitmap image, string path)
        {
            try
            {
                image.Save(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

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
            int id = addImage(p.nom, p.blob, p.alb.Id);
            p.id = id;
            return p;
        }

        /// <summary>
        /// Retourne les photos ayant l'id = imageID
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        private ImageObjet getPhotoID(int imageID)
        {
            String nom = "";
            byte[] blob = { 0, 1 };
            int size = 0;

            try
            {
                // connexion au serveur
                bdd.connexion();

                // construit la requête
                SqlCommand getImage = new SqlCommand("SELECT id, nom, size, blob " + "FROM Image " + "WHERE id = @id", bdd.oConnection);
                getImage.Parameters.Add("@id", SqlDbType.Int).Value = imageID;

                Console.WriteLine("Image " + imageID);

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

            ImageObjet o = new ImageObjet(nom.Trim(), blob);
            o.setId(imageID);

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
        /// Ajouter l'image a la base de donnée
        /// </summary>
        /// <param name="path">Chemin vers l'image sur le disque dur</param>
        /// <param name="ID"></param>
        public void addImageBDD(string path, String nom, int numAlbum)
        {
            Bitmap bit = new Bitmap(path);
            addImage(nom, ImageToByte(bit), numAlbum);
        }

        /// <summary>
        /// Afficher le nom de toutes les images
        /// </summary>
        public void displayAllImages()
        {
            try
            {
                bdd.connexion();
                String sql = "SELECT * FROM Image";
                SqlCommand oCommand = bdd.executeSQL(sql);

                Console.WriteLine("****** Images de la base de données ********");
                Console.WriteLine("ID" + "\t" + "size " + "\t" + "album" + "\t");

                SqlDataReader myReader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                while (myReader.Read())
                {
                    Console.Write(myReader.GetValue(1) + "\t");
                    Console.Write(myReader.GetValue(2) + "\t");
                    Console.WriteLine(myReader.GetValue(4));
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
        /// Recuperer les images d'un utilisateur sous forme d'une photo
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public ImageCollection getPhotoUser(int idUser)
        {
            List<int> idImages = getImageIDUser(idUser);
            ImageCollection retour = new ImageCollection();

            foreach (int id in idImages)
            {
                retour.Add(getPhotoID(id));
            }

            return retour;
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

        /// <summary>
        /// Lit et retourne le contenu du fichier sous la forme de tableau de byte
        /// </summary>
        /// <param name="chemin">chemin du fichier</param>
        /// <returns></returns>
        public static byte[] lireFichier(string chemin)
        {
            byte[] data = null;
            try
            {
                FileInfo fileInfo = new FileInfo(chemin);
                int nbBytes = (int)fileInfo.Length;
                FileStream fileStream = new FileStream(chemin, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fileStream);
                data = br.ReadBytes(nbBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return data;
        }

        #endregion
    }
}
