using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ObjetDefinition;

namespace ServeurWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService
    {
        /// <summary>
        /// Gestion de la connexion d'un utilisateur
        /// </summary>
        /// <param name="pseudo">Pseudo</param>
        /// <param name="mdp">Mot de passe</param>
        /// <returns>L'id de l'utilisateur ou -1 si la connexion à échoué</returns>
        [OperationContract]
        int connexion(String pseudo, String mdp);

        /// <summary>
        /// Gestion inscription
        /// </summary>
        /// <param name="u">L'utilisateur à inscrir</param>
        /// <returns>Le nouvel utilisateur si ok, sinon null</returns>
        [OperationContract]
        Utilisateur instription(Utilisateur u);

        [OperationContract]
        AlbumCollection getAlbumCollection(int userId);

        [OperationContract]
        PhotoCollection getPhotoAlbum(int userId, int albumId);

        [OperationContract]
        void AddPhoto(Photo p);

        [OperationContract]
        void RemovePhoto(int userId, int albumId, int photoId);

        [OperationContract]
        void AddAlbum(Album a);

        [OperationContract]
        void RemoveAlbum(int userId, int albumId);
    }
}
