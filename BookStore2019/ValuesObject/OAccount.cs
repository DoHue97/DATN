﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuesObject
{
    public class OAccount
    {
        public System.Guid MaTK { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public Nullable<int> MaQuyen { get; set; }
        public Nullable<int> MaKhach { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string TenQuyen { get; set; }
        public string TenKhach { get; set; }

        public string NewPassword { get; set; }
    }
}
