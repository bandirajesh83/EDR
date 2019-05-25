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
using System.Net;
using System.Net.NetworkInformation;
using System.Management;
using Oracle.DataAccess.Client;


public partial class DailyReport : System.Web.UI.Page
{
    DBClass db = new DBClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.User.Identity.IsAuthenticated)
        {
            FormsAuthentication.RedirectToLoginPage();
        }

        if (db.IsEmpExit(EmpID(), db.GetDutyDate(EmpID())))
        {
            if (db.IsODEmp(EmpID()))
                TxtDutyDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        else
            TxtDutyDate.Text = db.GetDutyDate(EmpID()).ToString("dd-MMM-yyyy");

        LoadDataGrid(); 
    }

    private void LoadDataGrid()
    {
        DataSet ds = db.GetData("select frm_time \"FROM TIME\", to_time \"TO TIME\", work_desc \"WORK DESCRIPTION\", TO_CHAR(TO_DATE(CASE WHEN NVL (ROUND (datediff ('SS',frm_time,to_time),0),0) > 0 THEN NVL (ROUND (datediff ('SS',frm_time,to_time),0),0) ELSE 0 END,'sssss'),'hh24:mi:ss') DURATION from hrm_emp_edr where emp_id=" + EmpID() + " and edr_date='" + TxtDutyDate.Text + "' order by frm_time desc");
        GridView1.DataSource = ds;
        GridView1.DataBind(); 
    }

    public string GetMacAddress(string ipAddress)
    {
        string macAddress = string.Empty;
        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
        pProcess.StartInfo.FileName = "arp";
        pProcess.StartInfo.Arguments = "-a " + ipAddress;
        pProcess.StartInfo.UseShellExecute = false;
        pProcess.StartInfo.RedirectStandardOutput = true;
        pProcess.StartInfo.CreateNoWindow = true;
        pProcess.Start();
        string strOutput = pProcess.StandardOutput.ReadToEnd();
        string[] substrings = strOutput.Split('-');
        if (substrings.Length >= 8)
        {
            macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                + "-" + substrings[7] + "-"
                + substrings[8].Substring(0, 2);
            return macAddress;
        }
        else
        {
            return "not found";
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

    protected void Button2_Click(object sender, EventArgs e)
    {
        TxtFrmTime.Text = "";
        TxtToTime.Text = "";
        TxtDescription.Text = "";
    }
  
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TxtFrmTime.Text.Trim().Length <= 0 || TxtToTime.Text.Trim().Length <= 0 || TxtFrmTm.Text.Trim().Length<=0 || TxtToTm.Text.Trim().Length<=0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid Work Duration Time');", true);
            return;
        }

        if (TxtDescription.Text.Trim().Length <= 10)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please enter work Description. (Minimum 10 and Maximum 2000 Characters)');", true);
            return;
        }

        DateTime DutyDt = DateTime.Now;
        DateTime FrmDt = DateTime.Now;
        DateTime ToDt = DateTime.Now;
        TimeSpan ts = ToDt - FrmDt;
        try
        {
            DutyDt = Convert.ToDateTime(TxtDutyDate.Text);
            FrmDt = Convert.ToDateTime(TxtFrmTime.Text + " " + TxtFrmTm.Text);
            ToDt = Convert.ToDateTime(TxtToTime.Text + " " + TxtToTm.Text);
            ts = ToDt - FrmDt;
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid Date time format. Please check it once.)');", true);
            return;
        }
        
        if (FrmDt < DutyDt || ToDt<DutyDt || FrmDt>DateTime.Now || ToDt>DateTime.Now)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid Working Date & Time');", true);
            return;
        }

        if (ts.TotalSeconds> 50000 || ts.TotalSeconds<300)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid Work Duration Time');", true);
            return;
        }

        DataSet ds = db.GetData("Select * from hrm_emp_edr where emp_id=" + Convert.ToInt32(EmpID()) + " and To_Date('" + FrmDt.ToString("dd-MMM-yyyy HH:mm:ss") + "','DD-MON-YY HH24:MI:SS') between frm_time and to_time");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This time is alreay entered');", true);
            ds.Dispose();
            return;
        }
        ds.Dispose();

        ds = db.GetData("Select * from hrm_emp_edr where emp_id=" + Convert.ToInt32(EmpID()) + " and to_date('" + ToDt.ToString("dd-MMM-yyyy HH:mm:ss") + "','DD-MON-YY HH24:MI:SS') between frm_time and to_time");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('This time is alreay entered');", true);
            ds.Dispose();
            return;
        }
        ds.Dispose();

        //string strUserAgent = Request.UserAgent;
        string Manfacturer = Request.Browser.MobileDeviceManufacturer;
        string model=Request.Browser.MobileDeviceModel;
        string screen = Request.Browser.ScreenPixelsWidth.ToString() + " x " + Request.Browser.ScreenPixelsHeight.ToString();
        string browser=Request.Browser.Browser.ToString();
        string platform = Request.UserAgent.ToString();
        string ipadd = Request.UserHostAddress.ToString();

        //string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
        //String ecname = System.Environment.MachineName;
        //string sysname = computer_name[0].ToString();

        //System.Net.IPHostEntry ipList;
        //ipList = System.Net.Dns.GetHostByAddress(Request.ServerVariables["REMOTE_HOST"].ToString());

        string sysname = "";
        
        string macadd = GetMacAddress(Request.UserHostAddress.ToString()).ToUpper();

        if (Convert.ToInt32(EmpID()) <= 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invaid Employee Code');", true);
            return;
        }



        InsertEDR(Convert.ToInt32(EmpID()), DutyDt, FrmDt, ToDt, TxtDescription.Text, Manfacturer, model, screen, browser, platform, ipadd, sysname, macadd);

        LoadDataGrid();

        TxtFrmTime.Text = "";
        TxtToTime.Text = "";
        TxtDescription.Text = "";

        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Successfully Saved');", true);
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //DataSet ds = db.GetData("select frm_time \"FROM TIME\", to_time \"TO TIME\", work_desc \"WORK DESCRIPTION\", TO_CHAR(TO_DATE(CASE WHEN NVL (ROUND (datediff ('SS',frm_time,to_time),0),0) > 0 THEN NVL (ROUND (datediff ('SS',frm_time,to_time),0),0) ELSE 0 END,'sssss'),'hh24:mi:ss') DURATION from hrm_emp_edr where emp_id=" + EmpID() + " and edr_date='" + TxtDutyDate.Text + "'");
        //ds.Tables[0].DefaultView.Sort = e.SortExpression;
        //GridView1.DataSource = ds;
        //GridView1.DataBind();
        GridView1.DataBind();
    }

    public void InsertEDR(int EmpID, DateTime EdrDate, DateTime FrmTime, DateTime ToTime, string WrkDesc, string ManFact, string Model, string Screen, string Browser, string Plotform, string IPAdd, string SysName, string MacAdd)
    {
            string acttype = "I";
            if (db.Con.State == ConnectionState.Closed) db.Con.Open();
            OracleCommand  cmd = new OracleCommand("Insert into hrm_emp_edr values (:EDR_ID,:EMP_ID,:EDR_DATE,:FRM_TIME,:TO_TIME,:WORK_DESC,:MANFACTURER,:MODEL_NAME,:SCREEN,:BROWSER,:PLOTFORM,:IP_ADDRESS,:SYSNAME,:MAC_ADD,:CREATED_DATE,:ACTION_TYPE)", db.Con);
            cmd.Parameters.Add(new OracleParameter("@EDR_ID", db.GetPrimaryVal("hrm_emp_edr", "EDR_ID")));
            cmd.Parameters.Add(new OracleParameter("@EMP_ID", EmpID));
            cmd.Parameters.Add(new OracleParameter("@EDR_DATE", EdrDate));
            cmd.Parameters.Add(new OracleParameter("@FRM_TIME", FrmTime));
            cmd.Parameters.Add(new OracleParameter("@TO_TIME", ToTime));
            OracleParameter param = new OracleParameter();
            param.ParameterName = "@WORK_DESC";
            param.OracleDbType = OracleDbType.NVarchar2;
            param.Value = WrkDesc;
            cmd.Parameters.Add(param);
            cmd.Parameters.Add(new OracleParameter("@MANFACTURER", ManFact));
            cmd.Parameters.Add(new OracleParameter("@MODEL_NAME", Model));
            cmd.Parameters.Add(new OracleParameter("@SCREEN", Screen));
            cmd.Parameters.Add(new OracleParameter("@BROWSER", Browser));
            cmd.Parameters.Add(new OracleParameter("@PLOTFORM", Plotform));
            cmd.Parameters.Add(new OracleParameter("@IP_ADDRESS", IPAdd));
            cmd.Parameters.Add(new OracleParameter("@SYSNAME", SysName));
            cmd.Parameters.Add(new OracleParameter("@MAC_ADD", MacAdd));
            cmd.Parameters.Add(new OracleParameter("@CREATED_DATE", DateTime.Now));
            cmd.Parameters.Add(new OracleParameter("@ACTION_TYPE", acttype));
            if (db.Con.State == ConnectionState.Closed) db.Con.Open();
            cmd.ExecuteNonQuery();
            db.Con.Close();
    }
}
