using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using boardMVC.DotNetNote.Models;
using System.Web.Security;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;
using System.Data;
using boardMVC.DotNetNote.Models;


namespace boardMVC.DotNetNote
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //변수 선언
            string userID = txtUserID.Text;
            string userPW = txtPassword.Text;
            var UserRepo = new usersrepository();

            Session["userID"] = userID;
            

            if (UserRepo.IsCorrectUser(userID, userPW))
            {
                if(Session["userID"] != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(userID, false);
                    Session["logged_in"] = 1;
                    Session["level"]=UserRepo.findingLevel(Session["userID"].ToString());
                    
                   // Session["user_ip"] = Request.ServerVariables
                    
                } else
                {
                    FormsAuthentication.SetAuthCookie(userID, false);
                    
                }
            }else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showMsg", "<script>alert('잘못된 사용자입니다.').</script>");
            }
            
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DotNetNote/Register.aspx");
        }
    }
}