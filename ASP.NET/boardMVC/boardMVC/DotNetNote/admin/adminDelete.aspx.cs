using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using boardMVC.DotNetNote.Models;
namespace boardMVC.DotNetNote.admin
{
    public partial class adminDelete : System.Web.UI.Page
    {
        private string _level;
        private string _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _level = Session["level"].ToString();
            _id = Session["key"].ToString();

            lnkCancel.NavigateUrl = "BoardView.aspx?Id=" + _id;
            lblId.Text = _id;

            btnDelete.Attributes["onclick"] = "return ConfirmDelete()";

            if (String.IsNullOrEmpty(_id))
            {
                Response.Redirect("~/DotNetNote/BoardList.aspx");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            NoteRespository repo = new NoteRespository();


            repo.adminDelete(Convert.ToInt32(_id));
            Response.Redirect("~/DotNetNote/boardList.aspx");


        }
    }
}