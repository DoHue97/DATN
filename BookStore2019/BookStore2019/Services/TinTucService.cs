using BookStore2019.ConnectDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ValuesObject;

namespace BookStore2019.Services
{
    public class TinTucService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OTinTuc> GetAll()
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_GetAll",conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            
            List<OTinTuc> list = new List<OTinTuc>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OTinTuc>(dt);
            conn.Close();
            return list;
        }
        public List<OTinTuc> GetAllByCateShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_GetAllByCateShortName", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = shortname;

            
            List<OTinTuc> list = new List<OTinTuc>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OTinTuc>(dt);
            conn.Close();
            return list;
        }
        public List<OTinTuc> GetAllActive()
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_GetAllAcitve", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            
            List<OTinTuc> list = new List<OTinTuc>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OTinTuc>(dt);

            return list;
        }
        public List<OTinTuc> GetHot()
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_GetHot", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            List<OTinTuc> list = new List<OTinTuc>();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OTinTuc>(dt);

            return list;
        }
        public void Add (OTinTuc item)
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_Add", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@TieuDe", SqlDbType.NVarChar).Value = item.TieuDe;
            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@NoiDung", SqlDbType.NVarChar).Value = item.NoiDung;
            comm.Parameters.Add("@Anh", SqlDbType.NVarChar).Value = item.Anh;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.TrangThai;
            comm.Parameters.Add("@MaLoaiTin", SqlDbType.Int).Value = item.MaLoaiTin;
            
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = item.TenVanTat;

            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Update(OTinTuc item)
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_Update", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("@MaTin", SqlDbType.NVarChar).Value = item.MaTin;
            comm.Parameters.Add("@TieuDe", SqlDbType.NVarChar).Value = item.TieuDe;
            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@NoiDung", SqlDbType.NVarChar).Value = item.NoiDung;
            comm.Parameters.Add("@Anh", SqlDbType.NVarChar).Value = item.Anh;
            comm.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = item.TrangThai;
            comm.Parameters.Add("@MaLoaiTin", SqlDbType.NVarChar).Value = item.MaLoaiTin;
            
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = item.TenVanTat;

            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Delete(OTinTuc item)
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_Update", conn.db);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("@MaTin", SqlDbType.NVarChar).Value = item.MaTin;

            comm.ExecuteNonQuery();
            conn.Close();
        }
        public OTinTuc Get(int id)
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@MaTin", SqlDbType.Int).Value = id;

            
            OTinTuc item = new OTinTuc();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            item = Help.DAL.ConvertDataTable<OTinTuc>(dt).FirstOrDefault();
            conn.Close();
            return item;
        }
        public OTinTuc GetShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_GetShortName", conn.db);
            if (comm == null) return null;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = shortname;

            
            OTinTuc item = new OTinTuc();

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            item = Help.DAL.ConvertDataTable<OTinTuc>(dt).FirstOrDefault();
            conn.Close();
            return item;
        }
    }
}