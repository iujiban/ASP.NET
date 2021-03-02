using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace boardMVC.DotNetNote
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { // admin관리자면 admin 관리자 홈페이지로 아니면 BoardList.aspx 페이지로 이동
           if (Session["level"].ToString() == "300SA" )
            {
                Response.Redirect("~/DotNetNote/admin/Default.aspx");
            } else
            {
                Response.Redirect("~/DotNetNote/admin/BoardList.aspx");
            }
        }
    }
}