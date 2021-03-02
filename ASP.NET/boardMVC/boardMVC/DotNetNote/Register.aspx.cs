using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using boardMVC.DotNetNote.Models;

namespace boardMVC.DotNetNote
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            var userRepo = new usersrepository();
            userRepo.AddUser(txtUserName.Text, txtUserID.Text, txtPassword.Text);

            string userPW = txtPassword.Text;
            string userID = txtUserID.Text;
            string userName = txtUserName.Text;

            Session["userID"] = userID;
            Session["userPW"] = userPW;
            Session["userName"] = userName;

            string strJs = "<script>alert ('가입완료');location.href='UserLogin.aspx';</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "goDefault", strJs);
        }
    }
}