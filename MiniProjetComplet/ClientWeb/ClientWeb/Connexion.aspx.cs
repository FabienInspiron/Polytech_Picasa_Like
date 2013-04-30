using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWeb
{
    public partial class Connexion : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Création des webservices
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();

            // Recuperation de l'id de l'utilisateur
            int id = 0;

            // Verification que le client se trouve dans la base de donnée

            // Création des variables de cession
            Session["idUser"] = id;
            Session["nomUser"] = TextBoxLogin.Text;

            // Redirection vers la page de visualisation des images
            Response.Redirect("VoirImage.aspx");
        }


        protected void ButtonInscription_Click(object sender, EventArgs e)
        {
            // Redirection vers la page de visualisation des images
            Response.Redirect("Inscription.aspx");
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {

        }
    }
}