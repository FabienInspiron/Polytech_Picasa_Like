using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.WebServices;

namespace ClientWeb
{
    public partial class ImageThumb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            // On récupére la valeur du paramètre ImageID passé dans l’URL
            String id = Request.QueryString["ImageID"];
            String album = Request.QueryString["album"];

            int idI = int.Parse(id);
            int albumI = int.Parse(album);

            // Si ce paramètre n'est pas nul
            if (id != null)
            {
                ImageInfo i = new ImageInfo();
                i.Album = albumI;
                i.Id = idI;

                Byte[] bytes = Util.StreamToByte(WebService.Service.GetPictureThumbnail(i, 100));

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