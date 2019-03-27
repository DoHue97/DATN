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
            var comm = new SqlCommand("Sach_Insert", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            comm.Parameters.Add("@TenSach", SqlDbType.NVarChar).Value = sach.TenSach;

            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = sach.MaChuDe;
            comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = sach.MoTa;
            comm.Parameters.Add("@Anh", SqlDbType.NVarChar).Value = sach.Anh;
            comm.Parameters.Add("@GiaBan", SqlDbType.Decimal).Value = sach.GiaBan;
            comm.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = sach.GiaNhap;
            comm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = sach.SoLuong;
            comm.Parameters.Add(new SqlParameter("@GhiChu", sach.GhiChu ?? (object)DBNull.Value));
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(sach.TenVanTat);

            comm.Parameters.Add("@IsHot", SqlDbType.Bit).Value = sach.IsHot;

            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = sach.IsActive;
            comm.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = sach.Keyword;
            comm.Parameters.Add("@Sale", SqlDbType.Int).Value = sach.Sale;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = sach.IsSach;
            comm.Parameters.Add("@MaNXB", SqlDbType.Int).Value = sach.MaNXB;


            comm.Parameters.Add(new SqlParameter("@DichGia", sach.DichGia ?? (object)DBNull.Value));
            //comm.Parameters.Add("@KichThuoc", SqlDbType.NVarChar).Value = sach.KichThuoc;
            comm.Parameters.Add(new SqlParameter("@KichThuoc", sach.KichThuoc ?? (object)DBNull.Value));
            comm.Parameters.Add("@NamXB", SqlDbType.Int).Value = sach.NamXB;
            comm.Parameters.Add(new SqlParameter("@SoTrang", sach.SoTrang ?? (object)DBNull.Value));

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
            comm.Parameters.Add("@Anh", SqlDbType.NVarChar).Value = sach.Anh;
            comm.Parameters.Add("@GiaBan", SqlDbType.Decimal).Value = sach.GiaBan;
            comm.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = sach.GiaNhap;
            comm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = sach.SoLuong;
            comm.Parameters.Add(new SqlParameter("@GhiChu", sach.GhiChu ?? (object)DBNull.Value));
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(sach.TenVanTat);

            comm.Parameters.Add("@IsHot", SqlDbType.Bit).Value = sach.IsHot;

            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = sach.IsActive;
            comm.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = sach.Keyword;
            comm.Parameters.Add("@Sale", SqlDbType.Int).Value = sach.Sale;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = sach.IsSach;
            comm.Parameters.Add("@MaNXB", SqlDbType.Int).Value = sach.MaNXB;


            comm.Parameters.Add("@DichGia", SqlDbType.NVarChar).Value = sach.DichGia;
            //comm.Parameters.Add("@KichThuoc", SqlDbType.NVarChar).Value = sach.KichThuoc;
            comm.Parameters.Add(new SqlParameter("@KichThuoc", sach.KichThuoc ??(object)DBNull.Value));
            comm.Parameters.Add("@NamXB", SqlDbType.Int).Value = sach.NamXB;
            comm.Parameters.Add(new SqlParameter("@SoTrang", sach.SoTrang ?? (object)DBNull.Value));

            comm.ExecuteNonQuery();
        }
        public OSach Get(OSach sach)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_Get", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaSach", SqlDbType.Int).Value = sach.MaSach;
           
            OSach item = new OSach();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            
            item = Help.DAL.ConvertDataTable<OSach>(dt).FirstOrDefault();
            return item;
        }
                
        public List<OSach> GetAll(bool? isSach = true)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            List<OSach> list = new List<OSach>();

            list = Help.DAL.ConvertDataTable<OSach>(dt);
            return list;
        }
        public List<OSach> GetAllActive(int startIndex, int length, ref int total)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            //comm.Parameters.Add("@IsSach", SqlDbType.Bit).Value = isSach;
            comm.Parameters.Add("@StartIndex", SqlDbType.Int).Value = startIndex;
            
            comm.Parameters.Add("@Length", SqlDbType.Int).Value = length;

            comm.Parameters.Add("@TotalItems",SqlDbType.Int).Direction = ParameterDirection.Output;

            List<OSach> list = new List<OSach>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OSach>(dt);
            int output = Convert.ToInt32(comm.Parameters["@TotalItems"].Value);
            total = output;
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
            list = Help.DAL.ConvertDataTable<OSach>(dt);
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
            list = Help.DAL.ConvertDataTable<OSach>(dt);
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
            list = Help.DAL.ConvertDataTable<OSach>(dt);
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

            comm.Parameters.Add("@TotalItems", SqlDbType.Int).Direction = ParameterDirection.Output;

            List<OSach> list = new List<OSach>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OSach>(dt);
            total = Convert.ToInt32(comm.Parameters["@TotalItems"].Value);
            return list;
        }
        public OSach GetByShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_GetByShortName", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = shortname;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            
            OSach item = new OSach();
            item = Help.DAL.ConvertDataTable<OSach>(dt).FirstOrDefault();
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

            List<OSach> list = new List<OSach>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OSach>(dt);
            return list;
        }
    }
}