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
    public class Sach_TacGiaService
    {
        DatabaseConnect conn = new DatabaseConnect();
        
        public void Add(OSach_TacGia item)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_TacGia_Insert", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            if (comm == null) return;
            comm.Parameters.Add("@MaTacGia", SqlDbType.Int).Value = item.MaTacGia;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = item.MaSanPham;
            comm.Parameters.Add(new SqlParameter("@GhiChu", item.GhiChu ?? (object)DBNull.Value));

            comm.ExecuteNonQuery();
        }
        public void Delete(int MaSanPham)
        {
            conn.connect();
            var comm = new SqlCommand("Sach_TacGia_Delete", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = MaSanPham;

            comm.ExecuteNonQuery();
        }
    }
}