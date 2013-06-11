using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ClientWeb.WebServices;

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
            Utilisateur u = new Utilisateur();
            u.Prenom = TextBoxPrenom.Text;
            u.Nom = TextBoxNom.Text;
            u.Mdp = TextBoxMDP.Text;

            Utilisateur u_new = WebService.Service.Inscription(u);

            // Mise en place de la variable de session contenant l'utilisateur
            Session["Utilisateur"] = u_new; 

            if (u_new == null)
            {
                Console.WriteLine("Inscription impossible");
                return;
            }

            // Redirection vers la page de visualisation des images
            Response.Redirect("Connexion.aspx");
        }
    }
}