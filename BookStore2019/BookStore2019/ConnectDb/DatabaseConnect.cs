using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookStore2019.ConnectDb
{
    public class DatabaseConnect
    {
        
        string strConnect = "Server = ADMIN-PC\\SQLEXPRESS; Database = BanSach; User ID = sa;PASSWORD = 123456; Trusted_Connection = True; ";
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