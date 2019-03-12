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
            SqlDataReader reader = comm.ExecuteReader();
            List<OTinTuc> list = new List<OTinTuc>();
            while (reader.Read())
            {
                OTinTuc item = new OTinTuc();
                item.MaTin = Convert.ToInt32(reader["MaTin"].ToString());
                item.TieuDe = reader["TieuDe"].ToString();
                item.MoTa = reader["MoTa"].ToString();
                item.NoiDung = reader["NoiDung"].ToString();
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.MaLoaiTin = Convert.ToInt32(reader["MaLoaiTin"].ToString());
                item.NgayTao = DateTime.Parse(reader["NgayTao"].ToString());
                item.TenLoai = reader["TenLoai"].ToString();

                list.Add(item);
            }
            return list;
        }
        public List<OTinTuc> GetAllByCateShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_GetAll", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = shortname;

            SqlDataReader reader = comm.ExecuteReader();
            List<OTinTuc> list = new List<OTinTuc>();
            while (reader.Read())
            {
                OTinTuc item = new OTinTuc();
                item.MaTin = Convert.ToInt32(reader["MaTin"].ToString());
                item.TieuDe = reader["TieuDe"].ToString();
                item.MoTa = reader["MoTa"].ToString();
                item.NoiDung = reader["NoiDung"].ToString();
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.MaLoaiTin = Convert.ToInt32(reader["MaLoaiTin"].ToString());
                item.NgayTao = DateTime.Parse(reader["NgayTao"].ToString());
                item.TenLoai = reader["TenLoai"].ToString();

                list.Add(item);
            }
            return list;
        }
        public List<OTinTuc> GetAllActive()
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_GetAllActive", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = comm.ExecuteReader();
            List<OTinTuc> list = new List<OTinTuc>();
            while (reader.Read())
            {
                OTinTuc item = new OTinTuc();
                item.MaTin = Convert.ToInt32(reader["MaTin"].ToString());
                item.TieuDe = reader["TieuDe"].ToString();
                item.MoTa = reader["MoTa"].ToString();
                item.NoiDung = reader["NoiDung"].ToString();
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.MaLoaiTin = Convert.ToInt32(reader["MaLoaiTin"].ToString());
                item.NgayTao = DateTime.Parse(reader["NgayTao"].ToString());
                item.TenLoai = reader["TenLoai"].ToString();

                list.Add(item);
            }
            return list;
        }
        public void Add (OTinTuc item)
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_Add", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@TieuDe", SqlDbType.NVarChar).Value = item.TieuDe;
            comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = item.MoTa;
            comm.Parameters.Add("@NoiDung", SqlDbType.NVarChar).Value = item.NoiDung;
            comm.Parameters.Add("@Anh", SqlDbType.NVarChar).Value = item.Anh;
            comm.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = item.IsActive;
            comm.Parameters.Add("@MaLoaiTin", SqlDbType.NVarChar).Value = item.MaLoaiTin;
            comm.Parameters.Add("@NgayTao", SqlDbType.NVarChar).Value = item.NgayTao;
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = item.ShortName;

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
            comm.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = item.MoTa;
            comm.Parameters.Add("@NoiDung", SqlDbType.NVarChar).Value = item.NoiDung;
            comm.Parameters.Add("@Anh", SqlDbType.NVarChar).Value = item.Anh;
            comm.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = item.IsActive;
            comm.Parameters.Add("@MaLoaiTin", SqlDbType.NVarChar).Value = item.MaLoaiTin;
            
            comm.Parameters.Add("@ShortName", SqlDbType.NVarChar).Value = item.ShortName;

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

            SqlDataReader reader = comm.ExecuteReader();
            OTinTuc item = new OTinTuc();
            while (reader.Read())
            {
                item.MaTin = Convert.ToInt32(reader["MaTin"].ToString());
                item.TieuDe = reader["TieuDe"].ToString();
                item.MoTa = reader["MoTa"].ToString();
                item.NoiDung = reader["NoiDung"].ToString();
                item.Anh = reader["Anh"].ToString();
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.MaLoaiTin = Convert.ToInt32(reader["MaLoaiTin"].ToString());
                item.NgayTao = DateTime.Parse(reader["NgayTao"].ToString());
                item.ShortName = reader["ShortName"].ToString();
                item.TenLoai = reader["TenLoai"].ToString();
            }
            return item;
        }
        public OTinTuc GetShortName(string shortname)
        {
            conn.connect();
            var comm = new SqlCommand("TinTuc_GetShortName", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@ShortName", SqlDbType.Int).Value = shortname;

            SqlDataReader reader = comm.ExecuteReader();
            OTinTuc item = new OTinTuc();
            while (reader.Read())
            {
                item.MaTin = Convert.ToInt32(reader["MaTin"].ToString());
                item.TieuDe = reader["TieuDe"].ToString();
                item.MoTa = reader["MoTa"].ToString();
                item.NoiDung = reader["NoiDung"].ToString();
                item.Anh = reader["Anh"].ToString();
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.MaLoaiTin = Convert.ToInt32(reader["MaLoaiTin"].ToString());
                item.NgayTao = DateTime.Parse(reader["NgayTao"].ToString());
                item.ShortName = reader["ShortName"].ToString();
                item.TenLoai = reader["TenLoai"].ToString();
            }
            return item;
        }
    }
}