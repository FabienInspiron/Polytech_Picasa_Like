<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoirImage.aspx.cs" Inherits="ClientWeb.VoirImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
Bonjour <% // Si la variable de session user est non nulle,
            // on "écrit" sa valeur dans la page HTML que l'on génère
            if (Session["nomUser"] != null)
            {
                Response.Write(Session["nomUser"]);
            }
            else
            {
                Response.Write("inconnu");
                Response.Redirect("Connexion.aspx");
            }
    
     %>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:ListBox ID="ListAlbum" runat="server" 
                onselectedindexchanged="AlbumSelected" Width="300px">
            </asp:ListBox>
            
            <asp:Image ID="ImageCourante" runat="server" Height="16px" />
        </p>
        <p>
            Image&nbsp;ID&nbsp;:&nbsp;
            <asp:TextBox ID="ImageIDBox" runat="server" />
            <asp:Button ID="Visualiser" runat="server" OnClick="Visualiser_Click" Text="Visualiser" />
        </p>
    </div>
    </form>
</body>
</html>
