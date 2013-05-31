using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceReference1;

namespace ClientWeb
{
    public partial class Image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            // On récupére la valeur du paramètre ImageID passé dans l’URL
            String id = Request.QueryString["ImageID"];
            String album = Request.QueryString["album"];
            String user = Request.QueryString["user"];

            int idI = int.Parse(id);
            int albumI = int.Parse(album);
            int userI = int.Parse(user);

            // Si ce paramètre n'est pas nul
            if (id != null)
            {
                ServiceClient service = new ServiceClient();

                Picture p = service.GetPicture(userI, albumI, idI);

                // on récupére notre image là où il faut
                Byte[] bytes = p.Image;

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