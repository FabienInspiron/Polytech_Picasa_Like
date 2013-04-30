using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
    public partial class Inscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonInscription_click(object sender, EventArgs e)
        {
            // Création des webservices
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();

            // Redirection vers la page de visualisation des images
            Response.Redirect("Connexion.aspx");
        }
    }
}