using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using boardMVC.DotNetNote.Models;
using System.Web.Configuration;
using System.Web.Security;
namespace boardMVC.DotNetNote
{
    public partial class BoardView : System.Web.UI.Page
    {
      
        private String _Id; // 앞 리스트에서 넘어온 번호 저장
        private String _level;
        protected void Page_Load(object sender, EventArgs e)
        {
            _level = Session["level"].ToString();
            if(_level == "300SA")
            {
                lnkDelete.NavigateUrl = "~/DotNetNote/admin/adminDelete.aspx";
                lnkModify.NavigateUrl = "BoardModify.aspx?Id=" + Request["Id"];
                lnkReply.NavigateUrl = "BoardReply.aspx?Id=" + Request["Id"];
            }
            else
            {
                lnkDelete.NavigateUrl = "BoardDelete.aspx?Id=" + Request["Id"];
                lnkModify.NavigateUrl = "BoardModify.aspx?Id=" + Request["Id"];
                lnkReply.NavigateUrl = "BoardReply.aspx?Id=" + Request["Id"];
            }

            _Id = Request.QueryString["Id"];
            if(_Id == null)
            {
                Response.Redirect("./BoardList.aspx");
            }
            if(!Page.IsPostBack)
            {
               //if(!String.IsNullOrEmpty(Session["userID"].ToString()))
               if(Session["userID"] != null)
                {
                    DisplayData();
                }else 
                {
                    Response.Redirect("~/DotNetNote/UserLogin.aspx");
                }
   
            }

        }
        private void DisplayData()
        {
            //넘어온 Id 값에 해당하는 레코드를 하나 읽어서 Note 클래스에 바인딩
            var note = (new NoteRespository()).GetNoteById(Convert.ToInt32(_Id));

            lblNum.Text = _Id;
            Session["key"] = lblNum.Text;
            lblName.Text = note.Name;
            Session["ModifyLogin"] = lblName.Text;
            lblEmail.Text = String.Format("<a href=\"mailto:{0}]\">{0}</a>", note.Email);
            lblTitle.Text = note.Title;
            string content = note.Content.ToString();

            //인코딩 방식에 따른 데이터 출력
            string strEncoding = note.Encoding;
            if(strEncoding=="Text")
            {
                lblContent.Text = DUI.HtmlUtility.EncodeWithTabAndSpace(content);
            } else if (strEncoding=="Mixed")
            {
                lblContent.Text = content.Replace("\r\n", "<br />");
            } else
            {
                lblContent.Text = content;
            }

            lblReadCount.Text = note.ReadCount.ToString();
            lblHomepage.Text = String.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", note.Homepage);
            lblPostDate.Text = note.PostDate.ToString();
            lblPostIp.Text = note.PostIp;

            if(note.FileName.Length > 1)
            {
                lblFile.Text = String.Format("<a href='./BoardDown.aspx?Id={0}'>" +
                    "{1}{2} / 전송수: {3}</a>", note.Id, "<img src=\"/images/ext/ext_zip.gif\" boarder=\"0\">", note.FileName, note.DownCount);
            }
            if (DUI.BoardLibrary.IsPhoto(note.FileName))
            {
                ltrImage.Text = "<img src =\'ImageDown.aspx?FileName=" +
                    $"{Server.UrlEncode(note.FileName)}\'>";
            } else
            {
                lblFile.Text = "(업로드된 파일이 없습니다.)";
            }
        } 
    }
}