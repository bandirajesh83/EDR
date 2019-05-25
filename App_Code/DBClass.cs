using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Oracle.DataAccess.Client;
using System.Globalization;

/// <summary>
/// Summary description for DBClass
/// </summary>
/// 
public class DBClass
{
    //public OracleConnection Con = new OracleConnection("Data Source=orcl12c;Persist Security Info=True;User ID=c##tekdev;Password=user123#;Min Pool Size=10;Connection Lifetime=120;Connection Timeout=60;Incr Pool Size=5; Decr Pool Size=2");
    public OracleConnection Con = new OracleConnection("Data Source=orcl12c;Persist Security Info=True;User ID=c##tekdev;Password=user123#;Validate Connection=true;Min Pool Size=10;Connection Lifetime=100000;Connection Timeout=60;Incr Pool Size=5; Decr Pool Size=2;");
    public OracleCommand cmd;

	public DBClass()
	{
        if (Con.State == ConnectionState.Closed)
            Con.Open();
	}

    ~DBClass()
    {
        Con.Close();
    }

    public string GetEmpusername(string EmpID)
    {
        string retval = "";
        if (Con.State == ConnectionState.Closed) Con.Open();
        cmd = new OracleCommand("select * from hrm_emp_basic_details where emp_id=" + EmpID, Con);
        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
            retval = dr["Emp_Fname"].ToString() + "(" + dr["emp_code"].ToString() + ")";
        dr.Close();
        Con.Close();
        return retval;
    }

    public string GetEmpName(string EmpID)
    {
        string retval="";
        if (Con.State == ConnectionState.Closed) Con.Open();
        cmd = new OracleCommand("select * from hrm_emp_basic_details where emp_id=" + EmpID, Con);
        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
            retval = dr["Emp_Fname"].ToString();
        dr.Close();
        Con.Close();
        return retval;
    }

    public string GetEmpID(string EmpCode, string ChannelID)
    {
        string retval = "";
        if (Con.State == ConnectionState.Closed) Con.Open();
        cmd = new OracleCommand("select * from hrm_emp_basic_details where emp_code='" + EmpCode.ToString() + "' and Channel_code=" + ChannelID.ToString(), Con);
        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
            retval = dr["emp_id"].ToString();
        dr.Close();
        Con.Close();
        return retval;
    }

    
    public DataSet GetEmpDtls(string EmpID)
    {
        if (Con.State == ConnectionState.Closed) Con.Open();
        cmd = new OracleCommand("select * from vew_hrm_employee_Information where emp_id=" + EmpID, Con);
        OracleDataReader dr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        dt.Load(dr);
        ds.Tables.Add(dt);
        dr.Close();
        Con.Close();
        return ds;
    }

   

    public int GetPrimaryVal(string tlbName, string ColName)
    {
        int res=0;
        if (Con.State == ConnectionState.Closed) Con.Open();
        cmd = new OracleCommand("select decode(max(" + ColName + "),null,0,max(" + ColName + ")) from " + tlbName, Con);
        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (dr[0].ToString() !=null)
                res = Convert.ToInt32(dr[0].ToString());
            else
                res = 0;
        }
        dr.Close();
        Con.Close();
        res++;
        return res;
    }

    public int GetChannelID(string empid)
    {
        int res=0;
        if (Con.State == ConnectionState.Closed) Con.Open();
        cmd = new OracleCommand("select * from hrm_emp_basic_details where emp_id=" + empid, Con);
        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
            res = Convert.ToInt16(dr["channel_code"].ToString());
        dr.Close();
        Con.Close();
        return res;
    }

    public int GetEmpID(string EmpCOde)
    {
        int res = 0;
        if (Con.State == ConnectionState.Closed) Con.Open();
        cmd = new OracleCommand("Select * from hrm_emp_basic_details where emp_code='" + EmpCOde + "'", Con);
        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
            res = Convert.ToInt32(dr["emp_ID"].ToString());
        dr.Close();
        Con.Close();
        return res;
    }

    public bool IsDateTime(string inputDateTime)
    {
        DateTime dttm;
        bool isdatetm = DateTime.TryParse(inputDateTime, out dttm);
        return isdatetm;
    }

    public bool IsODEmp(string EmpID)
    {
        bool res = false;
        if (Con.State == ConnectionState.Closed) Con.Open();
        OracleCommand cmd = new OracleCommand("select * from hrm_emp_duty_schedule where emp_id=" + EmpID + " and att_date='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' and shift_name='O'",Con);
        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
            res = true;
        dr.Close();
        Con.Close();
        return res;
    }

    public int ValidateUser(string UserName, string Password, string channelID)
    {
        if (Con.State == ConnectionState.Closed) Con.Open();
        OracleCommand Cmd = new OracleCommand("select * from hrm_emp_basic_details where emp_code='" + UserName + "' and Channel_Code in (0," + channelID + ") and action_type in ('I','U')", Con);
        OracleDataReader  dr = Cmd.ExecuteReader();
        if (dr.Read())
        {
            int uid = Convert.ToInt32(dr["Emp_id"].ToString());
            string dob = Convert.ToDateTime(dr["dob"].ToString()).ToString("ddMMyyyy").ToUpper();
            if ( dob.ToString() == Password.ToString().ToUpper())
            {
                if (IsEmpExit(dr["Emp_ID"].ToString(), GetDutyDate(dr["emp_id"].ToString())))
                {
                    if (IsODEmp(uid.ToString()))
                    {
                        dr.Close();
                        Con.Close();
                        return uid;
                    }
                    else
                    {
                        dr.Close();
                        Con.Close();
                        return 0;
                    }
                }
                else
                {
                    dr.Close();
                    Con.Close();
                    return uid;
                }
            }
            else
            {
                dr.Close();
                Con.Close();
                return -2;
            }
        }
        else
        {

            dr.Close();
            Con.Close();
            return -1;
        }
    }

    public string Insert_Values(string tb1_name, string statement)
    {
        cmd = new OracleCommand("PKG_ADMIN.PRD_INSERT_VALUES", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("P_TABNAME", OracleDbType.Varchar2, 50).Value = tb1_name;
        cmd.Parameters.Add("P_STMT", OracleDbType.Varchar2, 1000000).Value = statement.ToUpper();
        cmd.Parameters.Add("P_RETCODE", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("P_CODE_VAL", OracleDbType.Varchar2, 40).Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string k = Convert.ToString(cmd.Parameters["P_CODE_VAL"].Value) + "," + Convert.ToString(cmd.Parameters["P_RETCODE"].Value);
        return k;
    }

    public DateTime GetDutyDate(string EmpID)
    {
        DateTime dt=DateTime.Now;
        if (Convert.ToInt32(EmpID.ToString()) > 0)
        {
            if (Con.State == ConnectionState.Closed) Con.Open();
            cmd = new OracleCommand("select max(att_date) att_date from hrm_emp_palmvein_logs where emp_id=" + EmpID.ToString(), Con);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!DBNull.Value.Equals(dr["att_date"]))
                {
                    dt = Convert.ToDateTime(dr["att_date"].ToString());
                }
            }
            dr.Close();
            Con.Close();
        }
        if (dt.Date == DateTime.Now.Date.AddDays(-1) || dt.Date == DateTime.Now.Date.AddDays(1))
            return dt;
        else
            return DateTime.Now;
    }

    public bool IsEmpExit(string EmpID, DateTime DutyDate)
    {
        bool IsExit = true;
        if (Con.State == ConnectionState.Closed) Con.Open();
        cmd = new OracleCommand("select * from hrm_emp_palmvein_logs where emp_id=" + EmpID + " and att_date='" + DutyDate.ToString("dd-MMM-yyyy") + "' order by att_timestamp", Con);
        OracleDataReader dr = cmd.ExecuteReader();
        while(dr.Read())
        {
            if (dr["in_out_mode"].ToString() == "0")
                IsExit = true;
            else
                IsExit = false;
        }
        dr.Close();
        Con.Close();
        return IsExit;
    }
    

    public DataSet GetData(string Sql)
    {
        if (Con.State == ConnectionState.Closed) Con.Open();
        DataSet ds = new DataSet();
        cmd = new OracleCommand(Sql, Con);
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        da.SelectCommand = cmd;
        da.Fill(ds, "table");
        Con.Close();
        return ds;
    }
}
