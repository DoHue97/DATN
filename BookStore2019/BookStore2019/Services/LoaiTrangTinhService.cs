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
    public class LoaiTrangTinhService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OLoaiTrangTinh> GetAll()
        {
            List<OLoaiTrangTinh> list = new List<OLoaiTrangTinh>();
            conn.connect();
            var comm = new SqlCommand("LoaiTrangTinh_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OLoaiTrangTinh>(dt);
            conn.Close();
            return list;
        }
        public List<OLoaiTrangTinh> GetAllActive()
        {
            List<OLoaiTrangTinh> list = new List<OLoaiTrangTinh>();
            conn.connect();
            var comm = new SqlCommand("LoaiTrangTinh_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OLoaiTrangTinh>(dt);
            conn.Close();
            return list;
        }
        public void Add(OLoaiTrangTinh item)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTrangTinh_Add", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@TenLoai", SqlDbType.NVarChar).Value = item.TenLoai;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.TrangThai;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(item.TenLoai);
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Update(OLoaiTrangTinh item)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTrangTinh_Add", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaLoai", SqlDbType.Int).Value = item.MaLoai;
            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@TenLoai", SqlDbType.NVarChar).Value = item.TenLoai;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.TrangThai;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(item.TenLoai);
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public OLoaiTrangTinh Get(int id)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTrangTinh_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            OLoaiTrangTinh item = new OLoaiTrangTinh();
            comm.Parameters.Add("@MaLoai", SqlDbType.Int).Value = id;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            item = Help.DAL.ConvertDataTable<OLoaiTrangTinh>(dt).FirstOrDefault();
            conn.Close();
            return item;
        }
        public OLoaiTrangTinh GetByShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTrangTinh_GetByShortName", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            OLoaiTrangTinh item = new OLoaiTrangTinh();
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = shortname;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            item = Help.DAL.ConvertDataTable<OLoaiTrangTinh>(dt).FirstOrDefault();
            conn.Close();
            return item;
        }
        
    }
}