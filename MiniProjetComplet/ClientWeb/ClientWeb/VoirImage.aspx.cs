using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceReference1;

namespace ClientWeb
{
    public partial class VoirImage : System.Web.UI.Page
    {
        ServiceClient service;
        Utilisateur sess;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Utilisateur"] == null)
                return;

            sess = (Utilisateur)Session["Utilisateur"];
            service = new ServiceClient();
            MAJAlbums();
        }

        protected void MAJAlbums()
        {
            try
            {
                Album[] albums = service.GetAlbumCollection(sess.Id);

                ListAlbum.Items.Clear();
                foreach (Album a in albums)
                {
                    ListItem item = new ListItem();
                    item.Text = a.Nom;
                    item.Value = a.Id.ToString();

                    ListAlbum.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected void AlbumSelected(object sender, EventArgs e)
        {
            ListBox send = (ListBox)sender;
            Response.Write(send.SelectedItem.Value);

            /*
            String alb = sender.ToString();
            int album = 0;
            Photo[] photos = service.GetPhotoAlbum(sess.Id, album);
            foreach (Photo p in photos)
            {
                ListItem l = new ListItem();
                l.Text = p.Nom;
                l.Value = p.Id.ToString();
                ListBoxPhoto.Items.Add(l);
            }
             * */
        }

        // Afficher les albums
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Album a = new Album();
                a.Nom = TextAlbumAdd.Text;
                a.UserId = sess.Id;
                Album nouveau = service.AddAlbum(a);

                if (nouveau != null)
                    Console.WriteLine("Album ajouté avec succès");
            }
            catch (Exception eb)
            {
                Console.WriteLine(eb.Message);
            }
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            //Response.Write(ListAlbum.SelectedItem.Value);
        }

        // Click sur une photo
        protected void ListBoxPhoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            String idImage = "0";
            String album = "";
            ImageCourante.ImageUrl = "Image.aspx?ImageID=" + idImage + "&user=" + sess.Id + "&album=" + album;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd1 = new System.Windows.Forms.FolderBrowserDialog();
            fbd1.ShowDialog();
            string myFolder = fbd1.SelectedPath;


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // Iterate through the Items collection of the ListBox and 
            // display the selected items.
            foreach (ListItem item in ListAlbum.Items)
            {
                if (item.Selected)
                {
                    Console.Write(item.Text);
                }

            }
        }



        protected void Button3_Click1(object sender, EventArgs e)
        {
            ListBox lstbox1 = form1.FindControl("TextAlbumAdd") as ListBox;

            ListItem item = lstbox1.SelectedItem;
            if (Items != null)
            {
                Response.Write(item.Text);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

        }

        protected void ImageIDBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ListItem li = ListAlbum.SelectedItem;
            //TextAlbumAdd.Text = li.Text;
            Response.Write(ListAlbum.Items.Count);
        }
    }
}