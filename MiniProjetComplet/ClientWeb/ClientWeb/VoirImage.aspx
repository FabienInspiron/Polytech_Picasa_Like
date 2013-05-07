<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoirImage.aspx.cs" Inherits="ClientWeb.VoirImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    Bonjour
    <% // Si la variable de session user est non nulle,
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
            Liste des albums :
            <br />
            <asp:TextBox ID="TextAlbumAdd" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Ajouter album" OnClick="Button1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:ListBox ID="ListAlbum" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged"
                Width="300px" AutoPostBack="True">
                <asp:ListItem></asp:ListItem>
            </asp:ListBox>
            <asp:Button ID="Supprimer" runat="server" OnClick="Button2_Click1" Text="Supprimer album" />
            <br />
            <br />
            <asp:Button ID="Button3" runat="server" Text="Voir images ..." OnClick="Button3_Click1" />
        </p>
        Liste des photos
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Ajouter" Width="68px" />
        <br />
        <asp:ListBox ID="ListBoxPhoto" runat="server" Width="300px" OnSelectedIndexChanged="ListBoxPhoto_SelectedIndexChanged">
        </asp:ListBox>
        <br />
        <asp:Image ID="ImageCourante" runat="server" Height="16px" />
    </div>
    </form>
</body>
</html>
