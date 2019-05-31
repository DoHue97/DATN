using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookStore2019.ConnectDb
{
    public class DatabaseConnect
    {

        //string strConnect = "Server = DESKTOP-H2ET7F5\\SQLEXPRESS; Database = BanSach; User ID = sa;PASSWORD = 1234; Trusted_Connection = True; ";
        string strConnect= ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
        public SqlConnection db;
        public DatabaseConnect()
        {

        }
        public void connect()
        {
            try
            {
                db = new SqlConnection(strConnect);
                db.Open();
            }
            catch (Exception e)
            {

            }
        }
        public void Close()
        {
            db.Close();
        }
    }
}