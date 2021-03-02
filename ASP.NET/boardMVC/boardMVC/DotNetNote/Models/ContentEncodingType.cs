using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace boardMVC.DotNetNote.Models
{
    public enum ContentEncodingType
    {
        //게시판 글 내용의 인코딩 처리 방식
        Text,

        //Html로 실행
        Html,

        //HTML로 실행 + 엔터키 (\r\n) 적용 됨
         Mixed
    }
}