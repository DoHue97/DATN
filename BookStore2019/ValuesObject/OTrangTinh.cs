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
        public string MaTrang { get; set; }
        public string TenTrang { get; set; }
        public string MoTa { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
