﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.296
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientAdmin.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Utilisateur", Namespace="http://schemas.datacontract.org/2004/07/ObjetDefinition")]
    [System.SerializableAttribute()]
    public partial class Utilisateur : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MdpField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NomField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PrenomField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Mdp {
            get {
                return this.MdpField;
            }
            set {
                if ((object.ReferenceEquals(this.MdpField, value) != true)) {
                    this.MdpField = value;
                    this.RaisePropertyChanged("Mdp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nom {
            get {
                return this.NomField;
            }
            set {
                if ((object.ReferenceEquals(this.NomField, value) != true)) {
                    this.NomField = value;
                    this.RaisePropertyChanged("Nom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Prenom {
            get {
                return this.PrenomField;
            }
            set {
                if ((object.ReferenceEquals(this.PrenomField, value) != true)) {
                    this.PrenomField = value;
                    this.RaisePropertyChanged("Prenom");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Album", Namespace="http://schemas.datacontract.org/2004/07/ObjetDefinition")]
    [System.SerializableAttribute()]
    public partial class Album : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NomField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nom {
            get {
                return this.NomField;
            }
            set {
                if ((object.ReferenceEquals(this.NomField, value) != true)) {
                    this.NomField = value;
                    this.RaisePropertyChanged("Nom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Photo", Namespace="http://schemas.datacontract.org/2004/07/ObjetDefinition")]
    [System.SerializableAttribute()]
    public partial class Photo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AlbumField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ImageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NomField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Album {
            get {
                return this.AlbumField;
            }
            set {
                if ((this.AlbumField.Equals(value) != true)) {
                    this.AlbumField = value;
                    this.RaisePropertyChanged("Album");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Image {
            get {
                return this.ImageField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageField, value) != true)) {
                    this.ImageField = value;
                    this.RaisePropertyChanged("Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nom {
            get {
                return this.NomField;
            }
            set {
                if ((object.ReferenceEquals(this.NomField, value) != true)) {
                    this.NomField = value;
                    this.RaisePropertyChanged("Nom");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Connexion", ReplyAction="http://tempuri.org/IService/ConnexionResponse")]
        ClientAdmin.ServiceReference1.Utilisateur Connexion(string pseudo, string mdp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Inscription", ReplyAction="http://tempuri.org/IService/InscriptionResponse")]
        ClientAdmin.ServiceReference1.Utilisateur Inscription(ClientAdmin.ServiceReference1.Utilisateur u);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetAlbumCollection", ReplyAction="http://tempuri.org/IService/GetAlbumCollectionResponse")]
        ClientAdmin.ServiceReference1.Album[] GetAlbumCollection(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddAlbum", ReplyAction="http://tempuri.org/IService/AddAlbumResponse")]
        ClientAdmin.ServiceReference1.Album AddAlbum(ClientAdmin.ServiceReference1.Album a);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/RemoveAlbum", ReplyAction="http://tempuri.org/IService/RemoveAlbumResponse")]
        void RemoveAlbum(int userId, int albumId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetPhotoAlbum", ReplyAction="http://tempuri.org/IService/GetPhotoAlbumResponse")]
        ClientAdmin.ServiceReference1.Photo[] GetPhotoAlbum(int userId, int albumId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetPhoto", ReplyAction="http://tempuri.org/IService/GetPhotoResponse")]
        ClientAdmin.ServiceReference1.Photo GetPhoto(int userId, int albumId, int photoId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddPhoto", ReplyAction="http://tempuri.org/IService/AddPhotoResponse")]
        void AddPhoto(ClientAdmin.ServiceReference1.Photo p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/RemovePhoto", ReplyAction="http://tempuri.org/IService/RemovePhotoResponse")]
        void RemovePhoto(int userId, int albumId, int photoId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : ClientAdmin.ServiceReference1.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<ClientAdmin.ServiceReference1.IService>, ClientAdmin.ServiceReference1.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ClientAdmin.ServiceReference1.Utilisateur Connexion(string pseudo, string mdp) {
            return base.Channel.Connexion(pseudo, mdp);
        }
        
        public ClientAdmin.ServiceReference1.Utilisateur Inscription(ClientAdmin.ServiceReference1.Utilisateur u) {
            return base.Channel.Inscription(u);
        }
        
        public ClientAdmin.ServiceReference1.Album[] GetAlbumCollection(int userId) {
            return base.Channel.GetAlbumCollection(userId);
        }
        
        public ClientAdmin.ServiceReference1.Album AddAlbum(ClientAdmin.ServiceReference1.Album a) {
            return base.Channel.AddAlbum(a);
        }
        
        public void RemoveAlbum(int userId, int albumId) {
            base.Channel.RemoveAlbum(userId, albumId);
        }
        
        public ClientAdmin.ServiceReference1.Photo[] GetPhotoAlbum(int userId, int albumId) {
            return base.Channel.GetPhotoAlbum(userId, albumId);
        }
        
        public ClientAdmin.ServiceReference1.Photo GetPhoto(int userId, int albumId, int photoId) {
            return base.Channel.GetPhoto(userId, albumId, photoId);
        }
        
        public void AddPhoto(ClientAdmin.ServiceReference1.Photo p) {
            base.Channel.AddPhoto(p);
        }
        
        public void RemovePhoto(int userId, int albumId, int photoId) {
            base.Channel.RemovePhoto(userId, albumId, photoId);
        }
    }
}
