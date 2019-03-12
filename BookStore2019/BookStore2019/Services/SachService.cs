using BookStore2019.ConnectDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ValuesObject;

namespace BookStore2019.Services
{
    public class SachService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public void Delete(OSach sach)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_Delete", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSach", SqlDbType.Int).Value = sach.MaSach;
            comm.ExecuteNonQuery();
        }
        public void Add(OSach sach)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_Add", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenSach", SqlDbType.NVarChar).Value = sach.TenSach;
            comm.Parameters.Add("@TenSach", SqlDbType.NVarChar).Value = sach.TenSach;
            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = sach.MaChuDe;
            comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = sach.MoTa;
            comm.Parameters.Add("@Anh", SqlDbType.Text).Value = sach.Anh;
            comm.Parameters.Add("@GiaBan", SqlDbType.Decimal).Value = sach.GiaBan;
            comm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = sach.SoLuong;
            comm.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = sach.GhiChu;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(sach.TenSach);
            comm.Parameters.Add("@IdSach", SqlDbType.NVarChar).Value = sach.IdSach;
            comm.Parameters.Add("@IsHot", SqlDbType.Bit).Value = sach.IsHot;
            comm.Parameters.Add("@IsFull", SqlDbType.Bit).Value = sach.IsFull;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = sach.IsActive;
            comm.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = sach.Keyword;
            comm.Parameters.Add("@Sale", SqlDbType.Int).Value = sach.Sale;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = sach.IsSach;

            comm.ExecuteNonQuery();
        }
        public void Update(OSach sach)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_Update", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSach", SqlDbType.Int).Value = sach.MaSach;
            comm.Parameters.Add("@TenSach", SqlDbType.NVarChar).Value = sach.TenSach;
            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = sach.MaChuDe;
            comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = sach.MoTa;
            comm.Parameters.Add("@Anh", SqlDbType.Text).Value = sach.Anh;
            comm.Parameters.Add("@GiaBan", SqlDbType.Decimal).Value = sach.GiaBan;
            comm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = sach.SoLuong;
            comm.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = sach.GhiChu;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(sach.TenSach);
            comm.Parameters.Add("@IdSach", SqlDbType.NVarChar).Value = sach.IdSach;
            comm.Parameters.Add("@IsHot", SqlDbType.Bit).Value = sach.IsHot;
            comm.Parameters.Add("@IsFull", SqlDbType.Bit).Value = sach.IsFull;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = sach.IsActive;
            comm.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = sach.Keyword;
            comm.Parameters.Add("@Sale", SqlDbType.Int).Value = sach.Sale;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = sach.IsSach;

            comm.ExecuteNonQuery();
        }
        public OSach Get(OSach sach)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_Get", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSach", SqlDbType.Int).Value = sach.MaSach;
            SqlDataReader reader = comm.ExecuteReader();
            OSach item = new OSach();
            while (reader.Read())
            {
                item.MaSach = Convert.ToInt32(reader["MaSach"]);
                item.TenSach = reader["TenSach"].ToString();
                item.MaChuDe = Convert.ToInt32(reader["MaChuDe"]);
                item.MoTa = reader["MoTa"].ToString();
                item.Anh = reader["Anh"].ToString();
                item.GiaBan = Decimal.Parse(reader["GiaBan"].ToString());
                item.SoLuong = Convert.ToInt32(reader["SoLuong"]);
                item.GhiChu = reader["GhiChu"].ToString();
                item.TenVanTat = reader["TenVanTat"].ToString();
                item.IdSach = reader["IdSach"].ToString();
                item.IsHot = Boolean.Parse(reader["IsHot"].ToString());
                item.IsFull = Boolean.Parse(reader["IsFull"].ToString());
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.Keyword = reader["Keyword"].ToString();
                item.IsSach = Boolean.Parse(reader["IsSach"].ToString());
                item.TenTacGia = reader["TenTacGia"].ToString();
                item.TenNXB = reader["TenNXB"].ToString();


            }
            return item;
        }
        public List<OSach> GetAll()
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            List<OSach> list = new List<OSach>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                OSach item = new OSach();
                item.MaSach = Convert.ToInt32(dr["MaSach"]);
                item.TenSach = dr["TenSach"].ToString();
                item.MaChuDe = Convert.ToInt32(dr["MaChuDe"]);
                item.MoTa = dr["MoTa"].ToString();
                item.Anh = dr["Anh"].ToString();
                item.GiaBan = Decimal.Parse(dr["GiaBan"].ToString());
                item.SoLuong = Convert.ToInt32(dr["SoLuong"]);
                item.GhiChu = dr["GhiChu"].ToString();
                item.TenVanTat = dr["TenVanTat"].ToString();
                item.IdSach = dr["IdSach"].ToString();
                item.IsHot = Boolean.Parse(dr["IsHot"].ToString());
                item.IsFull = Boolean.Parse(dr["IsFull"].ToString());
                item.IsActive = Boolean.Parse(dr["IsActive"].ToString());
                item.Keyword = dr["Keyword"].ToString();
                item.IsSach = Boolean.Parse(dr["IsSach"].ToString());
                //item.TenTacGia = dr["TenTacGia"].ToString();

                list.Add(item);
            }
            return list;
        }
        public List<OSach> GetAllActive(int startIndex, int length, ref int total)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@StartIndex", SqlDbType.Int).Value = startIndex;
            comm.Parameters.Add("@Length", SqlDbType.Int).Value = length;

            var totalItems = comm.Parameters.Add("@TotalItems", DbType.Int32);
            totalItems.Direction = ParameterDirection.Output;
            if (totalItems.Value != DBNull.Value)
            {
                total = Convert.ToInt32(totalItems.Value);
            }
            List<OSach> list = new List<OSach>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                OSach item = new OSach();
                item.MaSach = Convert.ToInt32(dr["MaSach"]);
                item.TenSach = dr["TenSach"].ToString();
                item.MaChuDe = Convert.ToInt32(dr["MaChuDe"]);
                item.MoTa = dr["MoTa"].ToString();
                item.Anh = dr["Anh"].ToString();
                item.GiaBan = Decimal.Parse(dr["GiaBan"].ToString());
                item.SoLuong = Convert.ToInt32(dr["SoLuong"]);
                item.GhiChu = dr["GhiChu"].ToString();
                item.TenVanTat = dr["TenVanTat"].ToString();
                item.IdSach = dr["IdSach"].ToString();
                item.IsHot = Boolean.Parse(dr["IsHot"].ToString());
                item.IsFull = Boolean.Parse(dr["IsFull"].ToString());
                item.IsActive = Boolean.Parse(dr["IsActive"].ToString());
                item.Keyword = dr["Keyword"].ToString();
                item.IsSach = Boolean.Parse(dr["IsSach"].ToString());
                //item.TenTacGia = dr["TenTacGia"].ToString();

                list.Add(item);
            }
            return list;
        }
        public List<OSach> GetHot(bool isSach)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetAllHot", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;
            List<OSach> list = new List<OSach>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                OSach item = new OSach();
                item.MaSach = Convert.ToInt32(dr["MaSach"]);
                item.TenSach = dr["TenSach"].ToString();
                item.MaChuDe = Convert.ToInt32(dr["MaChuDe"]);
                item.MoTa = dr["MoTa"].ToString();
                item.Anh = dr["Anh"].ToString();
                item.GiaBan = Decimal.Parse(dr["GiaBan"].ToString());
                item.SoLuong = Convert.ToInt32(dr["SoLuong"]);
                item.GhiChu = dr["GhiChu"].ToString();
                item.TenVanTat = dr["TenVanTat"].ToString();
                item.IdSach = dr["IdSach"].ToString();
                item.IsHot = Boolean.Parse(dr["IsHot"].ToString());
                item.IsFull = Boolean.Parse(dr["IsFull"].ToString());
                item.IsActive = Boolean.Parse(dr["IsActive"].ToString());
                item.Keyword = dr["Keyword"].ToString();
                item.IsSach = Boolean.Parse(dr["IsSach"].ToString());
                //item.TenTacGia = dr["TenTacGia"].ToString();

                list.Add(item);
            }
            return list;
        }
        public List<OSach> GetHotByTag(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetAllHotByTag", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = shortname;
            //comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;
            List<OSach> list = new List<OSach>();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                OSach item = new OSach();
                item.MaSach = Convert.ToInt32(dr["MaSach"]);
                item.TenSach = dr["TenSach"].ToString();
                item.MaChuDe = Convert.ToInt32(dr["MaChuDe"]);
                item.MoTa = dr["MoTa"].ToString();
                item.Anh = dr["Anh"].ToString();
                item.GiaBan = Decimal.Parse(dr["GiaBan"].ToString());
                item.SoLuong = Convert.ToInt32(dr["SoLuong"]);
                item.GhiChu = dr["GhiChu"].ToString();
                item.TenVanTat = dr["TenVanTat"].ToString();
                item.IdSach = dr["IdSach"].ToString();
                item.IsHot = Boolean.Parse(dr["IsHot"].ToString());
                item.IsFull = Boolean.Parse(dr["IsFull"].ToString());
                item.IsActive = Boolean.Parse(dr["IsActive"].ToString());
                item.Keyword = dr["Keyword"].ToString();
                item.IsSach = Boolean.Parse(dr["IsSach"].ToString());
                //item.TenTacGia = dr["TenTacGia"].ToString();

                list.Add(item);
            }
            return list;
        }
        public List<OSach> Search(int startIndex, int length, ref int total, string key)
        {
            conn.connect();
            var comm = new SqlCommand("Search_Product", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@StartIndex", SqlDbType.Int).Value = startIndex;
            comm.Parameters.Add("@Length", SqlDbType.Int).Value = length;

            var totalItems = comm.Parameters.Add("@TotalItems", DbType.Int32);
            totalItems.Direction = ParameterDirection.Output;
            if (totalItems.Value != DBNull.Value)
            {
                total = Convert.ToInt32(totalItems.Value);
            }
            comm.Parameters.Add("@Key", SqlDbType.NVarChar).Value = key;
            List<OSach> list = new List<OSach>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                OSach item = new OSach();
                item.MaSach = Convert.ToInt32(dr["MaSach"]);
                item.TenSach = dr["TenSach"].ToString();
                item.MaChuDe = Convert.ToInt32(dr["MaChuDe"]);
                item.MoTa = dr["MoTa"].ToString();
                item.Anh = dr["Anh"].ToString();
                item.GiaBan = Decimal.Parse(dr["GiaBan"].ToString());
                item.SoLuong = Convert.ToInt32(dr["SoLuong"]);
                item.GhiChu = dr["GhiChu"].ToString();
                item.TenVanTat = dr["TenVanTat"].ToString();
                item.IdSach = dr["IdSach"].ToString();
                item.IsHot = Boolean.Parse(dr["IsHot"].ToString());
                item.IsFull = Boolean.Parse(dr["IsFull"].ToString());
                item.IsActive = Boolean.Parse(dr["IsActive"].ToString());
                item.Keyword = dr["Keyword"].ToString();
                item.IsSach = Boolean.Parse(dr["IsSach"].ToString());
                item.TenTacGia = dr["TenTacGia"].ToString();

                list.Add(item);
            }
            return list;
        }
        public List<OSach> GetAllByCate(int startIndex, int length, ref int total, int id)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetAllByCate", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = id;
            comm.Parameters.Add("@StartIndex", SqlDbType.Int).Value = startIndex;
            comm.Parameters.Add("@Length", SqlDbType.Int).Value = length;

            var totalItems = comm.Parameters.Add("@TotalItems", DbType.Int32);
            totalItems.Direction = ParameterDirection.Output;
            if (totalItems.Value != DBNull.Value)
            {
                total = Convert.ToInt32(totalItems.Value);
            }
            
            List<OSach> list = new List<OSach>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                OSach item = new OSach();
                item.MaSach = Convert.ToInt32(dr["MaSach"]);
                item.TenSach = dr["TenSach"].ToString();
                item.MaChuDe = Convert.ToInt32(dr["MaChuDe"]);
                item.MoTa = dr["MoTa"].ToString();
                item.Anh = dr["Anh"].ToString();
                item.GiaBan = Decimal.Parse(dr["GiaBan"].ToString());
                item.SoLuong = Convert.ToInt32(dr["SoLuong"]);
                item.GhiChu = dr["GhiChu"].ToString();
                item.TenVanTat = dr["TenVanTat"].ToString();
                item.IdSach = dr["IdSach"].ToString();
                item.IsHot = Boolean.Parse(dr["IsHot"].ToString());
                item.IsFull = Boolean.Parse(dr["IsFull"].ToString());
                item.IsActive = Boolean.Parse(dr["IsActive"].ToString());
                item.Keyword = dr["Keyword"].ToString();
                item.IsSach = Boolean.Parse(dr["IsSach"].ToString());
                //item.TenTacGia = dr["TenTacGia"].ToString();

                list.Add(item);
            }
            return list;
        }
        public OSach GetByShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetByShortName", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = shortname;
            SqlDataReader reader = comm.ExecuteReader();
            OSach item = new OSach();
            while (reader.Read())
            {
                item.MaSach = Convert.ToInt32(reader["MaSach"]);
                item.TenSach = reader["TenSach"].ToString();
                item.MaChuDe = Convert.ToInt32(reader["MaChuDe"]);
                item.MoTa = reader["MoTa"].ToString();
                item.Anh = reader["Anh"].ToString();
                item.GiaBan = Decimal.Parse(reader["GiaBan"].ToString());
                item.SoLuong = Convert.ToInt32(reader["SoLuong"]);
                item.GhiChu = reader["GhiChu"].ToString();
                item.TenVanTat = reader["TenVanTat"].ToString();
                item.IdSach = reader["IdSach"].ToString();
                item.IsHot = Boolean.Parse(reader["IsHot"].ToString());
                item.IsFull = Boolean.Parse(reader["IsFull"].ToString());
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.Keyword = reader["Keyword"].ToString();
                item.IsSach = Boolean.Parse(reader["IsSach"].ToString());
                
                //item.TenNXB = reader["TenNXB"].ToString();


            }
            return item;
        }
        public List<OTacGia> GetNameTacgia(int masach)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_TacGia_GetName", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSach", SqlDbType.Int).Value = masach;
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
            var comm = new SqlCommand("Image_Sach_GetByMaSach", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSach", SqlDbType.Int).Value = id;
            //comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;
            List<OImageSach> list = new List<OImageSach>();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                OImageSach item = new OImageSach();
                item.ImageId = Convert.ToInt32(dr["IdImage"].ToString());
                item.MoTa = dr["MoTa"].ToString();
                item.DuongDan = dr["DuongDan"].ToString();

                list.Add(item);
            }
            return list;
        }
        public List<OSach> GetOrther(OSach model)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetOrther", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSach", SqlDbType.Int).Value = model.MaSach;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = model.IsSach;
            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = model.MaChuDe;

            SqlDataReader reader = comm.ExecuteReader();
            List<OSach> list = new List<OSach>();
            while (reader.Read())
            {
                OSach item = new OSach();
                item.MaSach = Convert.ToInt32(reader["MaSach"]);
                item.TenSach = reader["TenSach"].ToString();
                item.MaChuDe = Convert.ToInt32(reader["MaChuDe"]);
                item.MoTa = reader["MoTa"].ToString();
                item.Anh = reader["Anh"].ToString();
                item.GiaBan = Decimal.Parse(reader["GiaBan"].ToString());
                item.SoLuong = Convert.ToInt32(reader["SoLuong"]);
                item.GhiChu = reader["GhiChu"].ToString();
                item.TenVanTat = reader["TenVanTat"].ToString();
                item.IdSach = reader["IdSach"].ToString();
                item.IsHot = Boolean.Parse(reader["IsHot"].ToString());
                item.IsFull = Boolean.Parse(reader["IsFull"].ToString());
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.Keyword = reader["Keyword"].ToString();
                item.IsSach = Boolean.Parse(reader["IsSach"].ToString());

                list.Add(item);
            }
            return list;
        }
    }
}