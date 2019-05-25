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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBClass db = new DBClass();

        Label1.Text = db.GetEmpusername(EmpID());
    }

    private string EmpID()
    {
        try
        {
            string[] tt = this.Page.User.Identity.Name.ToString().Split('(');
            string[] tt1 = tt[1].ToString().Split(')');
            return tt1[0].ToString();
        }
        catch
        {
            return "0";
        }
    }
}
