using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuesObject
{
   public class OHoaDonBan
    {
        public int MaHDB { get; set; }
        public Nullable<System.DateTime> NgayBan { get; set; }
        public Nullable<int> MaKhach { get; set; }
        public string GhiChu { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }
    }
}
