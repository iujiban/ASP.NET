using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;
using boardMVC.DotNetNote.Models;
using System.Web.UI.HtmlControls;

namespace boardMVC.DotNetNote.admin
{
    public partial class EditUser : System.Web.UI.Page
    {
        private string str = "Data Source = spectra; User Id = spectra; Password=artceps";
        protected void Page_Load(object sender, EventArgs e)
        {

            HtmlForm modifyForm = (HtmlForm)Page.FindControl("ModifyForm");
            modifyForm.Visible = false;

        }
        

        protected void SearchText_Click(object sender, EventArgs e)
        {
            var _ID = "";
            var _User = ""; 
            var _SearchUser = UserIdtext.Text;
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Select user_Id, user_uid from loggedin_ where user_uid = '{0}'", _SearchUser);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    _ID = reader[0].ToString();
                    _User = reader[1].ToString();

                }
                reader.Close();
            }

            Session["ModifyKey"] = _ID;
            Session["ModifyUser"] = _User;

            con.Close();

            Response.Redirect("RedirectForm.aspx");
        // 두개의 form을 만드는 것보다 따로 form을 만들어서 연결 시켜주는게 더 편하고 빠름.
            HtmlForm searchForm = (HtmlForm)Page.FindControl("SearchForm");
            searchForm.Visible = false;

            HtmlForm modifyForm = (HtmlForm)Page.FindControl("ModifyForm");
            modifyForm.Visible = true;

        }

        protected void ModifyButtonEvent(object sender, EventArgs e)
        {
            usersrepository userepo = new usersrepository();

            var _ID = Convert.ToInt32(Session["ModifyKey"].ToString());
            var _userID = Session["ModifyUser"].ToString();


            userepo.adminModify(_ID, _userID, txtPassword.Text, txtLevel.Text);

            

            Response.Redirect("Default.aspx");

        }

        /*
        protected void ModifyButtonEvent(object sender, EventArgs e)
        {
            /*
            var _ID = Session["ModifyKey"].ToString();
            var _User = Session["ModifyUser"].ToString();

            var _password = txtPassword.Text;
            var _level = txtLevel.Text;

            var _checkUpdate = txtPassword.Text;
            var _checkPassword = "";

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Update loggedin_ set user_password = '{0}', user_level = '{1}' where user_ID = {2} and user_UID = '{3}'", _password, _level, _ID, _User);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            HtmlForm searchForm = (HtmlForm)Page.FindControl("SearchForm");
            searchForm.Visible = false;

            HtmlForm modifyForm = (HtmlForm)Page.FindControl("ModifyForm");
            ModifyForm.Visible = false;

            HtmlForm redirectForm = (HtmlForm)Page.FindControl("RedirectForm");
            redirectForm.Visible = true;

            cmd.CommandText = string.Format("Select user_password from loggedin_ where user_id = {0}", _checkUpdate);
            cmd.CommandType = CommandType.Text;

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while( reader.Read())
                {
                    _checkPassword = reader[0].ToString();
                }
                reader.Close();
            }
            if(_checkUpdate == _checkPassword)
            {
                string strJs = "<script>alert('수정완료');location.href='~/DotNetNote/admin/default.aspx'</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ModifyDefault", strJs);
            } else
            {
                Response.Redirect("~/DotNetNote/admin/deafult.aspx");
            }
            



        }
    */
    }
}