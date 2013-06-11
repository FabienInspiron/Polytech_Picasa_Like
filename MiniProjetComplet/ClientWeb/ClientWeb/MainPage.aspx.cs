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

            String signOut = Request.QueryString["signout"];
            if (signOut != null)
            {
                sess = null;
                Session.Clear();
            }
        }

        protected void AlbumRefreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                Album[] albums = WebService.Service.GetAlbumCollection(sess.Id);

                lstBrowser.Items.Clear();
                foreach (Album a in albums)
                {
                    ListItem item = new ListItem();
                    item.Text = a.Nom;
                    item.Value = a.Id.ToString();

                    lstBrowser.Items.Add(item);
                }
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

        protected void lstBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            MAJPhotos();
        }

        public void MAJPhotos(){
            int pho = int.Parse(lstBrowser.SelectedItem.Value);
            albumSelected = pho;
            //Photo[] listPhotos = WebService.Service.GetPhotoAlbum(sess.Id, pho);

            ImageInfo[] listPhotos = WebService.Service.GetPicturesFromUserAlbum(sess.Id, pho);
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
                p.ImageInfo.Album = int.Parse(lstBrowser.SelectedItem.Value);
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