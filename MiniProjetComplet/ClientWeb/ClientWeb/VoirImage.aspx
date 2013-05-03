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
            Liste des albums : <br />
            <asp:TextBox ID="TextAlbumAdd" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Ajouter album" 
                onclick="Button1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
            <asp:ListBox ID="ListAlbum" runat="server" 
                onselectedindexchanged="AlbumSelected" Width="300px">
            </asp:ListBox>

            <asp:Button ID="Supprimer" runat="server" onclick="Button2_Click1" 
                Text="Supprimer album" />
            
        </p>
            Liste des photos
            <br />

            <asp:TextBox ID="TextBoxParcourir" runat="server"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Parcourir ..." 
            onclick="Button2_Click" />

            <br />
            <asp:ListBox ID="ListBoxPhoto" runat="server" 
                Width="300px" onselectedindexchanged="ListBoxPhoto_SelectedIndexChanged">
            </asp:ListBox>
            
            <asp:Image ID="ImageCourante" runat="server" Height="16px" />
            
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>

        <p>
            &nbsp;</p>
        <p>
            Image&nbsp;ID&nbsp;:&nbsp;
            <asp:TextBox ID="ImageIDBox" runat="server" />
        </p>
    </div>
    </form>
</body>
</html>
