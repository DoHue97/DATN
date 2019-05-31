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
        public List<OHoaDonBan> GetByMaKhach(int makhach,int startIndex, int length, ref int totals)
        {
            List<OHoaDonBan> list = new List<OHoaDonBan>();
            conn.connect();
            var comm = new SqlCommand("HDB_GetByMaKhach", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            if (comm == null) return null;
            comm.Parameters.Add("@MaKhach",SqlDbType.Int).Value=makhach;

            comm.Parameters.Add("@StartIndex", SqlDbType.Int).Value = startIndex;
            comm.Parameters.Add("@Length", SqlDbType.Int).Value = length;
            comm.Parameters.Add("@TotalItems", SqlDbType.Int).Direction = ParameterDirection.Output;

            
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OHoaDonBan>(dt);
            int output = Convert.ToInt32(comm.Parameters["@TotalItems"].Value);
            totals = output;

            //DataTable dt = new DataTable();
            //dt.Load(comm.ExecuteReader());

            //list = Help.DAL.ConvertDataTable<OHoaDonBan>(dt);

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
            
            comm.Parameters.Add("@IsActive", SqlDbType.Int).Value = item.TrangThai;

            comm.ExecuteNonQuery();
        }
        public void Edit(OHoaDonBan item)
        {
            conn.connect();
            var comm = new SqlCommand("HDB_Edit", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;

            comm.Parameters.Add("@MaHDB", SqlDbType.Int).Value = item.MaHDB;
            comm.Parameters.Add("@NgayGiao", SqlDbType.DateTime).Value = item.NgayGiao;

            comm.Parameters.Add("@TrangThai", SqlDbType.Int).Value = item.TrangThai;

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
        public OHoaDonBan GetById(int mahdb)
        {
            conn.connect();
            var comm = new SqlCommand("HDB_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return null;
            comm.Parameters.Add("@mahdb", SqlDbType.Int).Value = mahdb;
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            OHoaDonBan item = new OHoaDonBan();
            item = Help.DAL.ConvertDataTable<OHoaDonBan>(dt).FirstOrDefault();

            conn.Close();
            return item;
        }
    }
}