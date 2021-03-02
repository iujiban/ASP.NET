using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace boardMVC.DotNetNote.Models
{
    public class users
    {
        [Key]
        public int Id { get; set; }

        [Display (Name = "이름")]
        [Required (ErrorMessage = "이름을 입력하시오.")]
        [StringLength(25, MinimumLength =1, ErrorMessage ="이름을 제대로 적으시오.")]
        public string userName { get; set; }

        [Display (Name = "아이디")]
        [Required (ErrorMessage = "아이디를 입력하시오.")]
        [StringLength(25, MinimumLength =3, ErrorMessage = "3자에서 25자 사이로 입력하시오.")]

        public string userId { get; set; }
        
        [Display(Name = "비밀번호")]
        [Required(ErrorMessage = "비밀번호를 입력하시오.")]
        [StringLength(20, MinimumLength =6, ErrorMessage ="비밀번호는 자에서 20자 사이로 입력하시오.")]

        public string password { get; set; }
        [Display(Name ="유저레벨")]
        public string userLevel { get; set; }
    }
}