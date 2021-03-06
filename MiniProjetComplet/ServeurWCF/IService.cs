﻿using System;
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

        #region utilisateur
        /// <summary>
        /// Gestion de la connexion d'un utilisateur
        /// </summary>
        /// <param name="pseudo">Pseudo</param>
        /// <param name="mdp">Mot de passe</param>
        /// <returns>L'utilisateur ou null si la connexion à échoué</returns>
        [OperationContract]
        Utilisateur Connexion(String pseudo, String mdp);

        /// <summary>
        /// Gestion inscription
        /// </summary>
        /// <param name="u">L'utilisateur à inscrir</param>
        /// <returns>Le nouvel utilisateur si ok, sinon null</returns>
        [OperationContract]
        Utilisateur Inscription(Utilisateur u);

        #endregion 

        #region album

        [OperationContract]
        List<Album> GetAlbumCollection(int userId);

        [OperationContract]
        List<Album> GetPublicAlbumCollection();

        [OperationContract]
        Album AddAlbum(Album a);

        [OperationContract]
        int RemoveAlbum(int userId, int albumId);

        #endregion

        #region photo

        [OperationContract]
        List<ImageInfo> GetPicturesFromAlbum(int albumId);

        [OperationContract]
        List<int> GetPicturesIdFromAlbum(int albumId);

        [OperationContract]
        ImageDownloadResponse GetPicture(ImageDownloadRequest p);

        [OperationContract]
        ImageDownloadResponse GetPictureThumbnail(ImagethumbnailDownloadRequest p);

        [OperationContract]
        void AddPicture(Picture p);

        [OperationContract]
        int RemovePicture(int userId, int albumId, int photoId);

        # endregion
    }
}
