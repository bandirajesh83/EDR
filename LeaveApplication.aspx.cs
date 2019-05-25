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
using Oracle.DataAccess.Client;

public partial class LeaveApplication : System.Web.UI.Page
{
    DBClass db = new DBClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.User.Identity.IsAuthenticated)
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        DateTime FrmDt = DateTime.Now;
        DateTime ToDt = DateTime.Now;
        try
        {
            FrmDt = Convert.ToDateTime(TxtDutyDate.Text);
            ToDt = Convert.ToDateTime(TxtFrmTime.Text);
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid Date Format');", true);
            return;
        }

        if (FrmDt < DateTime.Now || ToDt < DateTime.Now)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid Leave Dates');", true);
            return;
        }

        //db.Insert_Values("hrm_emp_leave_apply","Insert into hrm_emp_leave_apply values (:pk_val,
        



    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
