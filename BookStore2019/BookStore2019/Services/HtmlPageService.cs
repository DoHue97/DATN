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
    public class HtmlPageService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OTrangTinh> GetAll()
        {
            List<OTrangTinh> list = new List<OTrangTinh>();
            conn.connect();
            var comm = new SqlCommand("TrangTinh_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OTrangTinh>(dt);

            conn.Close();
            return list;
        }
        public List<OTrangTinh> GetAllActive()
        {
            List<OTrangTinh> list = new List<OTrangTinh>();
            conn.connect();
            var comm = new SqlCommand("TrangTinh_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OTrangTinh>(dt);

            conn.Close();
            return list;
        }
        public void Add(OTrangTinh item)
        {
            conn.connect();
            var comm = new SqlCommand("TrangTinh_Add", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenTrang", SqlDbType.NVarChar).Value = item.TenTrang;
            
            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@Body", SqlDbType.NVarChar).Value = item.Body;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.IsActive;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(item.TenTrang);
            comm.Parameters.Add("@MaLoai", SqlDbType.Int).Value = item.MaLoai;
            SqlDataReader reader = comm.ExecuteReader();

            conn.Close();
        }
        public void Update(OTrangTinh item)
        {
            conn.connect();
            var comm = new SqlCommand("TrangTinh_Update", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaTrangTinh", SqlDbType.Int).Value = item.MaTrangTinh;
            comm.Parameters.Add("@TenTrang", SqlDbType.NVarChar).Value = item.TenTrang;

            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@Body", SqlDbType.NVarChar).Value = item.Body;
            //comm.Parameters.Add("@NgayTao", SqlDbType.DateTime).Value = item.NgayTao;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.IsActive;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Help.Helper.convertToUnSign3(item.TenTrang);
            comm.Parameters.Add("@MaLoai", SqlDbType.Int).Value = item.MaLoai;
            SqlDataReader reader = comm.ExecuteReader();

            conn.Close();
        }
        public OTrangTinh Get(int id)
        {
            conn.connect();
            var comm = new SqlCommand("TrangTinh_Get", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaTrangTinh", SqlDbType.Int).Value = id;


            OTrangTinh item = new OTrangTinh();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            item = Help.DAL.ConvertDataTable<OTrangTinh>(dt).FirstOrDefault();
            conn.Close();
            return item;
        }
        public OTrangTinh GetByShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("TrangTinh_GetByShortName", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = shortname;


            OTrangTinh item = new OTrangTinh();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            item = Help.DAL.ConvertDataTable<OTrangTinh>(dt).FirstOrDefault();
            conn.Close();
            return item;
        }
        public bool Delete(OTrangTinh item)
        {
            conn.connect();
            var comm = new SqlCommand("TrangTinh_Delete", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaTrangTinh", SqlDbType.Int).Value = item.MaTrangTinh;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }


        public List<OTrangTinh> GetByCateId(int cateid)
        {
            List<OTrangTinh> list = new List<OTrangTinh>();
            conn.connect();
            var comm = new SqlCommand("TrangTinh_GetByCateId", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaLoai", SqlDbType.Int).Value = cateid;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OTrangTinh>(dt);

            conn.Close();
            return list;
        }

        public List<OTrangTinh> GetByCateShortName(string shortname)
        {
            List<OTrangTinh> list = new List<OTrangTinh>();
            conn.connect();
            var comm = new SqlCommand("TrangTinh_GetByCateShortName", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = shortname;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OTrangTinh>(dt);

            conn.Close();
            return list;
        }
    }
}