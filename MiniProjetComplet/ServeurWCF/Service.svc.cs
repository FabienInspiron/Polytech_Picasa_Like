using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LibrairieServeur;
using ObjetDefinition;

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

        public Utilisateur Instription(Utilisateur u)
        {
            return gestionBdd.addUser(u);
        }

        public List<Album> GetAlbumCollection(int userId)
        {
            List<Album> l = gestionBdd.getAlbums(userId);
            Console.WriteLine("NB album: {0}", l.Count);
            return gestionBdd.getAlbums(userId);
        }

        public List<Photo> GetPhotoAlbum(int userId, int albumId)
        {
            return gestionBdd.getPhotoUserAlbum(userId, albumId);
        }

        public void AddPhoto(Photo p)
        {
            gestionBdd.addImage(p);
        }

        public void RemovePhoto(int userId, int albumId, int photoId)
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
