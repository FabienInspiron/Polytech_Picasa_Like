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
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ServiceAdmin" à la fois dans le code, le fichier svc et le fichier de configuration.
    public class ServiceAdmin : IServiceAdmin
    {
        private GestionBDD gestionBdd;

        public ServiceAdmin()
        {
            DataBase bdd = new DataBase();
            gestionBdd = new GestionBDD(bdd);
        }

        # region Utilisateur

        public void delUtilisateur(int idUser)
        {
            gestionBdd.delUser(idUser);
        }

        public List<Utilisateur> getAllUsers()
        {
            return gestionBdd.getAllUser();
        }
        #endregion

        # region Album

        public List<Album> getAllAlbums()
        {
            return gestionBdd.getAllAlbum();
        }

        #endregion

        # region Photo

        public List<ImageInfo> getAllPhoto()
        {
            return gestionBdd.getAllPhoto();
        }

        #endregion
    }
}
