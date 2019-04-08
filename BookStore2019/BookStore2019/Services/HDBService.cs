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
    public class HDBService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OHoaDonBan> GetAll()
        {
            List<OHoaDonBan> list = new List<OHoaDonBan>();
            conn.connect();
            var comm = new SqlCommand("HDB_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OHoaDonBan>(dt);

            conn.Close();
            return list;
        }
        public void Add(OHoaDonBan item)
        {
            conn.connect();
            var comm = new SqlCommand("HDB_Add", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;
            comm.Parameters.Add("@MaKhach", SqlDbType.Int).Value = item.MaKhach;

            comm.ExecuteNonQuery();
        }
        public void Update(OHoaDonBan item)
        {
            conn.connect();
            var comm = new SqlCommand("HDB_Add", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;

            comm.Parameters.Add("@MaHDB", SqlDbType.Int).Value = item.MaHDB;
            comm.Parameters.Add("@NgayGiao", SqlDbType.DateTime).Value = item.NgayGiao;
            
            comm.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = item.TrangThai;

            comm.ExecuteNonQuery();
        }
        public int GetLastId()
        {
            conn.connect();
            var comm = new SqlCommand("HDB_GetLastId", conn.db);
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
    }
}