using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.WebServices;
using System.Windows.Forms;
using System.IO;

namespace ClientWeb
{
    public partial class MainPage : System.Web.UI.Page
    {
        Utilisateur sess;
        int albumSelected = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            String login = Request.QueryString["login"];
            String password = Request.QueryString["password"];
            if (login != null && password != null)
            {
                Utilisateur u = WebService.Service.Connexion(login, password);

                // Création des variables de cession
                Session["idUser"] = u.Id;
                Session["nomUser"] = u.Nom;
                Session["Utilisateur"] = u;

                sess = u;
            }
            else if (Session["Utilisateur"] != null)
            {
                sess = (Utilisateur) Session["Utilisateur"];
            }

            String signOut = Request.QueryString["signout"];
            if (signOut != null)
            {
                sess = null;
                Session.Clear();
            }
            MajAlbums();

            String album = Request.QueryString["album"];
            if (album != null)
            {
                albumSelected = int.Parse(album);
                MAJPhotos();
            }
        }

        private void MajAlbums()
        {
            try
            {
                Album[] albums;
                if (sess != null) albums = WebService.Service.GetAlbumCollection(sess.Id);
                else albums = WebService.Service.GetPublicAlbumCollection();

                AlbumList.DataSource = albums;
                AlbumList.DataBind();
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
            }
        }
        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (lstBrowser.Items.Count > 0 && lstBrowser.SelectedIndex >= 0)
        //    {
        //        String val = lstBrowser.SelectedItem.Value;

        //        int albVal = int.Parse(val);
        //        WebService.Service.RemoveAlbum(sess.Id, albVal);
        //    }
        //}

        //protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(AlbumList.SelectedItem != null)
        //        albumSelected = int.Parse(AlbumList.SelectedValue);
        //        MAJPhotos();
        //}

        public void MAJPhotos(){
            ImageInfo[] listPhotos = WebService.Service.GetPicturesFromAlbum(albumSelected);
            PictureList.DataSource = listPhotos;
            PictureList.DataBind();

            //PictureList.Items.Clear();
            //foreach (ImageInfo ii in listPhotos)
            //{
            //    ListItem i = new ListItem();
            //    i.Attributes.Add Text = p.Item2.ToString();
            //    i.Value = p.Item1.ToString();
            //    listPhoto.Items.Add(i);
            //}
        }

        protected void UploadImageButton_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.FileName.Equals(""))
            {
                Picture p = new Picture();
                p.ImageInfo = new ImageInfo();
                p.ImageInfo.Album = albumSelected;
                p.ImageInfo.Name = FileUpload1.FileName;
                p.ImageData = new MemoryStream(FileUpload1.FileBytes);
                WebService.Service.AddPicture(p.ImageInfo, p.ImageData);
                MAJPhotos();
            }
        }

        protected void AddFolderButton_Click(object sender, EventArgs e)
        {
            Album a = new Album();
            a.Nom = TextBox1.Text;
            a.UserId = sess.Id;

            Album ret = WebService.Service.AddAlbum(a);

            if (ret == null)
                Response.Write("Erreur, impossible d'ajouter l'album");
            else
                Response.Write("Album ajouté");
        }

    }
}