using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.WebServices;

namespace ClientWeb
{
    public partial class Connexion : System.Web.UI.Page
    {
        protected void ButtonInscription_Click(object sender, EventArgs e)
        {
            // Redirection vers la page de visualisation des images
            Response.Redirect("Inscription.aspx");
        }
    }
}