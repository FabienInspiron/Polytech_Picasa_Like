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
    <form id="form1" action="MainPage.aspx">
    <div class="style1">
        Login: <input type="text" name="login"><br/>
        Password: <input type="password" name="password"><br />
        <input type="submit" value="Submit">
        <%--<asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Connexion" style="text-align: center" />--%>
    </div>
    </form>
    <form id="form2" runat="server">
    <p>
        <asp:Button ID="BoutonInscription" runat="server" onclick="ButtonInscription_Click" 
            Text="Inscription" style="text-align: center" />
    </p>
    </form>

    
</body>
</html>
