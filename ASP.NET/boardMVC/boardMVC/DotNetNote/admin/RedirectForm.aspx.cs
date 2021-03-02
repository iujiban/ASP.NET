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
    public partial class RedirectForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ModifyButtonEvent(object sender, EventArgs e)
        {
            usersrepository userepo = new usersrepository();

            var _ID = Convert.ToInt32(Session["ModifyKey"].ToString());
            var _userID = Session["ModifyUser"].ToString();

            
            userepo.adminModify(_ID, _userID, txtPassword.Text, txtLevel.Text);

            Response.Redirect("Default.aspx");
            
            //왜 프로시저 아닌 Update구문으로 executenonquery를 돌릴 때 response.redirect가 안 먹는지.
            /*
            string strJs = "<script>alert('수정완료');location.href='~/DotNetNote/admin/default.aspx'</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ModifyDefault", strJs);
            */


        }
    }
}