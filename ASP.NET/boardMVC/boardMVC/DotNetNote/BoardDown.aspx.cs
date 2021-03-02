using System;
using boardMVC.DotNetNote.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace boardMVC.DotNetNote
{// 이 페이지를 구현하면 파일 첨부란을 클릭 하였을 때 이 페이지를 통해서 파일 강제 다운로드 실행
    //넘어온 번호에 해당하는 파일을 지정된 폴더로부터 강제 다운로드 시키고 다운로드 카운트 1 증가.
    public partial class BoardDown : System.Web.UI.Page
    {
        private string fileName = "";
        private string dir = "";

        private NoteRespository _repository;
        public BoardDown()
        {
            _repository = new NoteRespository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //넘어온 번호에 해당한느 파일명 가져오기(보안 때문에... 파일명 숨김)
            fileName = _repository.GetFileNameById(Convert.ToInt32(Request["Id"]));

            //다운로드 폴더 지정: 실제 사용 시 반드시 벼경
            dir = Server.MapPath(" ./MyFiles/");
            if(fileName == null) // 특정 번호에 해당하는 첨부파일이 없다면,
            {
                Response.Clear();
                Response.End();
            }
            else
            {   //다운로드 카운트 증가 메서드 호출
                _repository.UpdateDownCount(fileName);

                //강제 다운로드 창 띄우기 주요 로직
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlPathEncode((fileName.Length > 50) ?
                    fileName.Substring(fileName.Length - 50, 50) : fileName));
                Response.WriteFile(dir + fileName);
                Response.End();
            }
        }
    }
}