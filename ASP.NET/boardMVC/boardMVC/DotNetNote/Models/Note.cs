using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
/// <summary>
/// 모델과 뷰 모델 클래스 형태로 개발하면 웹 폼이 아닌 MVC에서도 그대로 사용 할 수 잇다.
/// 따라서 게시판 프로젝트에서는 SqlDataReader와 DataSet 같은 웹 폼 친화적인 클래스를 사용하지 않고, List<T>의 Generic Class형태를 사용했음.
/// </summary>
namespace boardMVC.DotNetNote.Models
{//Note 클래스: Notes 테이블과 일대일 매핑되는 ViewModel 클라스
    public class Note
    {
        [Display(Name = "번호")]
        public int Id { get; set; }
        [Display(Name = "작성자")]
        [Required(ErrorMessage = "* 이름을 작성해 주세요.")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "이메일을 정확히 입력하세요.")]
        public string Email { get; set; }
        [Display(Name = "제목")]
        [Required(ErrorMessage = "* 제목을 작성해주세요.")]
        public string Title { get; set; }
        [Display(Name = "작성일")]
        public DateTime PostDate { get; set; }
        public string PostIp { get; set; }
        [Display(Name = "내용")]
        [Required(ErrorMessage = "* 내용을 작성해 주세요.")]
        public string Content { get; set; }
        [Display(Name = "비밀번호")]
        [Required(ErrorMessage = "* 비밀번호를 작성해 주세요.")]
        public string Password { get; set; }
        [Display(Name = "조회수")]
        public int ReadCount { get; set; }
        [Display(Name = "인코딩")]
        public string Encoding { get; set; } = "Text";
        public string Homepage { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyIp { get; set; }
        [Display(Name = "파일")]
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public int DownCount { get; set; }
        public int Ref { get; set; }
        public int Step { get; set; }
        public int RefOrder { get; set; }
        public int AnswerNum { get; set; }
        public int ParentNum { get; set; }

        public int CommentCount { get; set; }
        public string Category { get; set; } = "Free"; //자유 게시판 기본

        public int Rownumber { get; set; }
        }
}