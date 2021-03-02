using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace boardMVC.DotNetNote
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lblButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/DotNetNote/BoardList.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           Response.Redirect("~/DotNetNote/Logout.aspx");

        }
    }
}