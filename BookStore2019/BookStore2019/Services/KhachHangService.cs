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
    public class KhachHangService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OKhachHang> GetAll()
        {
            List<OKhachHang> list = new List<OKhachHang>();
            conn.connect();
            var comm = new SqlCommand("KhachHang_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OKhachHang>(dt);

            conn.Close();
            return list;
        }
        public void Add(OKhachHang item)
        {
            conn.connect();
            var comm = new SqlCommand("KhachHang_Add", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            if (comm == null) return;
            comm.Parameters.Add("@ten", SqlDbType.NVarChar).Value = item.TenKhach;
            comm.Parameters.Add(new SqlParameter("@diachi", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add("@dienthoai", SqlDbType.NVarChar).Value = item.DienThoai;
            comm.Parameters.Add("@email", SqlDbType.NVarChar).Value = item.Email;

            comm.ExecuteNonQuery();
        }
        public bool Update(OKhachHang item)
        {
            conn.connect();
            var comm = new SqlCommand("KhachHang_Edit", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            if (comm == null) return false;
            comm.Parameters.Add("@ma", SqlDbType.Int).Value = item.MaKhach;
            comm.Parameters.Add("@ten", SqlDbType.NVarChar).Value = item.TenKhach;
            comm.Parameters.Add(new SqlParameter("@diachi", item.DiaChi ?? (object)DBNull.Value));
            comm.Parameters.Add("@dienthoai", SqlDbType.NVarChar).Value = item.DienThoai;
            comm.Parameters.Add("@email", SqlDbType.NVarChar).Value = item.Email;

            if (comm.ExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }
        public int GetLastId()
        {
            conn.connect();
            var comm = new SqlCommand("KhachHang_GetLastId", conn.db);
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
        public OKhachHang GetById(int id)
        {
            conn.connect();
            var comm = new SqlCommand("KhachHang_GetById", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return null;
            comm.Parameters.Add("@MaKhach", SqlDbType.Int).Value = id;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            OKhachHang item = new OKhachHang();
            item = Help.DAL.ConvertDataTable<OKhachHang>(dt).FirstOrDefault();
            return item;
        }
    }
}