using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using boardMVC.DotNetNote.Models;
namespace boardMVC.DotNetNote
{
    public partial class BoardDelete : System.Web.UI.Page
    {
        private string _Id;
        private string _level;

        protected void Page_Load(object sender, EventArgs e)
        {
            _level = Session["level"].ToString();
            _Id = Request.QueryString["Id"];

            if (!Page.IsPostBack)
            {
                if (_level == "300SA" || Session["userID"].ToString() == "admin")
                {
                    Response.Redirect("~/DotnetNote/admin/adminDelete.aspx");

                    if (String.IsNullOrEmpty(_Id))
                    {
                        Response.Redirect("BoardList.aspx");
                    }
                }


                else if (_level == "200A" && Session["userID"].ToString() == Session["ModifyLogin"].ToString())
                {
                    lnkCancel.NavigateUrl = "C:/ASP.NET/boardMVC/boardMVC/DotNetNote/BoardView.aspx? Id=" + _Id;
                    //lnkCancel.NavigateUrl = "BoardView.aspx?Id=" + _Id;
                    lblId.Text = _Id;

                    //버튼의 OnClientClick 속성 지성 방식과 동일
                    btnDelete.Attributes["onclick"] = "return ConfirmDelete();";

                    if (String.IsNullOrEmpty(_Id))
                    {
                        Response.Redirect("BoardList.aspx");
                    }
                }
                else
                {
                    Response.Redirect("BoardList.aspx");
                }
            }
        }

    


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            NoteRespository res = new NoteRespository();
            res.Delete(Convert.ToInt32(_Id), txtPassword.Text);
            Response.Redirect("~/DotNetNote/boardList.aspx");
            /*
            //현재 글(Id)의 비밀번호가 맞으면 삭제
            if ((new NoteRespository()).DeleteNotes
                (Convert.ToInt32(_Id), txtPassword.Text) <0)
            {
                Response.Redirect("BoardList.aspx");
            } else
            {
                lblMessage.Text = "삭제되지 않았습니다. 비밀번호를 확인하세요.";
            }
            */
        }
    }
}