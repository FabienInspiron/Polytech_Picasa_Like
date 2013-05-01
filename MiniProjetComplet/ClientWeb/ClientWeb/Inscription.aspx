<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inscription.aspx.cs" Inherits="ClientWeb.Inscription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    </form>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label2" runat="server" Text="Login : "></asp:Label>
        <asp:TextBox ID="TextBoxNom" runat="server" style="margin-left: 52px" ></asp:TextBox> <br />
        <asp:Label ID="Label3" runat="server" Text="Nom : "></asp:Label>
        <asp:TextBox ID="TextBoxPrenom" runat="server" style="margin-left: 54px" ></asp:TextBox>
        <br />
        Mot de passe :
        <asp:TextBox ID="TextBoxMDP" runat="server" style="margin-left: 2px"></asp:TextBox>
    
    </div>
    <p>
        <asp:Button ID="Button2" runat="server" onclick="ButtonInscription_click" 
            Text="Inscription" />
    </p>
    </form>
</body>
</html>
