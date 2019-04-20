using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuesObject
{
   public class OCTHDB
    {
        public int MaHDB { get; set; }
        public int MaSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }

        public string TenSanPham { get; set; }
        public decimal GiaBan { get; set; }
        public string ShortName { get; set; }
    }
}
