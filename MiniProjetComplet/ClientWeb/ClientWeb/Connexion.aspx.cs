using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceReference1;

namespace ClientWeb
{
    public partial class Connexion : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Création des webservices
            ServiceClient service = new ServiceClient();
            Utilisateur u = service.Connexion(TextBoxLogin.Text, TextBoxMDP.Text);

            if (u == null)
            {
                Response.Write("Connexion Impossible");
                return;
            }

            // Recuperation de l'id de l'utilisateur
            int id = 0;

            // Verification que le client se trouve dans la base de donnée

            // Création des variables de cession
            Session["idUser"] = id;
            Session["nomUser"] = TextBoxLogin.Text;
            Session["Utilisateur"] = u;

            // Redirection vers la page de visualisation des images
            Response.Redirect("MainPage.aspx");
        }


        protected void ButtonInscription_Click(object sender, EventArgs e)
        {
            // Redirection vers la page de visualisation des images
            Response.Redirect("Inscription.aspx");
        }
    }
}