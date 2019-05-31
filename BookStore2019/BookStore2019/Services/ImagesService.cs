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
    public class ImagesService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OImageSach> GetAll(int masp)
        {
            List<OImageSach> list = new List<OImageSach>();
            conn.connect();
            var comm = new SqlCommand("Image_SanPham_GetByMaSanPham", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            if (comm == null) return null;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = masp;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OImageSach>(dt);

            conn.Close();
            return list;
        }
        public void Add(OImageSach item)
        {
            conn.connect();
            var comm = new SqlCommand("Image_SanPham_Add", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;
            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@DuongDan", SqlDbType.NVarChar).Value = item.DuongDan;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = item.MaSanPham;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.TrangThai;

            comm.ExecuteNonQuery();
        }
        public void Update(OImageSach item)
        {
            conn.connect();
            var comm = new SqlCommand("Image_SanPham_Update", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return;
            comm.Parameters.Add(new SqlParameter("@MoTa", item.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add("@DuongDan", SqlDbType.NVarChar).Value = item.DuongDan;
            comm.Parameters.Add("@MaSanPham", SqlDbType.Int).Value = item.MaSanPham;
            comm.Parameters.Add("@IdImage", SqlDbType.Int).Value = item.MaAnh;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = item.TrangThai;

            comm.ExecuteNonQuery();
        }
        public bool Delete(int id)
        {
            conn.connect();
            var comm = new SqlCommand("Image_SanPham_Delete", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return false;
            comm.Parameters.Add("@IdImage", SqlDbType.Int).Value = id;
            if (comm.ExecuteNonQuery() != 0)
                return true;
            return false;
        }
        public OImageSach Get(OImageSach item)
        {
            conn.connect();
            var comm = new SqlCommand("Image_SanPham_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return null;
            comm.Parameters.Add("@IdImage", SqlDbType.Int).Value = item.MaAnh;

            DataTable dt = new DataTable();
            OImageSach model = new OImageSach();
            dt.Load(comm.ExecuteReader());
            model = Help.DAL.ConvertDataTable<OImageSach>(dt).FirstOrDefault();
            return model;
        }
    }
}