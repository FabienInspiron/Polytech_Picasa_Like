using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LibrairieServeur;
using ObjetDefinition;
using System.IO;

namespace ServeurWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service" à la fois dans le code, le fichier svc et le fichier de configuration.
    public class Service :IService
    {
        private GestionBDD gestionBdd;

        public Service()
        {
            DataBase bdd = new DataBase();
            gestionBdd = new GestionBDD(bdd);
        }

        public Utilisateur Connexion(string pseudo, string mdp)
        {
            return gestionBdd.getUser(pseudo, mdp);
        }

        public Utilisateur Inscription(Utilisateur u)
        {
            return gestionBdd.addUser(u);
        }

        public List<Album> GetAlbumCollection(int userId)
        {
            List<Album> l = gestionBdd.getAlbums(userId);
            Console.WriteLine("NB album: {0}", l.Count);
            return gestionBdd.getAlbums(userId);
        }

        public List<ImageInfo> GetPicturesFromUserAlbum(int userId, int albumId)
        {
            return gestionBdd.getImagesFromAlbum(userId, albumId);
        }

        public List<int> GetPicturesIdFromUserAlbum(int userId, int albumId)
        {
            return gestionBdd.getImagesIdFromAlbum(userId, albumId);
        }

        public List<Tuple<int, String>> GetPicturesAlbumTuple(int userId, int albumId)
        {
            return gestionBdd.getImageIDUserTuple(userId, albumId);
        }

        public ImageDownloadResponse GetPicture(ImageDownloadRequest p)
        {
            ImageDownloadResponse imdr = new ImageDownloadResponse();
            imdr.ImageData = new MemoryStream(gestionBdd.getPhoto(p.ImageInfo.Id));
            return imdr;
        }

        public ImageDownloadResponse GetPictureThumbnail(ImagethumbnailDownloadRequest p)
        {
            ImageDownloadResponse imdr = new ImageDownloadResponse();
            imdr.ImageData = new MemoryStream(Util.CreateThumbnail(gestionBdd.getPhoto(p.ImageInfo.Id), p.MaxLargestSide));
            return imdr;
        }

        public void AddPicture(Picture p)
        {
            gestionBdd.addImage(p);
        }

        public void RemovePicture(int userId, int albumId, int photoId)
        {
            throw new NotImplementedException();
        }

        public Album AddAlbum(Album a)
        {
            return gestionBdd.addAlbum(a);
        }

        public void RemoveAlbum(int userId, int albumId)
        {
            throw new NotImplementedException();
        }

    }
}
