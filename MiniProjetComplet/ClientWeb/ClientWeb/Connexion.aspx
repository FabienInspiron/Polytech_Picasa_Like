<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Connexion.aspx.cs" Inherits="ClientWeb.Connexion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style1">
        
        <asp:Label ID="Label1" runat="server" Text="Login : "></asp:Label>
        <asp:TextBox ID="TextBoxLogin" runat="server" style="margin-left: 48px" 
            Width="171px" ></asp:TextBox>
        <br />
        Mot de passe :
        <asp:TextBox ID="TextBoxMDP" runat="server" Width="168px"></asp:TextBox>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Connexion" style="text-align: center" />
    
    </div>
    <p>
        <asp:Button ID="BoutonInscription" runat="server" onclick="ButtonInscription_Click" 
            Text="Inscription" style="text-align: center" />
    </p>

    </form>
</body>
</html>
