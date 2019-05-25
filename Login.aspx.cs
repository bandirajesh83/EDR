using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (GetUserIP() != "192.168.98.96" && GetUserIP() != "192.168.98.58" && GetUserIP() != "192.168.98.80")
        //{
        //    Session["ERROR"] = "You are not Authorised Person";
        //    Response.Redirect("~/Error.aspx");
        //}
        Session.Abandon();
        Session.Contents.RemoveAll();
        Session.Clear();
        FormsAuthentication.SignOut();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //string usrname = TxtUserName.Text.ToUpper();

        int userId = db.ValidateUser(TxtUserName.Text, TxtPassword.Text, DropDownList1.SelectedValue.ToString());
        string usrname = db.GetEmpName(userId.ToString()) + "(" + userId.ToString() + ")";
        string message = string.Empty;
        if (userId < 0)
            message = "Invalid User Name or Password...";
        else if(userId==0)
            message = "You are already Exit from the Office";
        else
            message = "Welcome to " + usrname.ToString();

        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);

        if (userId > 0)
        {
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(usrname, false);
        }
    }
    private string GetUserIP()
    {
        return Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"];
    }

    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
}
