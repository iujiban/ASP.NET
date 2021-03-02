using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace boardMVC
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //Session level이 관리자면 관리자 홈페이지로 아니면 boardList홈페이지로.
                if (Session["level"].ToString() == "300SA")
                {
                    Response.Redirect("~/DotNetNote/admin/Default.aspx");
                }
                else
                {
                    Response.Redirect("~/DotNetNote/BoardList.aspx");
                }
            }
        }

    }
}