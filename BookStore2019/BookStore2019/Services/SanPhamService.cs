using BookStore2019.ConnectDb;
using BookStore2019.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ValuesObject;

namespace BookStore2019.Services
{
    public class SanPhamService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public void Delete(OSanPham sach)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_Delete", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = sach.MaSanPham;
            comm.ExecuteNonQuery();
        }
        public void Add(OSanPham sach)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_Insert", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            comm.Parameters.Add("@TenSanPham", SqlDbType.NVarChar).Value = sach.TenSanPham;

            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = sach.MaChuDe;
            //comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = sach.MoTa;
            comm.Parameters.Add(new SqlParameter("@MoTa", sach.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@Anh", SqlDbType.NVarChar).Value = sach.Anh;
            comm.Parameters.Add("@GiaBan", SqlDbType.Decimal).Value = sach.GiaBan;
            comm.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = sach.GiaNhap;
            comm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = sach.SoLuong;
            comm.Parameters.Add(new SqlParameter("@GhiChu", sach.GhiChu ?? (object)DBNull.Value));
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(sach.TenVanTat);

            comm.Parameters.Add("@IsHot", SqlDbType.Bit).Value = sach.SanPhamHot;

            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = sach.TrangThai;
            comm.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = sach.TuKhoa;
            //comm.Parameters.Add("@Sale", SqlDbType.Int).Value = sach.Sale;
            comm.Parameters.Add(new SqlParameter("@Sale", sach.KhuyenMai ?? (object)DBNull.Value));
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = sach.IsSach;
            //comm.Parameters.Add("@MaNXB", SqlDbType.Int).Value = sach.MaNXB;
            comm.Parameters.Add(new SqlParameter("@MaNXB", sach.MaNXB ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@MaNCC", sach.MaNCC ?? (object)DBNull.Value));

            comm.Parameters.Add(new SqlParameter("@DichGia", sach.DichGia ?? (object)DBNull.Value));
            //comm.Parameters.Add("@KichThuoc", SqlDbType.NVarChar).Value = sach.KichThuoc;
            comm.Parameters.Add(new SqlParameter("@KichThuoc", sach.KichThuoc ?? (object)DBNull.Value));
            //comm.Parameters.Add("@NamXB", SqlDbType.Int).Value = sach.NamXB;
            comm.Parameters.Add(new SqlParameter("@NamXB", sach.NamXB ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SoTrang", sach.SoTrang ?? (object)DBNull.Value));
            comm.Parameters.Add("@NguoiTao", SqlDbType.UniqueIdentifier).Value = sach.NguoiTao;

            comm.ExecuteNonQuery();
        }
        public void Update(OSanPham sach)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_Update", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = sach.MaSanPham;
            comm.Parameters.Add("@TenSanPham", SqlDbType.NVarChar).Value = sach.TenSanPham;

            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = sach.MaChuDe;
            //comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = sach.MoTa;
            comm.Parameters.Add(new SqlParameter("@MoTa", sach.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@Anh", SqlDbType.NVarChar).Value = sach.Anh;
            comm.Parameters.Add("@GiaBan", SqlDbType.Decimal).Value = sach.GiaBan;
            //comm.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = sach.GiaNhap;
            comm.Parameters.Add(new SqlParameter("@GiaNhap", sach.GiaNhap ?? (object)DBNull.Value));
            comm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = sach.SoLuong;
            comm.Parameters.Add(new SqlParameter("@GhiChu", sach.GhiChu ?? (object)DBNull.Value));
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(sach.TenVanTat);

            comm.Parameters.Add("@IsHot", SqlDbType.Bit).Value = sach.SanPhamHot;

            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = sach.TrangThai;
            comm.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = sach.TuKhoa;
            //comm.Parameters.Add("@Sale", SqlDbType.Int).Value = sach.Sale;
            comm.Parameters.Add(new SqlParameter("@Sale", sach.KhuyenMai ?? (object)DBNull.Value));
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = sach.IsSach;
            //comm.Parameters.Add("@MaNXB", SqlDbType.Int).Value = sach.MaNXB;
            comm.Parameters.Add(new SqlParameter("@MaNXB", sach.MaNXB ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@MaNCC", sach.MaNCC ?? (object)DBNull.Value));

            comm.Parameters.Add(new SqlParameter("@DichGia", sach.DichGia ?? (object)DBNull.Value));
            //comm.Parameters.Add("@KichThuoc", SqlDbType.NVarChar).Value = sach.KichThuoc;
            comm.Parameters.Add(new SqlParameter("@KichThuoc", sach.KichThuoc ?? (object)DBNull.Value));
            //comm.Parameters.Add("@NamXB", SqlDbType.Int).Value = sach.NamXB;
            comm.Parameters.Add(new SqlParameter("@NamXB", sach.NamXB ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SoTrang", sach.SoTrang ?? (object)DBNull.Value));

            comm.ExecuteNonQuery();
        }
        public OSanPham Get(OSanPham sach)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_Get", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = sach.MaSanPham;
           
            OSanPham item = new OSanPham();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            
            item = Help.DAL.ConvertDataTable<OSanPham>(dt).FirstOrDefault();
            return item;
        }
                
        public List<OSanPham> GetAll(bool? isSach = true)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            List<OSanPham> list = new List<OSanPham>();

            list = Help.DAL.ConvertDataTable<OSanPham>(dt);
            return list;
        }
        public List<OSanPham> GetAllBook(int startIndex, int length, ref int total)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            //comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;
            comm.Parameters.Add("@StartIndex", SqlDbType.Int).Value = startIndex;
            
            comm.Parameters.Add("@Length", SqlDbType.Int).Value = length;

            comm.Parameters.Add("@TotalItems",SqlDbType.Int).Direction = ParameterDirection.Output;

            List<OSanPham> list = new List<OSanPham>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OSanPham>(dt);
            int output = Convert.ToInt32(comm.Parameters["@TotalItems"].Value);
            total = output;
            return list;
        }
        public List<OSanPham> GetAllDoDung(int startIndex, int length, ref int total)
        {
            conn.connect();
            var comm = new SqlCommand("DoDung_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            //comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;
            comm.Parameters.Add("@StartIndex", SqlDbType.Int).Value = startIndex;

            comm.Parameters.Add("@Length", SqlDbType.Int).Value = length;

            comm.Parameters.Add("@TotalItems", SqlDbType.Int).Direction = ParameterDirection.Output;

            List<OSanPham> list = new List<OSanPham>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OSanPham>(dt);
            int output = Convert.ToInt32(comm.Parameters["@TotalItems"].Value);
            total = output;
            return list;
        }
        public List<OSanPham> GetHot(bool isSach)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_GetAllHot", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;
            List<OSanPham> list = new List<OSanPham>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OSanPham>(dt);
            return list;
        }
        public List<OSanPham> GetHotByTag(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_GetAllHotByTag", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = shortname;
            //comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;
            List<OSanPham> list = new List<OSanPham>();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OSanPham>(dt);
            return list;
        }
        public List<OSanPham> Search(int startIndex, int length, ref int total, string key)
        {
            conn.connect();
            var comm = new SqlCommand("Search_Product", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@StartIndex", SqlDbType.Int).Value = startIndex;
            comm.Parameters.Add("@Length", SqlDbType.Int).Value = length;            
            comm.Parameters.Add("@Key", SqlDbType.NVarChar).Value = key;
            comm.Parameters.Add("@TotalItems", SqlDbType.Int).Direction = ParameterDirection.Output;

            List<OSanPham> list = new List<OSanPham>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OSanPham>(dt);
            int output = Convert.ToInt32(comm.Parameters["@TotalItems"].Value);
            total = output;
            return list;
        }
        public List<OSanPham> GetAllByCate(int startIndex, int length, ref int total, int id)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_GetAllByCate", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = id;
            comm.Parameters.Add("@StartIndex", SqlDbType.Int).Value = startIndex;
            comm.Parameters.Add("@Length", SqlDbType.Int).Value = length;

            comm.Parameters.Add("@TotalItems", SqlDbType.Int).Direction = ParameterDirection.Output;

            List<OSanPham> list = new List<OSanPham>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OSanPham>(dt);
            total = Convert.ToInt32(comm.Parameters["@TotalItems"].Value);
            return list;
        }
        public OSanPham GetByShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_GetByShortName", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = shortname;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            
            OSanPham item = new OSanPham();
            item = Help.DAL.ConvertDataTable<OSanPham>(dt).FirstOrDefault();
            return item;
        }
        public List<OTacGia> GetNameTacgia(int MaSanPham)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_TacGia_GetName", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = MaSanPham;
            List<OTacGia> list = new List<OTacGia>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                OTacGia item = new OTacGia();
                item.MaTacGia = Convert.ToInt32(dr["MaTacGia"].ToString());
                item.Ten = dr["Ten"].ToString();
                item.DienThoai = dr["DienThoai"].ToString();
                item.DiaChi = dr["DiaChi"].ToString();

                list.Add(item);
            }
            return list;
        }
        public List<OImageSach> GetById(int id)
        {
            conn.connect();
            var comm = new SqlCommand("Image_SanPham_GetByMaSanPham", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = id;
            //comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;
            List<OImageSach> list = new List<OImageSach>();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OImageSach>(dt);
            return list;
        }
        public List<OSanPham> GetOrther(OSanPham model)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_GetOrther", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = model.MaSanPham;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = model.IsSach;
            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = model.MaChuDe;

            List<OSanPham> list = new List<OSanPham>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OSanPham>(dt);
            return list;
        }

        public int GetLastId()
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_GetLastId", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return 0;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            int id = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i].Field<int>("LastId");
            }
            return id;
        }
        public bool UpdateQuantity(int masp,int soluong)
        {
            conn.connect();
            var comm = new SqlCommand("SanPham_UpdateQuantity", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return false;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = masp;
            comm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = soluong;
            if (comm.ExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;

        }

        public List<DataCharts> GetByYear(int year)
        {
            conn.connect();
            var comm = new SqlCommand("sp_DoanhThu", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@Year", SqlDbType.Int).Value = year;

            List<DataCharts> list = new List<DataCharts>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<DataCharts>(dt);
            return list;
        }
    }
}