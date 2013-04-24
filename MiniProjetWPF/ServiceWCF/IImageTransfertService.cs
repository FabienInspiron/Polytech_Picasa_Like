using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace ServiceWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IImageTransfertService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IImageTransfertService
    {
        [OperationContract]
        PhotoId Upload(Photo p);
        [OperationContract]
        Photo Download(PhotoId imageId);
    }
}
