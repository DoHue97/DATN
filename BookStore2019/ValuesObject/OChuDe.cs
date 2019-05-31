using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuesObject
{
   public class OChuDe
    {
        public int MaChuDe { get; set; }
        
        public string Ten { get; set; }
        public string GhiChu { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public int MaChuDeCha { get; set; }
        public string TenVanTat { get; set; }
    }
}
