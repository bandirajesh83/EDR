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

public partial class DutySchedule : System.Web.UI.Page
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

        string sql = "SELECT UNIQUE SCH.ATT_DATE \"ATT.DATE\", TO_CHAR(SCH.ATT_DATE, 'DAY') DAY, INTIME.ATT_TIMESTAMP \"SCHEDULED IN TIME\", OUTTIME.ATT_TIMESTAMP \"SCHEDULED OUT TIME\", DECODE(SCH.SHIFT_NAME,'G','GENERAL','P','PUBLIC HOLIDAY','L','LEAVE','W','W.OFF','R','RESERVED','O','ON DUTY','S','SPECIAL TIME',SCH.SHIFT_NAME) \"SHIFT NAME\" FROM HRM_EMP_DUTY_SCHEDULE SCH,  (SELECT * FROM HRM_EMP_DUTY_SCHEDULE WHERE IN_OUT_MODE=1) INTIME,  (SELECT * FROM HRM_EMP_DUTY_SCHEDULE WHERE IN_OUT_MODE=0) OUTTIME WHERE INTIME.EMP_ID (+)= SCH.EMP_ID AND INTIME.ATT_DATE (+)= SCH.ATT_DATE  AND OUTTIME.EMP_ID (+)= SCH.EMP_ID AND OUTTIME.ATT_DATE (+)= SCH.ATT_DATE AND SCH.EMP_ID=" + EmpID() + " AND SCH.ATT_DATE>='" + FrmDt.ToString("dd-MMM-yyyy") + "' AND SCH.ATT_DATE<='" + ToDt.ToString("dd-MMM-yyyy") + "' order by SCH.ATT_DATE"; 

        DataSet ds = db.GetData(sql);
        GridView1.DataSource = ds;
        GridView1.DataBind();
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
