<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="ClientWeb.MainPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

<% // Si la variable de session user est non nulle,
        // on "écrit" sa valeur dans la page HTML que l'on génère
        if (Session["nomUser"] != null)
        {
            %>
          <h1>Hello <% Response.Write(Session["nomUser"]); %></h1> 
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="MainPage.aspx?signout=1">Sign out</asp:HyperLink>   
            <%
        } else {
            %>
        <h1>Public access</h1> 
        <form id="form1" action="MainPage.aspx">
        <div class="style1">
            Login: <input type="text" name="login"/><br/>
            Password: <input type="password" name="password"/><br />
            <input type="submit" value="Submit"/>
        </div>
        </form>
    <%
        }
    %>
    <form id="form" runat="server">
    <div id="albums">
        <h2>List albums</h2>
        <% if (Session["nomUser"] != null)
        {
            %>
          <div class="form">
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="AddFolderButton" runat="server" OnClick="AddFolderButton_Click" Text="Ajouter" />
        </div>
            <%
        } %>
        <asp:ListBox ID="lstBrowser" runat="server" Width="200px" OnSelectedIndexChanged="lstBrowser_SelectedIndexChanged"
            AutoPostBack="True"></asp:ListBox>
        <asp:Button ID="AlbumRefreshButton" runat="server" OnClick="AlbumRefreshButton_Click"
            Text="Actualiser" />
        <br />
    </div>
    <div id="pictures">
        <h2>
            List Photos</h2>
            <% if (Session["nomUser"] != null)
        {
            %>
          <div class="form">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="UploadImageButton" runat="server" OnClick="UploadImageButton_Click"
                Text="Add" />
        </div>
            <%
        } %>
        <asp:ListView ID="PictureList" runat="server" DataKeyNames="Id">
            <ItemTemplate>
                <asp:Image ID="pictureControlID" runat="server" AlternateText='<% #Eval("Name") %>'
                    ImageUrl='<%# string.Format("Image.aspx?ImageID={0}&album={1}",Eval("Id"),Eval("Album"))%>' />
            </ItemTemplate>
        </asp:ListView>
    </div>
    </form>
</body>
</html>
