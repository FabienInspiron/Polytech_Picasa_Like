using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ObjetDefinition;
using ObjetsDefinition;

namespace ServeurWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IServiceAdmin" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IServiceAdmin
    {
        # region Utilisateur

        [OperationContract]
        void delUtilisateur(int idUser);

        [OperationContract]
        List<Utilisateur> getAllUsers();
        #endregion

        # region Album

        [OperationContract]
        List<Album> getAllAlbums();

        #endregion

        # region Photo

        [OperationContract]
        List<ImageInfo> getAllPhoto();

        #endregion

        # region Roles

        [OperationContract]
        void addRole(String username, String role);

        [OperationContract]
        void delRole(String username);

        [OperationContract]
        void delRole(int id);

        [OperationContract]
        List<Role> getAllRole(int id);
        
        #endregion
    }
}
