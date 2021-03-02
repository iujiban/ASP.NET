using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace boardMVC.DotNetNote
{
    public partial class ImageDown1 : System.Web.UI.Page
    {// 완성형 게시판의 이미지 전용 다운 페이지 (이미지 경로를 보여주지 않고 다운로드 한다.)
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Request.QueryString["FileName"]))
            {
                Response.End();
            }
            string fileName = Request.Params["FileName"].ToString();
            string ext = Path.GetExtension(fileName); //확장자만 추출
            string contentType = "";
            if (ext == ".gif" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
            {
                switch (ext)
                {
                    case ".gif":
                        contentType = "image/gif"; break;
                    case ".jpg":
                        contentType = "image/jpeg"; break;
                    case ".jpeg":
                        contentType = "image/jpeg"; break;
                    case ".png":
                        contentType = "image/png"; break;
                }
            }

            else
            {
                Response.Write(
                    "<script language ='javascript'> + alert('이미지 파일이 아닙니다.')</script>"
                    );
                Response.End();
            }
            string file = Server.MapPath("./MyFiles/") + fileName;

            //강제 다운로드 로직 적용
            Response.Clear();
            Response.ContentType = contentType;
            Response.WriteFile(file);
            Response.End();
        }
    }
}