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

public partial class LeaveHistory : System.Web.UI.Page
{
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.User.Identity.IsAuthenticated)
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
    private void LoadDataGrid()
    {
        DateTime FrmDt = Convert.ToDateTime(TxtFrm.Text);
        DateTime ToDt = Convert.ToDateTime(TxtTo.Text);


        string sql = "SELECT LEAVE_DATE \"LEAVE DATE\",TO_CHAR(LEAVE_DATE, 'DAY') \"DAY\", DESCRIPTION, LEAVE_TYPE \"LEAVE TYPE\", DECODE(STATUS,'C','CANCELLED','APPROVED') STATUS FROM VEW_HRM_LEAVE_HISTORY WHERE EMP_ID=" + EmpID() + " AND LEAVE_DATE>='" + FrmDt.ToString("dd-MMM-yyyy") + "' AND LEAVE_DATE<='" + ToDt.ToString("dd-MMM-yyyy") + "' ORDER BY LEAVE_DATE";
        
        if (db.Con.State == ConnectionState.Closed) db.Con.Open();
        Oracle.DataAccess.Client.OracleCommand cmd = new Oracle.DataAccess.Client.OracleCommand(sql, db.Con);
        Oracle.DataAccess.Client.OracleDataReader dr = cmd.ExecuteReader();
        if (!dr.HasRows)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('No Leaves in this period');", true);
            GridView1.DataSource = null;
            GridView1.DataBind();
            dr.Close();
            db.Con.Close();
            return;
        }
        dr.Close();
        db.Con.Close();


        DataSet ds = db.GetData(sql);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('No Leaves in this period');", true);
            return;
        }
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TxtFrm.Text.Trim().Length <= 0 || TxtTo.Text.Trim().Length <= 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid Inputs');", true);
            return;
        }

        LoadDataGrid();
    }
}
