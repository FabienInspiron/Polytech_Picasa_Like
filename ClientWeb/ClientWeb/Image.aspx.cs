using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminPicasaLike;

namespace ClientWeb
{
    public partial class Image : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            GestionBDD gest = new GestionBDD(db);

            // On récupére la valeur du paramètre ImageID passé dans l’URL
            String id = Request.QueryString["ImageID"];

            // Si ce paramètre n'est pas nul
            if (id != null)
            {
                // on récupére notre image là où il faut
                Byte[] bytes = gest.getImageByte(id);

                // et on crée le contenu de notre réponse à la requête HTTP
                // (ici un contenu de type image)
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "image/jpeg";
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
    }
}