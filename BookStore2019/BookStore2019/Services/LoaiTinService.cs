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
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                OLoaiTin loaiTin = new OLoaiTin();
                loaiTin.MaLoaiTin = Convert.ToInt32(reader["MaLoaiTin"].ToString());
                loaiTin.Ten = reader["Ten"].ToString();
                loaiTin.MoTa = reader["MoTa"].ToString() ;
                loaiTin.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                loaiTin.ShortName = reader["ShortName"].ToString();

                list.Add(loaiTin);
            }
            conn.Close();
            return list;
        }
        public List<OLoaiTin> GetAllActive()
        {
            List<OLoaiTin> list = new List<OLoaiTin>();
            conn.connect();
            var comm = new SqlCommand("LoaiTin_GetActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                OLoaiTin loaiTin = new OLoaiTin();
                loaiTin.MaLoaiTin = Convert.ToInt32(reader["MaLoaiTin"].ToString());
                loaiTin.Ten = reader["Ten"].ToString();
                loaiTin.MoTa = reader["MoTa"].ToString();
                loaiTin.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                loaiTin.ShortName = reader["ShortName"].ToString();
                list.Add(loaiTin);

            }
            conn.Close();
            return list;
        }
        public void Add(OLoaiTin item)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTin_Add", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = item.MoTa;
            comm.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = item.Ten;
            comm.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = item.IsActive;
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = item.ShortName;
            comm.ExecuteNonQuery();
            conn.Close();            
        }
        public void Update(OLoaiTin item)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTin_Update", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaLoaiTin", SqlDbType.NVarChar).Value = item.MaLoaiTin;
            comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = item.MoTa;
            comm.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = item.Ten;
            comm.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = item.IsActive;
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = item.ShortName;
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
            conn.Close();
        }
        public OLoaiTin Get(int id)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTin_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            OLoaiTin item = new OLoaiTin();
            comm.Parameters.Add("@MaLoaiTin", SqlDbType.Int).Value = id;

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                item.MaLoaiTin = Convert.ToInt32(reader["MaLoaiTin"].ToString());
                item.Ten = reader["Ten"].ToString();
                item.MoTa = reader["MoTa"].ToString();
                item.ShortName = reader["ShortName"].ToString();
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                
            }
            return item;
        }
        public OLoaiTin GetShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("LoaiTin_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            OLoaiTin item = new OLoaiTin();
            comm.Parameters.Add("@ShortName", SqlDbType.Int).Value = shortname;

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                item.MaLoaiTin = Convert.ToInt32(reader["MaLoaiTin"].ToString());
                item.Ten = reader["Ten"].ToString();
                item.MoTa = reader["MoTa"].ToString();
                item.ShortName = reader["ShortName"].ToString();
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());

            }
            return item;
        }
    }
}