using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore2019.Models
{
    public class Login
    {
        [Required]
        [Display(Name ="Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="Mật khẩu")]
        [DataType(DataType.Password)]
        //[MinLength(length:4)]
        public string Password { get; set; }

        public bool IsRemember { get; set; }
    }
}