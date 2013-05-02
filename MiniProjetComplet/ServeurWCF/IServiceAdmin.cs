using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ObjetDefinition;

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
        List<Photo> getAllPhoto();

        #endregion
    }
}
