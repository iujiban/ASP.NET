using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace boardMVC.DotNetNote
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void YesButton_Click(object sender, EventArgs e)
        {
           
            Session.Remove("userID");
            Session.RemoveAll();
            Response.Redirect("~/DotNetNote/UserLogin.aspx");
        }

        protected void NoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DotNetNote/BoardList.aspx");
        }
    }
}