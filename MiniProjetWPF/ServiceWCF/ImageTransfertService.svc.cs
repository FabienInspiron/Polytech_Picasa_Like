using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace ServiceWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ImageTransfertService" à la fois dans le code, le fichier svc et le fichier de configuration.
    public class ImageTransfertService : IImageTransfertService
    {
        // la classe AccesDonnees n’est pas donnée ici
        private AccesDonnees bdAccess = new AccesDonnees();

        PhotoId IImageTransfertService.Upload(Photo p)
        {
            PhotoId id = new PhotoId();
            id.ID = bdAccess.uploadImage(p);
            return id;
        }

        Photo IImageTransfertService.Download(PhotoId imageId)
        {
            return bdAccess.getImage(imageId.ID); ;
        }
    }
}
