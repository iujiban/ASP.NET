using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using boardMVC.DotNetNote.Models;

namespace boardMVC.DotNetNote.Controls
{
    public partial class BoardCommentControl : System.Web.UI.UserControl
    {   //리파지터리 개체 생성:
        private NoteCommentRepository _repository;
        public BoardCommentControl()
        {
            _repository = new NoteCommentRepository();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            NoteComment comment = new NoteComment();
            if (!Page.IsPostBack)
            {
                //데이터 출력(현재 게시글의 번호(ID)에 해당하는 댓글 리스트)
                ctlCommnetList.DataSource = _repository.GetNoteComments(Convert.ToInt32(Request["Id"]));
                ctlCommnetList.DataBind();

                txtName.Text = Session["userID"].ToString();
                comment.Name = txtName.Text;
                
                
            }

        }

        protected void btnWriteComment_Click(object sender, EventArgs e)
        {
            NoteComment comment = new NoteComment();
            comment.BoardId = Convert.ToInt32(Request["Id"]); //부모글
            txtName.Text = Session["userID"].ToString();
            comment.Name = txtName.Text;
            comment.Password = txtPassword.Text;
            comment.Opinion = txtOpinion.Text;

            _repository.AddNoteComment(comment);

            Response.Redirect($"{Request.ServerVariables["SCRIPT_NAME"]}?Id = {Request["Id"]}");
        }
    }
}