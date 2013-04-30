<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Connexion.aspx.cs" Inherits="ClientWeb.Connexion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Login : "></asp:Label>
        <asp:TextBox ID="TextBoxLogin" runat="server" 
            ontextchanged="TextBox1_TextChanged"></asp:TextBox>
        <br />
        Mot de passe :
        <asp:TextBox ID="TextBoxMDP" runat="server"></asp:TextBox>
    
    </div>
    <p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Connexion" />
    </p>
    </form>
</body>
</html>
