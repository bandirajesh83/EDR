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

public partial class Attendance : System.Web.UI.Page
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

        GenAttendance(EmpID(), FrmDt, ToDt);

        string sql = "select att.att_date \"ATT.DATE\",to_char(att.att_date,'DAY') \"DAY\", in_time \"IN TIME\", out_time \"OUT TIME\",DECODE(sch.SHIFT_NAME,'G','GENERAL','P','PUBLIC HOLIDAY','L','LEAVE','W','W.OFF','R','RESERVED','O','ON DUTY','S','SPECIAL TIME',sch.SHIFT_NAME) \"SHIFT NAME\",TO_CHAR(TO_DATE(CASE WHEN NVL (ROUND (datediff ('SS',in_time,out_time),0),0) > 0 THEN NVL (ROUND (datediff ('SS',in_time,out_time),0),0) ELSE 0 END,'sssss'),'hh24:mi:ss') Duration FROM HRM_EMP_EDR_ATTENDANCE att, (select unique emp_id, att_date, shift_name from hrm_emp_duty_schedule) sch where sch.emp_id (+)= att.emp_id and sch.att_date (+)= att.att_date AND ATT.EMP_ID=" + EmpID() + " order by att.att_date,in_time";

        DataSet ds = db.GetData(sql);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    private void GenAttendance(string EmpID, DateTime frmdt, DateTime todt)
    {

        if (db.Con.State == ConnectionState.Closed) db.Con.Open();

        OracleCommand cmd = new OracleCommand("delete from HRM_EMP_EDR_ATTENDANCE where emp_id=" + EmpID.ToString(), db.Con);
        cmd.ExecuteNonQuery();
        
        cmd = new OracleCommand("select * from hrm_emp_palmvein_logs where action_type<>'D' and emp_id=" + EmpID.ToString() + " and att_Date>='" + frmdt.ToString("dd-MMM-yyyy") + "' and att_date<='" + todt.ToString("dd-MMM-yyyy") + "' order by log_id", db.Con);
        OracleDataReader drt = cmd.ExecuteReader();
        int PVal = 0;
        while (drt.Read())
        {
            string stat = drt["in_out_mode"].ToString();
            DateTime attdt = Convert.ToDateTime(drt["att_date"].ToString());
            DateTime ptime = Convert.ToDateTime(drt["att_timestamp"].ToString());

            if (stat == "1")
            {
                string[] res = db.Insert_Values("hrm_emp_edr_attendance", "INSERT INTO HRM_EMP_EDR_ATTENDANCE VALUES (:PK_VAL," + EmpID + ",'" + attdt.ToString("dd-MMM-yyyy") + "',to_date('" + ptime.ToString("dd-MMM-yyyy HH:mm:ss") + "','dd-MON-yyyy HH24:MI:SS'),null)").Split(',');
                PVal = Convert.ToInt32(res[1].ToString());
            }
            else
            {
                OracleCommand cmd1 = new OracleCommand("Update hrm_emp_edr_attendance set out_time=to_date('" + ptime.ToString("dd-MMM-yyyy HH:mm:ss") + "','dd-MON-yyyy HH24:MI:SS') where edr_id=" + PVal, db.Con);
                cmd1.ExecuteNonQuery();
            }
        }
        drt.Close();
        db.Con.Close();
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
