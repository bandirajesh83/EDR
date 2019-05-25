<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" Title="Employee Information" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <title>TV5 News - Employee Information</title>
    <link rel="stylesheet" type="text/css" href="css/default1.css"/>
    
    <script type="text/javascript" language="javascript">
    window.onload = function () {
        noBack();
    }
    function noBack() {
        window.history.forward();
    }
</script>

<script type="text/javascript" >        history.forward();    </script>
</head>
<body onpageshow="if (event.persisted) noBack();">
<form id="form2" runat="server" class="register">
<img src="images/Logo.jpg" height="100px" width="200px" />
    <fieldset class="row1">
    <br />
    <br />
                <legend>Employee Information</legend>
                <br />
                <table border="0" cellpadding="5" cellspacing="5"  >
                    <tr>
                        <td style="width: 15%">Company&nbsp;</td>
                        <td style="width: 45%">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="100%">
                                <asp:ListItem Value="1">Shreya Broadcasting Pvt. Ltd., (TV5 News Telugu)</asp:ListItem>
                                <asp:ListItem Value="4">Nuzen Herbal Pvt. Ltd.,</asp:ListItem>
                                <asp:ListItem Value="5">Nuzen Industries Pvt. Ltd.,</asp:ListItem>
                                <asp:ListItem Value="7">TAFEL Technologies Pvt. Ltd.,</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                <tr>
                <td style="width: 15%"> Emp. ID&nbsp;</td>
                <td style="width: 45%"><asp:TextBox ID="TxtUserName" runat="server" Width="50%"></asp:TextBox></td>
                </tr>
                <tr>
                <td style="width: 15%">Password</td>
                <td style="width: 45%"><asp:TextBox ID="TxtPassword" runat="server" Width="50%" TextMode="Password">*</asp:TextBox></td>
                </tr>
                </table>
                <br />
                <p align="center">
                <asp:Button ID="Button1" runat="server" Text="Submit &raquo;" class="button" OnClick="Button1_Click"/>
                </p>
                </fieldset>
    </form>
</body>
</html>
