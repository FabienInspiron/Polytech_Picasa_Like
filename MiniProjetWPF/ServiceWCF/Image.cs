using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.ServiceModel;

namespace ServiceWCF
{
    /// <summary>
    /// Class représentant un message d'une image
    /// </summary>
    [MessageContract]
    public class Photo
    {
        [MessageHeader(MustUnderstand = true)]
        public PhotoInfo PhotoInfo;

        [MessageBodyMember(Order = 1)]
        public Stream Blob;
    }

    [DataContract]
    public class PhotoInfo
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int ID { get; set; }

        [DataMember(Order = 2, IsRequired = true)]
        public int AlbumId { get; set; }

        [DataMember(Order = 3, IsRequired = true)]
        public string Name { get; set; }
    }

    [MessageContract]
    public class PhotoId
    {
        [MessageBodyMember(Order = 1)]
        public int ID;
    }
}