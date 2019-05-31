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
    public class TacGiaService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OTacGia> GetAll()
        {
            List<OTacGia> list = new List<OTacGia>();
            conn.connect();
            var comm = new SqlCommand("TacGia_GetAll", conn.db);
            
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            if (comm == null) return null;
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OTacGia>(dt);

            conn.Close();
            return list;
        }
        public List<OTacGia> GetAllActive()
        {
            List<OTacGia> list = new List<OTacGia>();
            conn.connect();
            var comm = new SqlCommand("TacGia_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            if (comm == null) return null;
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OTacGia>(dt);

            conn.Close();
            return list;
        }
        public OTacGia Get(int id)
        {
            conn.connect();
            var comm = new SqlCommand("TacGia_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return null;
            comm.Parameters.Add("@MaTacGia", SqlDbType.Int).Value = id;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            OTacGia item = new OTacGia();
            item = Help.DAL.ConvertDataTable<OTacGia>(dt).FirstOrDefault();
            return item;
        }
        public void Add(OTacGia item)
        {
            conn.connect();
            var comm = new SqlCommand("TacGia_Insert", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;
            comm.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = item.Ten;
            comm.Parameters.Add(new SqlParameter("@DiaChi", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@DienThoai", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(item.Ten);
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.TrangThai;
            comm.ExecuteNonQuery();
        }
        public void Update(OTacGia item)
        {
            conn.connect();
            var comm = new SqlCommand("TacGia_Update", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;

            comm.Parameters.Add("@MaTacGia", SqlDbType.Int).Value = item.MaTacGia;
            comm.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = item.Ten;
            comm.Parameters.Add(new SqlParameter("@DiaChi", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@DienThoai", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(item.Ten);
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.TrangThai;
            comm.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            conn.connect();
            var comm = new SqlCommand("TacGia_Delete", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;
            comm.Parameters.Add("@MaTacGia", SqlDbType.Int).Value = id;

            comm.ExecuteNonQuery();
        }
        public List<OTacGia> GetByMaSanPham(int MaSanPham)
        {
            List<OTacGia> list = new List<OTacGia>();
            conn.connect();
            var comm = new SqlCommand("Sach_TacGia_GetName", conn.db);
            
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            if (comm == null) return null;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = MaSanPham;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OTacGia>(dt);

            conn.Close();
            return list;
        }
        public OTacGia GetByShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("TacGia_GetByShortName", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return null;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = shortname;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            OTacGia item = new OTacGia();
            item = Help.DAL.ConvertDataTable<OTacGia>(dt).FirstOrDefault();
            return item;
        }
    }
}