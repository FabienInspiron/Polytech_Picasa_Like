using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using LibrairieServeur;
using ObjetDefinition;
using ObjetsDefinition;
using System.Security.Principal;
using System.Security.Permissions;


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

        [PrincipalPermission(SecurityAction.Demand, Role = "admin")]
        public List<Album> getAllAlbums()
        {
            return gestionBdd.getAllAlbum();
        }

        #endregion

        # region Photo

        [PrincipalPermission(SecurityAction.Demand, Role = "admin")]
        public List<ImageInfo> getAllPhoto()
        {
            return gestionBdd.getAllPhoto();
        }

        #endregion

        #region Role

        [PrincipalPermission(SecurityAction.Demand, Role = "admin")]
        public void addRole(String username, String role)
        {
            gestionBdd.addRole(username, role);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "admin")]
        public void delRole(String username)
        {
            gestionBdd.delRole(username);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "admin")]
        public void delRole(int id)
        {
            gestionBdd.delRole(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "admin")]
        public List<Role> getAllRole(int id)
        {
            return gestionBdd.getAllRole();
        }

        #endregion
    }
}
