using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using boardMVC.DotNetNote;
namespace boardMVC
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserLogin user = new UserLogin();
            Session["display"] = user.Session["userID"];
            if (Session["display"] !=null)
            {
                Session_display.Text = Session["display"].ToString();
                Session_pw.Text = user.Session["userpw"].ToString();
            }
            else
            {
                Session_display.Text = "아무것도 없습니다.";
                Session_pw.Text = "아무것도 없습니다.";
            }
        }
    }
}