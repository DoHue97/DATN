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
    public class SlideService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<OSlide> GetAll()
        {
            List<OSlide> list = new List<OSlide>();
            conn.connect();
            var comm = new SqlCommand("Slide_GetAllSlide", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OSlide>(dt);
            conn.Close();
            return list;
        }
        public List<OSlide> GetAllActive()
        {
            List<OSlide> list = new List<OSlide>();
            conn.connect();
            var comm = new SqlCommand("Slide_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<OSlide>(dt);
            conn.Close();
            return list;
        }

        public void Add(OSlide slide)
        {
            conn.connect();
            var comm = new SqlCommand("Slide_Insert", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@SlideName", slide.Ten ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SlideDescription", slide.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SlideImage", slide.Anh ?? (object)DBNull.Value));
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = slide.TrangThai;
            comm.Parameters.Add("@OrderNo", SqlDbType.Int).Value = slide.ThuTu;
            

            SqlDataReader reader = comm.ExecuteReader();

        }
        public void Update(OSlide slide)
        {
            conn.connect();
            var comm = new SqlCommand("Slide_Update", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@SlideId", SqlDbType.NVarChar).Value = slide.MaSlide;
            comm.Parameters.Add(new SqlParameter("@SlideName", slide.Ten ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SlideDescription", slide.MoTa ?? (object)DBNull.Value));
            comm.Parameters.Add(new SqlParameter("@SlideImage", slide.Anh ?? (object)DBNull.Value));
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = slide.TrangThai;
            comm.Parameters.Add("@OrderNo", SqlDbType.Int).Value = slide.ThuTu;

            comm.ExecuteNonQuery();
        }
        public void Delete(OSlide slide)
        {
            conn.connect();
            var comm = new SqlCommand("Slide_Delete", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@SlideId", SqlDbType.Int).Value = slide.MaSlide;
            comm.ExecuteNonQuery();
        }

        public OSlide Get(int id)
        {
            conn.connect();
            var comm = new SqlCommand("Slide_Get", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("@SlideId", SqlDbType.Int).Value = id;
            
            OSlide item = new OSlide();
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            item = Help.DAL.ConvertDataTable<OSlide>(dt).FirstOrDefault();
            return item;
        }
    }
}