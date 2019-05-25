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
using System.IO;

public partial class Homepage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBClass db = new DBClass();

        if (!this.Page.User.Identity.IsAuthenticated)
        {
            FormsAuthentication.RedirectToLoginPage();
        }

        //FileStream FS = new FileStream(db.GetEmpID(EmpID()).ToString() + ".jpg", FileMode.Create); 

        DataSet ds = db.GetEmpDtls(EmpID());
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            LblAadhar.Text = dr["AADHAR"].ToString();
            LblACNo.Text = dr["SB_ACNO"].ToString();
            LblCompany.Text = dr["CHANNEL_NAME"].ToString();
            LblContact.Text = dr["MOBILE_NO"].ToString();
            LblDept.Text = dr["DEPT_NAME"].ToString();
            LblDesig.Text = dr["Emp_Position"].ToString();
            LblDob.Text = Convert.ToDateTime(dr["DOB"].ToString()).ToString("dd-MMM-yyyy");
            LblDOJ.Text = Convert.ToDateTime(dr["DOJ"].ToString()).ToString("dd-MMM-yyyy");
            LblEmpID.Text = dr["Emp_Code"].ToString();
            LblESI.Text = dr["ESI_NO"].ToString();
            LblFHName.Text = dr["EMP_FATHER_HUSBAND_NAME"].ToString();
            LblMailID.Text = dr["E_MAIL"].ToString().ToLower();
            LblName.Text = dr["Emp_Fname"].ToString();
            LblPAN.Text = dr["PAN"].ToString();
            LblRptTo.Text = dr["HOD_EMP_NAME"].ToString();
            LblUAN.Text = dr["PF_UAN"].ToString();

            if (!DBNull.Value.Equals(dr["PHOTO"]))
            {
                byte[] imgg = (byte[])(dr["PHOTO"]);
                if (imgg == null)
                {
                    Image1.Visible = true;
                    //Image1.ImageUrl = "~/EMPIMG/blank.bmp";
                }
                else
                {
                    Image1.Visible = true;
                    MemoryStream mstream = new MemoryStream(imgg);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(mstream);
                    string strPath = Server.MapPath("/EMPIMG/" + db.GetEmpID(EmpID()).ToString() + ".bmp");
                    img.Save(strPath);
                    Image1.ImageUrl = "~/EMPIMG/" + db.GetEmpID(EmpID()).ToString() + ".bmp";
                }
            }
            else
                Image1.Visible = true;
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
}
