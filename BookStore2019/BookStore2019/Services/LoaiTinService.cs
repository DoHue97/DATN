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
    public class LoaiTinService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OLoaiTin> GetAll()
        {
            List<OLoaiTin> list = new List<OLoaiTin>();
            conn.connect();
            var comm = new SqlCommand("LoaiTin_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OLoaiTin>(dt);
            conn.Close();
            return list;
        }
        public List<OLoaiTin> GetAllActive()
        {
            List<OLoaiTin> list = new List<OLoaiTin>();
            conn.connect();
            var comm = new SqlCommand("LoaiTin_GetAllActive", conn.db); 
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OLoaiTin>(dt);
            conn.Close();
            return list;
        }
        public void Add(OLoaiTin item)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTin_Add", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = item.Ten;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.IsActive;
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(item.Ten);
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Update(OLoaiTin item)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTin_Update", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaLoaiTin", SqlDbType.NVarChar).Value = item.MaLoaiTin;
            //comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = item.MoTa;
            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = item.Ten;
            comm.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = item.IsActive;
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(item.Ten);
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Delete(OLoaiTin item)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTin_Add", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaLoaiTin", SqlDbType.NVarChar).Value = item.MaLoaiTin;
            
            comm.ExecuteNonQuery();
            
        }
        public OLoaiTin Get(int id)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTin_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            OLoaiTin item = new OLoaiTin();
            comm.Parameters.Add("@MaLoaiTin", SqlDbType.Int).Value = id;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            item = Help.DAL.ConvertDataTable<OLoaiTin>(dt).FirstOrDefault();
            conn.Close();
            return item;
        }
        public OLoaiTin GetShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTin_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            OLoaiTin item = new OLoaiTin();
            comm.Parameters.Add("@ShortName", SqlDbType.Int).Value = shortname;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            item = Help.DAL.ConvertDataTable<OLoaiTin>(dt).FirstOrDefault();
            conn.Close();
            return item;
        }
    }
}