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
    public class CTHDBService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OCTHDB> GetAll(int id)
        {
            List<OCTHDB> list = new List<OCTHDB>();
            conn.connect();
            var comm = new SqlCommand("CTHDB_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            if (comm == null) return null;

            comm.Parameters.Add("@MaHDB", SqlDbType.Int).Value = id;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OCTHDB>(dt);

            conn.Close();
            return list;
        }
        public void Add(OCTHDB item)
        {
            conn.connect();
            var comm = new SqlCommand("CTHDB_Add", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;
            comm.Parameters.Add("@MaHDB", SqlDbType.Int).Value = item.MaHDB;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = item.MaSanPham;
            comm.Parameters.Add("@SoLuong", SqlDbType.Int).Value = item.SoLuong;
            comm.Parameters.Add("@ThanhTien", SqlDbType.Decimal).Value = item.ThanhTien;
           
            comm.ExecuteNonQuery();
        }
        public bool Delete(OCTHDB item)
        {
            conn.connect();
            var comm = new SqlCommand("CTHDB_Delete", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return false;
            comm.Parameters.Add("@MaHDB", SqlDbType.Int).Value = item.MaHDB;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = item.MaSanPham;
            if (comm.ExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }
    }
}