<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplication1.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="background-color:rgb(150, 150, 150);">
    <form id="form1" runat="server">
    <div>
    <table id="Table1" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblID" runat="server" Text="ID"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPw" runat="server" type="password"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td colspan = "2">
            <asp:Button ID="btnGiris" runat="server" Text="Giriş" 
                onclick="btnGiris_Click" />
            <asp:Button ID="btnCikis" runat="server" Text="Çıkış" 
                onclick="btnCikis_Click" />
            <asp:Button ID="btnYeni" runat="server" onclick="btnYeni_Click" Text="Yeni" />
        </td>
        </tr>
        <tr>
        <td colspan = "2">
            <asp:Label ID="lblHata" runat="server" ForeColor="Red"></asp:Label>
        </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
