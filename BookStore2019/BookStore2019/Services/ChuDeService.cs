using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BookStore2019.ConnectDb;
using ValuesObject;
using BookStore2019.Help;
namespace BookStore2019.Services
{
    public class ChuDeService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OChuDe> GetAll()
        {
            List<OChuDe> list = new List<OChuDe>();
            conn.connect();
            var comm = new SqlCommand("ChuDe_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                OChuDe chuDe = new OChuDe();
                chuDe.MaChuDe = Convert.ToInt32(reader["MaChuDe"].ToString());
                chuDe.Ten = reader["Ten"].ToString();
                chuDe.ParentId = Convert.ToInt32(reader["ParentId"]);
                chuDe.GhiChu = reader["GhiChu"].ToString();
                chuDe.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                chuDe.TenVanTat = reader["TenVanTat"].ToString();
                list.Add(chuDe);

            }
            conn.Close();
            return list;            
        }
        public List<OChuDe> GetAllActive()
        {
            List<OChuDe> list = new List<OChuDe>();
            conn.connect();
            var comm = new SqlCommand("ChuDe_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                OChuDe chuDe = new OChuDe();
                chuDe.MaChuDe = Convert.ToInt32(reader["MaChuDe"].ToString());
                chuDe.Ten = reader["Ten"].ToString();
                chuDe.ParentId = Convert.ToInt32(reader["ParentId"]);
                chuDe.GhiChu = reader["GhiChu"].ToString();
                chuDe.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                chuDe.TenVanTat = reader["TenVanTat"].ToString();
                list.Add(chuDe);

            }
            conn.Close();
            return list;
        }
        public List<OChuDe> GetAllByParentId(OChuDe oChuDe)
        {
            List<OChuDe> list = new List<OChuDe>();
            conn.connect();
            var comm = new SqlCommand("ChuDe_GetAllByParent", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@ParentId", SqlDbType.Int).Value = oChuDe.ParentId;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                OChuDe chuDe = new OChuDe();
                chuDe.MaChuDe = Convert.ToInt32(reader["MaChuDe"].ToString());
                chuDe.Ten = reader["Ten"].ToString();
                chuDe.ParentId = Convert.ToInt32(reader["ParentId"]);
                chuDe.GhiChu = reader["GhiChu"].ToString();
                chuDe.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                chuDe.TenVanTat = reader["TenVanTat"].ToString();
                list.Add(chuDe);

            }
            conn.Close();
            return list;
        }
        public List<OChuDe> GetAllByParentName(string name)
        {
            List<OChuDe> list = new List<OChuDe>();
            conn.connect();
            var comm = new SqlCommand("ChuDe_GetAllByParentName", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = name;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                OChuDe chuDe = new OChuDe();
                chuDe.MaChuDe = Convert.ToInt32(reader["MaChuDe"].ToString());
                chuDe.Ten = reader["Ten"].ToString();
                chuDe.ParentId = Convert.ToInt32(reader["ParentId"]);
                chuDe.GhiChu = reader["GhiChu"].ToString();
                chuDe.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                chuDe.TenVanTat = reader["TenVanTat"].ToString();
                list.Add(chuDe);

            }
            conn.Close();
            return list;
        }
        public void Add(OChuDe chuDe)
        {
            conn.connect();
            var comm =new SqlCommand("ChuDe_Add",conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@name", SqlDbType.NVarChar).Value = chuDe.Ten;
            comm.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = chuDe.GhiChu;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = chuDe.IsActive;
            comm.Parameters.Add("@ParentId", SqlDbType.Int).Value = chuDe.ParentId;
            comm.Parameters.Add("@TenVanTat",SqlDbType.NVarChar).Value=Helper.convertToUnSign3(chuDe.Ten);
            SqlDataReader reader = comm.ExecuteReader();
            conn.Close();
        }
        public void Update(OChuDe chuDe)
        {
            conn.connect();
            var comm = new SqlCommand("ChuDe_Edit", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@ma", SqlDbType.Int).Value = chuDe.MaChuDe;
            comm.Parameters.Add("@name", SqlDbType.NVarChar).Value = chuDe.Ten;
            comm.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = chuDe.GhiChu;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = chuDe.IsActive;
            comm.Parameters.Add("@ParentId", SqlDbType.Int).Value = chuDe.ParentId;
            comm.Parameters.Add("@TenVanTat", SqlDbType.NVarChar).Value = Helper.convertToUnSign3(chuDe.Ten);
            comm.ExecuteNonQuery();
            conn.Close();
        }
        public void Delete(OChuDe chuDe)
        {
            conn.connect();
            var comm = new SqlCommand("ChuDe_Delete", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@ma", SqlDbType.Int).Value = chuDe.MaChuDe;
            comm.ExecuteNonQuery();
            conn.Close();
        }
        
        public OChuDe Get(OChuDe chuDe)
        {
            conn.connect();
            var comm = new SqlCommand("ChuDe_Get", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaChuDe", SqlDbType.Int).Value = chuDe.MaChuDe;
            
            SqlDataReader reader = comm.ExecuteReader();
            OChuDe item = new OChuDe();
            while (reader.Read())
            {                
                item.MaChuDe = Convert.ToInt32(reader["MaChuDe"]);
                item.Ten = reader["Ten"].ToString();
                item.GhiChu = reader["GhiChu"].ToString();
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.ParentId = Convert.ToInt32(reader["ParentId"]);
                item.TenVanTat = reader["TenVanTat"].ToString();
            }
            conn.Close();
            return item;
        }
        public List<OChuDe> GetByParentId()
        {
            List<OChuDe> list = new List<OChuDe>();
            conn.connect();
            var comm = new SqlCommand("ChuDe_GetByParentId", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                OChuDe chuDe = new OChuDe();
                chuDe.MaChuDe = Convert.ToInt32(reader["MaChuDe"].ToString());
                chuDe.Ten = reader["Ten"].ToString();
                chuDe.ParentId = Convert.ToInt32(reader["ParentId"]);
                chuDe.GhiChu = reader["GhiChu"].ToString();
                chuDe.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                chuDe.TenVanTat = reader["TenVanTat"].ToString();
                list.Add(chuDe);

            }
            conn.Close();
            return list;
        }
    }
}