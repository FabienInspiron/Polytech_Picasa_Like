﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace PicasaLike
{
    public class ImageBDD
    {

        DataBase bdd;

        /// <summary>
        /// Constructeur normal
        /// </summary>
        /// <param name="db"></param>
        public ImageBDD(DataBase db)
        {
            this.bdd = db;
        }

        /// <summary>
        /// Ajouter une image dans la base de donnée
        /// </summary>
        /// <param name="imageID"></param>
        /// <param name="image"></param>
        private void addImage(String imageID, byte[] image)
        {
            try
            {
                // connexion au serveur
                bdd.connexion();

                // construit la requête
                SqlCommand ajoutImage = new SqlCommand("INSERT INTO Image (id, blob, size) " + "VALUES(@id, @blob, @size)", bdd.oConnection);
                ajoutImage.Parameters.Add("@id", SqlDbType.VarChar, imageID.Length).Value = imageID;
                ajoutImage.Parameters.Add("@blob", SqlDbType.Image, image.Length).Value = image;
                ajoutImage.Parameters.Add("@size", SqlDbType.Int).Value = image.Length;

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
        }

        /// <summary>
        ///  récupération d'une image de la base à l'aide d'un DataReader
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        private byte[] getImage(String imageID)
        {
            byte[] blob = null;
            try
            {
                // connexion au serveur
                bdd.connexion();

                // construit la requête
                SqlCommand getImage = new SqlCommand("SELECT id,size, blob " + "FROM Image " + "WHERE id = @id", bdd.oConnection);
                getImage.Parameters.Add("@id", SqlDbType.VarChar, imageID.Length).Value = imageID;

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
        private static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// Convertir un un tableau de Byte en image BMP
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        private static Bitmap BytesToBitmap(byte[] byteArray)
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
        /// <param name="path"></param>
        /// <param name="ID"></param>
        public void addImageBDD(string path, string ID)
        {
            Bitmap bit = new Bitmap(path);
            addImage(ID, ImageToByte(bit));
        }

        /// <summary>
        /// Retourner l'image presente dans la base de donnée
        /// L'image est identifiée par son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Bitmap getImageBDD(string ID)
        {
            byte[] bimg = getImage(ID);
            return new Bitmap(BytesToBitmap(bimg));
        }

        /// <summary>
        /// Sauvegarder une image dans un fichier temporaire
        /// </summary>
        /// <param name="image"></param>
        /// <returns>Nom de l'image créer</returns>
        public string saveImage(Bitmap image)
        {
            String name = Path.GetTempFileName();
            image.Save(name);
            return name;
        }

        /// <summary>
        /// Sauvegarde l'image avec le nom path
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path">Chemin contenant le nom de l'image</param>
        public void saveImage(Bitmap image, string path)
        {
            image.Save(path);
        }
    }
}
