<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LeaveApplication.aspx.cs" Inherits="LeaveApplication" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="prof_table">
    <tr align="center" valign="top">
        <td colspan="3"><h1>LEAVE APPLICATION</h1>
        </td>
    </tr>
    <tr>
        <td>From Date</td>
        <td>:</td>
        <td><asp:TextBox ID="TxtFrmDt" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>To Date</td>
        <td>:</td>
        <td><asp:TextBox ID="TxtToDt" runat="server"></asp:TextBox></td>    
    </tr>
    <tr>
        <td>Reason</td>
        <td>:</td>
        <td><asp:TextBox ID="TxtDescription" runat="server" MaxLength="100" Rows="1" Width="100%" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>    
    </tr>   
    <tr>
        <td>Remarks</td>
        <td>:</td>
        <td><asp:TextBox ID="TxtRemarks" runat="server" MaxLength="100" Rows="1" Width="100%" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>    
    </tr>   
    <tr align="center" valign="top">
        <td colspan="3" align="center">
            <asp:Button ID="Button1" runat="server" Text="Save" Width="100px" Height="25px" OnClick="Button1_Click" /><asp:Button ID="Button2" runat="server" Text="Clear" Width="100px" Height="25px" OnClick="Button2_Click" /></td>
    </tr> 
    <tr align="center" valign="top">
        <td align="center" colspan="3">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="true" ShowFooter="false" CssClass="mGrid" PagerStyle-CssClass="pgr" OnSorting="GridView1_Sorting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"></asp:GridView>        </td>
    </tr>
</table>
</asp:Content>

