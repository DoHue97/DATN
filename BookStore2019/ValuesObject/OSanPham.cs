using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace ValuesObject
{
    public class OSanPham
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public Nullable<int> MaChuDe { get; set; }
        public string MoTa { get; set; }
        public string Anh { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public Nullable<decimal> GiaNhap { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string GhiChu { get; set; }
        public string TenVanTat { get; set; }
        
        
        public Nullable<bool> IsHot { get; set; }
        
        public Nullable<bool> IsActive { get; set; }
        public string Keyword { get; set; }
        public Nullable<int> Sale { get; set; }
        public Nullable<bool> IsSach { get; set; }
        public Nullable<int> MaNXB { get; set; }
        public string TenTacGia { get; set; }
        public string TenNXB { get; set; }

        public Nullable<int> SoTrang { get; set; }
        public string DichGia { get; set; }
        public string KichThuoc { get; set; }
        public Nullable<int>  NamXB { get; set; }
        public Nullable<int> MaNCC { get; set; }
        public string TenNCC { get; set; }

        public List<OTacGia> ListTacGia { get; set; }
        public int[] MaTacGia { get; set; }
    }
}
