using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUI
{ //게시판의 상세보기 페이징서 태그를 실행하지않고 순수 텍스트로 변환 (HTML 태그를 특수 기호로 변경)
    public class HtmlUtility
    { //Html을 실행하지 않고 소스 그대로 표현해서 바로 웹 페이지로 보여주는 함수
        public static string Encode(string strContent)
        {
            string strTemp = "";
            if (String.IsNullOrEmpty(strContent))
            {
                strTemp = "";
            }
            else
            {
                strTemp = strContent;
                strTemp = strTemp.Replace("&", "&amp;");
                strTemp = strTemp.Replace(">", "&gt;");
                strTemp = strTemp.Replace("<", "&lt;");
                strTemp = strTemp.Replace("\r\n", "<br />");
                strTemp = strTemp.Replace("\"", "&#34;");
            }
            return strTemp;
        }
        //Html을 실행하지 않고 소스 그대로 표현해서 바로 웹 페이지로 보여주는 함수
        public static string EncodeWithTabAndSpace(string strContent)
        {
            return Encode(strContent)
                //추가적으로 탭과 공백도 HTML 코드 (&nbspl;)로 처리해서 출력
                .Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;")
                .Replace(" " + " ", "&nbsp;&nbsp;");
        }
    }
}
