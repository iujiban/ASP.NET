using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace boardMVC.DotNetNote.Models
{// 게시판의 글쓰기 폼 구성 분류 (Write, Modify, Reply)
    public enum BoardWriteFormType
    {
        //글 쓰기 페이지
        Write,

        //글 수정 페이지
        Modify,

        //글 답변 페이지
        Reply
    }
}