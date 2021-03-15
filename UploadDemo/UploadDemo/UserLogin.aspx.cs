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
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
namespace UploadDemo
{
    public partial class UserLogin : System.Web.UI.Page
    {
        private string str = "Data Source = spectra; User Id= spectra; Password= artceps";
        public DataSet GetProduct(string userId, string password)
        {

            DataSet ds = new DataSet();

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Select * from phoneUser where userID= '{0}' and userpassword = '{1}'", userId, password);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            oda.Fill(ds);
            return ds;
        }
        public Boolean IsCorrectUser(string userId, string password)
        {
            bool result = false;

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Select * from phoneUser where userID= '{0}' and  userpassword = '{1}'", userId, password);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    result = true;
                }
                reader.Close();
                reader.Dispose();
            }catch (Exception)
            {

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return result;
        }
        public string findinglevel(string userID)
        {
            String level = "";

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Select userlevel from phoneUser where userID = '{0}'", userID);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
          //Model 자체를 안 만들면 OracleDataReader를 쓸 때 자꾸 read가 안됨.
            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    level = reader[0].ToString();
                }
                reader.Close();
                reader.Dispose();
            } catch (Exception)
            {

            }
            finally
            {
                con.Close();
                con.Dispose();
            }


            return level;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            var userID = Request.Form["userID"].ToString();
            var password = Request.Form["userPassword"].ToString();

            Session["userID"] = userID;
            Session["userPW"] = password;

            if (IsCorrectUser(userID, password))
            {
                if (Session["userID"] != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(userID, false);
                    Session["logged_in"] = 1;
                    Session["level"] =findinglevel(Session["userID"].ToString());
                }
                else
                {
                    string strJs = "<script>alert ('아이디 혹은 패스워드가 틀렸습니다.');location.href='UserLogin.aspx';</script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", strJs);
                }
            } 
        }
    }
}