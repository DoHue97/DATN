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

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OChuDe>(dt);

            conn.Close();
            return list;            
        }
        public List<OChuDe> GetAllActive()
        {
            List<OChuDe> list = new List<OChuDe>();
            conn.connect();
            var comm = new SqlCommand("ChuDe_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OChuDe>(dt);
            conn.Close();
            return list;
        }
        public List<OChuDe> GetAllByParentId(OChuDe oChuDe)
        {
            List<OChuDe> list = new List<OChuDe>();
            conn.connect();
            var comm = new SqlCommand("ChuDe_GetAllParent", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@ParentId", SqlDbType.Int).Value = oChuDe.ParentId;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OChuDe>(dt);
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
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OChuDe>(dt);
            conn.Close();
            return list;
        }

        public List<OChuDe> GetAllParent()
        {
            List<OChuDe> list = new List<OChuDe>();
            conn.connect();
            var comm = new SqlCommand("ChuDe_GetByParent", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            list = Help.DAL.ConvertDataTable<OChuDe>(dt);
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
            
            
            OChuDe item = new OChuDe();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            item = Help.DAL.ConvertDataTable<OChuDe>(dt).FirstOrDefault();
            conn.Close();
            return item;
        }
        public List<OChuDe> GetByParentId()
        {
            List<OChuDe> list = new List<OChuDe>();
            conn.connect();
            var comm = new SqlCommand("ChuDe_GetByParent", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OChuDe>(dt);
            conn.Close();
            return list;
        }
    }
}