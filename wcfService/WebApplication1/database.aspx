<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="database.aspx.cs" Inherits="WebApplication1.database" %>

<asp:content ID="GirisForm" ContentPlaceHolderID="GirisForm" runat="server">
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
            <td class="style1">
                <asp:Label ID="lblFirstName" runat="server" Text="First Name" Width="75px"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblLastName" runat="server" Text="Last Name" Width="75px"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnGiris" runat="server" Text="Giris" Width="75px" OnClick="btnGiris_Click" />
                <asp:Button ID="btnGetir" runat="server" Text="Getir" Width="75px" OnClick="btnGetir_Click" />
                <asp:Button ID="btnGuncelle" runat="server" Text="Güncelle" Width="75px" OnClick="btnGuncelle_Click" />
                <asp:Button ID="btnSil" runat="server" Text="Sil" Width="75px" OnClick="btnSil_Click" />
                <asp:Button ID="btnTemizle" runat="server" Text="Temizle" Width="75px" OnClick="btnTemizle_Click" />
                <asp:Button ID="btnCikis" runat="server" Text="Çıkış" Width="75px" OnClick="btnCikis_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblHata" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:content>
