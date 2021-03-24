using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;
using System.Data;
using System.Web.Security;
namespace UploadDemo
{
    public partial class Register : System.Web.UI.Page
    {
        private string str = "Data Source = spectra; User Id= spectra; Password = artceps";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                var txtName = txtUserName.Text.ToString();
                var txtUser = txtUserID.Text.ToString();
                var txt_password = txtPassword.Text.ToString();

                OracleConnection con = new OracleConnection();
                con.ConnectionString = str;
                OracleCommand cmd = new OracleCommand();

                cmd.CommandText = string.Format("INSERT INTO phoneUser values (phoneUser_seq.nextval, '{0}', '{1}', '{2}', 'SMART')", txtName, txtUser, txt_password);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                Session["userID"] = txtUser;
                Session["userName"] = txtName;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                string strJs = "<script>alert ('가입완료');location.href='UserLogin.aspx';</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "goDefault", strJs);

            }
            catch (OracleException)
            {
                string strJs = "<script> alert ('동일한 아이디가 있습니다');location.href='UserLogin.aspx';</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ErrorMessage", strJs);
            }




        }
    }
}