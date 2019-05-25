<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="Homepage" Title="Employee Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%" class="prof_table">
    <tr align="center" valign="top">
        <td colspan="2"><h1>EMPLOYEE PROFILE</h1>
        </td>
    </tr>
    <tr>
        <td style="width: 20%">
            <asp:Image ID="Image1" runat="server" Width="150px" ImageUrl="~/EmpImg/blank.bmp" />
        </td>
        <td style="width: 80%">
            <table>
                <tr>
                    <td>Company</td>
                    <td>:</td>
                    <td><asp:Label ID="LblCompany" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Employee ID</td>
                    <td>:</td>
                    <td><asp:Label ID="LblEmpID" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Name</td>
                    <td>:</td>
                    <td><asp:Label ID="LblName" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Father / Husband </td>
                    <td>:</td>
                    <td><asp:Label ID="LblFHName" runat="server" Text="Label"></asp:Label></td>
                </tr>         
                <tr>
                    <td>Contact No</td>
                    <td>:</td>
                    <td><asp:Label ID="LblContact" runat="server" Text="Label"></asp:Label></td>
                </tr>     
                <tr>
                    <td>Date of Birth</td>
                    <td>:</td>
                    <td><asp:Label ID="LblDob" runat="server" Text="Label"></asp:Label></td>
                </tr>       
                <tr>
                    <td>E-Mail ID</td>
                    <td>:</td>
                    <td><asp:Label ID="LblMailID" runat="server" Text="Label"></asp:Label></td>
                </tr>     
                <tr>
                    <td>AADHAR No</td>
                    <td>:</td>
                    <td><asp:Label ID="LblAadhar" runat="server" Text="Label"></asp:Label></td>
                </tr>     
                 <tr>
                    <td>PAN No</td>
                    <td>:</td>
                    <td><asp:Label ID="LblPAN" runat="server" Text="Label"></asp:Label></td>
                </tr>     
                <tr>
                    <td>Designation</td>
                    <td>:</td>
                    <td><asp:Label ID="LblDesig" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Department</td>
                    <td>:</td>
                    <td><asp:Label ID="LblDept" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Date of Joining</td>
                    <td>:</td>
                    <td><asp:Label ID="LblDOJ" runat="server" Text="Label"></asp:Label></td>
                </tr>                  
                <tr>
                    <td>Reporting To</td>
                    <td>:</td>
                    <td><asp:Label ID="LblRptTo" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Salary Bank A/c No</td>
                    <td>:</td>
                    <td><asp:Label ID="LblACNo" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>PF UAN </td>
                    <td>:</td>
                    <td><asp:Label ID="LblUAN" runat="server" Text="Label"></asp:Label></td>
                </tr>     
                 <tr>
                    <td>ESI No</td>
                    <td>:</td>
                    <td><asp:Label ID="LblESI" runat="server" Text="Label"></asp:Label></td>
                </tr>     
            </table>
        </td>
    </tr>
</table>
</asp:Content>

