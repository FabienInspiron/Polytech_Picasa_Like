using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceReference1;
using System.Windows.Forms;

namespace ClientWeb
{
    public partial class MainPage : System.Web.UI.Page
    {
        ServiceClient service;
        Utilisateur sess;
        int albumSelected = -1;
        int iduser = 170;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Utilisateur"] == null)
                return;

            sess = (Utilisateur)Session["Utilisateur"];
            service = new ServiceClient();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Album[] albums = service.GetAlbumCollection(iduser);

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
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstBrowser.Items.Count > 0 && lstBrowser.SelectedIndex >= 0)
            {
                String val = lstBrowser.SelectedItem.Value;

                int albVal = int.Parse(val);
                service.RemoveAlbum(sess.Id, albVal);
            }
        }

        protected void lstBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            MAJPhotos();
        }

        public void MAJPhotos(){
            int pho = int.Parse(lstBrowser.SelectedItem.Value);
            albumSelected = pho;
            //Photo[] listPhotos = service.GetPhotoAlbum(sess.Id, pho);

            Tuple<int, String>[] listPhotos = service.GetPhotoAlbumTuple(iduser, pho);

            listPhoto.Items.Clear();
            foreach (Tuple<int, String> p in listPhotos)
            {
                ListItem i = new ListItem();
                i.Text = p.Item2.ToString();
                i.Value = p.Item1.ToString();
                listPhoto.Items.Add(i);
            }
        }

        protected void listPhoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            String idImage = listPhoto.SelectedItem.Value;
            String album = albumSelected.ToString();
            ImageCourante.ImageUrl = "Image.aspx?ImageID=" + idImage + "&user=" + iduser + "&album=" + album;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.FileName.Equals(""))
            {
                Photo p = new Photo();
                p.Album= int.Parse(lstBrowser.SelectedItem.Value);
                p.Image = FileUpload1.FileBytes;
                p.Nom = FileUpload1.FileName;
                service.AddPhoto(p);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MAJPhotos();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Album a = new Album();
            a.Nom = TextBox1.Text;
            a.UserId = sess.Id;

            Album ret = service.AddAlbum(a);

            if (ret == null)
                Response.Write("Erreur, impossible d'ajouter l'album");
            else
                Response.Write("Album ajouté");
        }

    }
}