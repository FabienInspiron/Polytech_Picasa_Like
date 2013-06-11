using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;

namespace ObjetDefinition
{
    [MessageContract]
    public class Picture
    {
        [MessageHeader(MustUnderstand = true)]
        public ImageInfo ImageInfo;
        [MessageBodyMember(Order = 1)]
        public Stream ImageData;
    }
    [MessageContract]
    public class ImageDownloadResponse
    {
        [MessageBodyMember(Order = 1)]
        public Stream ImageData;
    }
    [MessageContract]
    public class ImageDownloadRequest
    {
        [MessageBodyMember(Order = 1)]
        public ImageInfo ImageInfo;
    }
    [MessageContract]
    public class ImagethumbnailDownloadRequest
    {
        [MessageBodyMember(Order = 1)]
        public ImageInfo ImageInfo;
        [MessageBodyMember(Order = 2)]
        public int MaxLargestSide;
    }
    [DataContract]
    public class ImageInfo
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int Id { get; set; }
        [DataMember(Order = 2, IsRequired = true)]
        public String Name { get; set; }
        [DataMember(Order = 3, IsRequired = true)]
        public int Album { get; set; }

        public ImageInfo(String name)
            : this(-1, name, -1)
        { }

        public ImageInfo(String name, int album)
            : this(-1, name, album)
        { }

        public ImageInfo(int id, String name, int albumId)
        {
            this.Id = id;
            this.Name = name;
            this.Album = albumId;
        }
    }
}