using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuesObject
{
  public class OSlide
    {
        public int MaSlide { get; set; }

        [Display(Name = "Tên slide")]
        public string Ten { get; set; }

        [Display(Name = "Ảnh")]
        public string Anh { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Trạng thái")]
        public Nullable<bool> TrangThai { get; set; }

        public Nullable<int> ThuTu { get; set; }
    }
}
