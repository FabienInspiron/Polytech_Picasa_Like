﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="ClientWeb.MainPage" %>

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
        <div>
        <b>List albums</b>
            <br />
        <br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                Text="Ajouter" />
        <br />

        <asp:ListBox ID="lstBrowser" runat="server" Width="200px" 
                onselectedindexchanged="lstBrowser_SelectedIndexChanged" 
                AutoPostBack="True"></asp:ListBox>

    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Actualiser" />

        <br />
    </div>
            <br />
            <br />
     <b>List Photos</b><br /><br /><asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Ajouter" />
        <br />
     <asp:ListBox ID="listPhoto" runat="server" Width="200px" 
                AutoPostBack="True" 
            onselectedindexchanged="listPhoto_SelectedIndexChanged"></asp:ListBox>

    
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Actualiser" />
            <br />
            <b>Photo</b><br />
        <asp:Image ID="ImageCourante" runat="server" Height="16px" />

    </div>
    </form>
</body>
</html>