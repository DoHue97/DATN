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
            var comm = new SqlCommand("Slide_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                OSlide slide = new OSlide();
                slide.SlideId = Convert.ToInt32(reader["SlideId"].ToString());
                slide.SlideName = reader["SlideName"].ToString();
                slide.SlideImage = reader["SlideImage"].ToString();
                slide.SlideDescription = reader["SlideDescription"].ToString();
                slide.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                slide.OrderNo = Convert.ToInt32(reader["OrderNo"].ToString());
                list.Add(slide);

            }
            conn.Close();
            return list;
        }
        public List<OSlide> GetAllActive()
        {
            List<OSlide> list = new List<OSlide>();
            conn.connect();
            var comm = new SqlCommand("Slide_GetAllActive", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                OSlide slide = new OSlide();
                slide.SlideId = Convert.ToInt32(reader["SlideId"].ToString());
                slide.SlideName = reader["SlideName"].ToString();
                slide.SlideImage = reader["SlideImage"].ToString();
                slide.SlideDescription = reader["SlideDescription"].ToString();
                slide.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                slide.OrderNo = Convert.ToInt32(reader["OrderNo"].ToString());

                list.Add(slide);

            }
            conn.Close();
            return list;
        }

        public void Add(OSlide slide)
        {
            conn.connect();
            var comm = new SqlCommand("Slide_Add", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@SlideName", SqlDbType.NVarChar).Value = slide.SlideName;
            comm.Parameters.Add("@SlideImage", SqlDbType.NVarChar).Value = slide.SlideImage;
            comm.Parameters.Add("@SlideDescription", SqlDbType.NVarChar).Value = slide.SlideDescription;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = slide.IsActive;
            comm.Parameters.Add("@OrderNo", SqlDbType.Int).Value = slide.OrderNo;
            

            SqlDataReader reader = comm.ExecuteReader();

        }
        public void Update(OSlide slide)
        {
            conn.connect();
            var comm = new SqlCommand("Slide_Update", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@SlideId", SqlDbType.NVarChar).Value = slide.SlideId;
            comm.Parameters.Add("@SlideName", SqlDbType.NVarChar).Value = slide.SlideName;
            comm.Parameters.Add("@SlideImage", SqlDbType.NVarChar).Value = slide.SlideImage;
            comm.Parameters.Add("@SlideDescription", SqlDbType.NVarChar).Value = slide.SlideDescription;
            comm.Parameters.Add("@IsActive", SqlDbType.Bit).Value = slide.IsActive;
            comm.Parameters.Add("@OrderNo", SqlDbType.Int).Value = slide.OrderNo;

            comm.ExecuteNonQuery();
        }
        public void Delete(OSlide slide)
        {
            conn.connect();
            var comm = new SqlCommand("Slide_Delete", conn.db);
            comm.Parameters.Add("@SlideId", SqlDbType.Int).Value = slide.SlideId;
            comm.ExecuteNonQuery();
        }

        public OSlide Get(OSlide slide)
        {
            conn.connect();
            var comm = new SqlCommand("Slide_Get", conn.db);
            comm.Parameters.Add("@SlideId", SqlDbType.Int).Value = slide.SlideId;
            SqlDataReader reader = comm.ExecuteReader();
            OSlide item = new OSlide();
            while (reader.Read())
            {
                item.SlideId = Convert.ToInt32(reader["SlideId"].ToString());
                item.SlideName = reader["SlideName"].ToString();
                item.SlideImage = reader["SlideImage"].ToString();
                item.SlideDescription = reader["SlideDescription"].ToString();
                item.IsActive = Boolean.Parse(reader["IsActive"].ToString());
                item.OrderNo = Convert.ToInt32(reader["OrderNo"].ToString());

            }
            return item;
        }
    }
}