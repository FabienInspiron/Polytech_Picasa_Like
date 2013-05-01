using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LibrairieServeur;

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

        public ObjetDefinition.Utilisateur connexion(string pseudo, string mdp)
        {
            return gestionBdd.getUser(pseudo, mdp);
        }

        public ObjetDefinition.Utilisateur instription(ObjetDefinition.Utilisateur u)
        {
            return gestionBdd.addUser(u);
        }

        public ObjetDefinition.AlbumCollection getAlbumCollection(int userId)
        {
            throw new NotImplementedException();
        }

        public ObjetDefinition.PhotoCollection getPhotoAlbum(int userId, int albumId)
        {
            throw new NotImplementedException();
        }

        public void AddPhoto(ObjetDefinition.Photo p)
        {
            throw new NotImplementedException();
        }

        public void RemovePhoto(int userId, int albumId, int photoId)
        {
            throw new NotImplementedException();
        }

        public void AddAlbum(ObjetDefinition.Album a)
        {
            throw new NotImplementedException();
        }

        public void RemoveAlbum(int userId, int albumId)
        {
            throw new NotImplementedException();
        }
    }
}
