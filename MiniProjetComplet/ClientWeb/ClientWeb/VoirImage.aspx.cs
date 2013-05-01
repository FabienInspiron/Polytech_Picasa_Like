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
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilisateur u = (Utilisateur) Session["Utilisateur"];

            ServiceClient service = new ServiceClient();
            Album[] l = service.GetAlbumCollection(u.Id);

            for (int i = 0; i < l.Length; i++)
            {
                ListAlbum.Items.Add(l[i].Nom);
            }
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