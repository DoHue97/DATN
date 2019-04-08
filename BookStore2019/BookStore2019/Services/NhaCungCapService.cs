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
    public class NhaCungCapService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<ONhaCungCap> GetAll()
        {
            List<ONhaCungCap> list = new List<ONhaCungCap>();
            conn.connect();
            var comm = new SqlCommand("NCC_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;


            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<ONhaCungCap>(dt);

            conn.Close();
            return list;
        }
        public List<ONhaCungCap> GetAllActive()
        {
            List<ONhaCungCap> list = new List<ONhaCungCap>();
            conn.connect();
            var comm = new SqlCommand("NCC_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;


            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<ONhaCungCap>(dt);

            conn.Close();
            return list;
        }
        public void Add(ONhaCungCap item)
        {
            conn.connect();
            var comm = new SqlCommand("NCC_Add", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("@TenNCC", SqlDbType.NVarChar).Value = item.TenNCC;
            comm.Parameters.Add(new SqlParameter("@DiaChi", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SDT", item.SDT ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@IsActive", item.IsActive ?? (object)DBNull.Value));
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Update(ONhaCungCap item)
        {
            conn.connect();
            var comm = new SqlCommand("NCC_Update", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("@MaNCC", SqlDbType.Int).Value = item.MaNCC;
            comm.Parameters.Add("@TenNCC", SqlDbType.NVarChar).Value = item.TenNCC;
            comm.Parameters.Add(new SqlParameter("@DiaChi", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SDT", item.SDT ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@IsActive", item.IsActive ?? (object)DBNull.Value));
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Delete(int id)
        {
            conn.connect();
            var comm = new SqlCommand("NCC_Delete", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@MaNCC", SqlDbType.Int).Value = id;

            comm.ExecuteNonQuery();
        }
        public ONhaCungCap Get(int id)
        {
            conn.connect();
            var comm = new SqlCommand("NCC_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@MaNCC", SqlDbType.Int).Value = id;

            DataTable dt = new DataTable();
            ONhaCungCap NCC = new ONhaCungCap();
            dt.Load(comm.ExecuteReader());
            NCC = Help.DAL.ConvertDataTable<ONhaCungCap>(dt).FirstOrDefault();

            return NCC;
        }
    }
}