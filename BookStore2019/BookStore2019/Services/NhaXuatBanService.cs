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
    public class NhaXuatBanService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<ONhaXuatBan> GetAll()
        {
            List<ONhaXuatBan> list = new List<ONhaXuatBan>();
            conn.connect();
            var comm = new SqlCommand("NXB_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<ONhaXuatBan>(dt);

            conn.Close();
            return list;
        }
        public List<ONhaXuatBan> GetAllActive()
        {
            List<ONhaXuatBan> list = new List<ONhaXuatBan>();
            conn.connect();
            var comm = new SqlCommand("NXB_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;


            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<ONhaXuatBan>(dt);

            conn.Close();
            return list;
        }
        public void Add(ONhaXuatBan item)
        {
            conn.connect();
            var comm = new SqlCommand("NXB_Add", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("@TenNXB", SqlDbType.NVarChar).Value = item.TenNXB;
            comm.Parameters.Add(new SqlParameter("@DiaChi", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SDT", item.SDT ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@Email", item.Email ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@GhiChu", item.GhiChu ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@IsActive", item.IsActive ?? (object)DBNull.Value));
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Update(ONhaXuatBan item)
        {
            conn.connect();
            var comm = new SqlCommand("NXB_Update", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("@MaNXB", SqlDbType.Int).Value = item.MaNXB;
            comm.Parameters.Add("@TenNXB", SqlDbType.NVarChar).Value = item.TenNXB;
            comm.Parameters.Add(new SqlParameter("@DiaChi", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SDT", item.SDT ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@Email", item.Email ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@GhiChu", item.GhiChu ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@IsActive", item.IsActive ?? (object)DBNull.Value));
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Delete(int id)
        {
            conn.connect();
            var comm = new SqlCommand("NXB_Delete", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@MaNXB", SqlDbType.Int).Value = id;

            comm.ExecuteNonQuery();
        }
        public ONhaXuatBan Get(int id)
        {
            conn.connect();
            var comm = new SqlCommand("NXB_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@MaNXB", SqlDbType.Int).Value = id;

            DataTable dt = new DataTable();
            ONhaXuatBan nxb = new ONhaXuatBan();
            dt.Load(comm.ExecuteReader());
            nxb = Help.DAL.ConvertDataTable<ONhaXuatBan>(dt).FirstOrDefault();

            return nxb;
        }
    }
}