using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
    public partial class VoirImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Recuperation de tous les albums
            List<string> listeDAlbum = new List<string>();
            listeDAlbum.Add("a");
            listeDAlbum.Add("b");
            listeDAlbum.Add("c");
            listeDAlbum.Add("d");

            foreach (String t in listeDAlbum)
                ListAlbum.Items.Add(t);
        }

        protected void Visualiser_Click(object sender, EventArgs e)
        {
            //ImageCourante.ImageUrl = "Image.aspx?ImageID=" + ImageIDBox.Text;

            ListAlbum.Items.Add("salut");
        }

        protected void AlbumSelected(object sender, EventArgs e)
        {
            String alb = sender.ToString();
            Console.WriteLine(" --- > " + alb);
        }
    }
}