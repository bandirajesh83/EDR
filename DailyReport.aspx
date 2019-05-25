<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DailyReport.aspx.cs" Inherits="DailyReport" Title="Employee :: Employee Daily Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="prof_table">
    <tr align="center" valign="top">
        <td colspan="2"><h1>EMPLOYEE DAILY REPORT</h1></td>
    </tr>
    <tr>
        <td style="width: 25%">
            Duty Date</td>
        <td style="width: 75%">
            <asp:TextBox ID="TxtDutyDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="50%" ReadOnly="true"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width:25%">Start Time</td>
        <td style="width:75%"><asp:TextBox ID="TxtFrmTime" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="24%" TextMode="date"></asp:TextBox><asp:TextBox ID="TxtFrmTm" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="24%" TextMode="time"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width:25%">End Time</td>
        <td style="width:75%"><asp:TextBox ID="TxtToTime" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="24%" TextMode="date"></asp:TextBox><asp:TextBox ID="TxtToTm" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="24%" TextMode="time"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width:25%">Work Description</td>
        <td style="width:75%">
            <asp:TextBox ID="TxtDescription" runat="server" MaxLength="2000" Rows="5" TextMode="MultiLine" Width="100%" BorderStyle="solid" BorderWidth="1px" Height="100px" CssClass="responsive-textbox" ></asp:TextBox></td>
    </tr>
     <tr align="center" valign="top">
        <td colspan="2" align="center">
            <asp:Button ID="Button1" runat="server" Text="Save" Width="100px" Height="25px" OnClick="Button1_Click" /><asp:Button ID="Button2" runat="server" Text="Clear" Width="100px" Height="25px" OnClick="Button2_Click" /></td>
    </tr>
    <tr>
        <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="true" ShowFooter="false" CssClass="mGrid" PagerStyle-CssClass="pgr" OnSorting="GridView1_Sorting"></asp:GridView>
          </td>
    </tr>
</table>
</asp:Content>

