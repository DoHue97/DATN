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
        public int SlideId { get; set; }

        [Display(Name = "Tên slide")]
        public string SlideName { get; set; }

        [Display(Name = "Ảnh")]
        public string SlideImage { get; set; }
        [Display(Name = "Mô tả")]
        public string SlideDescription { get; set; }
        [Display(Name = "Trạng thái")]
        public Nullable<bool> IsActive { get; set; }

        public Nullable<int> OrderNo { get; set; }
    }
}
