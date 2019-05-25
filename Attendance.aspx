<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Attendance.aspx.cs" Inherits="Attendance" Title="Employee :: Attendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="prof_table">
    <tr>
        <td>From</td>
        <td><asp:TextBox ID="TxtFrm" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="50%" ReadOnly="false" TextMode="Date"></asp:TextBox></td>
        <td>To</td>
        <td><asp:TextBox ID="TxtTo" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="50%" ReadOnly="false" TextMode="Date"></asp:TextBox></td>
        <td><asp:Button ID="Button1" runat="server" Text="Show" Width="100px" Height="25px" OnClick="Button1_Click" /></td>
    </tr>
    <tr colspan="5">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="true" ShowFooter="false" CssClass="mGrid" PagerStyle-CssClass="pgr"></asp:GridView>
    </tr>
</table>
</asp:Content>

