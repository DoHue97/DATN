using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuesObject
{
   public class OTrangTinh
    {
        public int MaTrangTinh { get; set; }
        public string TenVanTat { get; set; }
        public string TenTrang { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }

        public string TenLoai { get; set; }
        public int MaLoai { get; set; }
        public string TenLoaiVanTat { get; set; }
    }
}
