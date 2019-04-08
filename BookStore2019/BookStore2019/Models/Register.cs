using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore2019.Models
{
    public class Register
    {
        [Required]
        [Display(Name ="Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [MinLength(length: 6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [MinLength(length: 6)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name ="Điện thoại")]
        public string Phone { get; set; }
    }
}