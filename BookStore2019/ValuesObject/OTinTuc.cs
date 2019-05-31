using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuesObject
{
   public class OTinTuc
    {
        public int MaTin { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public string Anh { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public int MaLoaiTin { get; set; }
        public DateTime NgayTao { set; get; }
        public string TenLoai { get; set; }
        public string TenVanTat { get; set; }
        public string TenLoaiVanTat { get; set; }
    }
}
