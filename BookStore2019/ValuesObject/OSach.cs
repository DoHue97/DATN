﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuesObject
{
    public class OSach
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public Nullable<int> MaChuDe { get; set; }
        public string MoTa { get; set; }
        public string Anh { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string GhiChu { get; set; }
        public string TenVanTat { get; set; }
        public string IdSach { get; set; }
        public Nullable<int> MaDanhMuc { get; set; }
        public Nullable<bool> IsHot { get; set; }
        public Nullable<bool> IsFull { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Keyword { get; set; }
        public Nullable<int> Sale { get; set; }
        public Nullable<bool> IsSach { get; set; }
        
        public string TenTacGia { get; set; }
        public string TenNXB { get; set; }
    }
}