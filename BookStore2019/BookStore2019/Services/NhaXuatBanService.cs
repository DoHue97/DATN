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
    public class NhaXuatBanService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public List<ONhaXuatBan> GetAll()
        {
            List<ONhaXuatBan> list = new List<ONhaXuatBan>();
            conn.connect();
            var comm = new SqlCommand("NXB_GetAll", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            

            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            list = Help.DAL.ConvertDataTable<ONhaXuatBan>(dt);

            conn.Close();
            return list;
        }
    }
}