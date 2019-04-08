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
    public class AccountService
    {
        DatabaseConnect conn = new DatabaseConnect();
        public OAccount Login(string username,string password)
        {
            conn.connect();
            var comm = new SqlCommand("Account_Login", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = username;
            comm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Help.Helper.EncodeSHA1(password);
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            OAccount oAccount = new OAccount();
            oAccount = Help.DAL.ConvertDataTable<OAccount>(dt).FirstOrDefault();
            conn.Close();
            return oAccount;
        }
        public OAccount Get(string username, string password)
        {
            conn.connect();
            var comm = new SqlCommand("Account_Get", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = username;
            comm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            OAccount oAccount = new OAccount();
            oAccount = Help.DAL.ConvertDataTable<OAccount>(dt).FirstOrDefault();
            conn.Close();
            return oAccount;
        }
        public OAccount GetByUserName(string username)
        {
            conn.connect();
            var comm = new SqlCommand("Account_GetByUserName", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            if (comm == null) return null;

            comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = username;
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            OAccount oAccount = new OAccount();
            oAccount = Help.DAL.ConvertDataTable<OAccount>(dt).FirstOrDefault();
            conn.Close();
            return oAccount;
        }
        public bool Register(OAccount account)
        {
            conn.connect();
            var comm = new SqlCommand("Account_Register", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = account.UserName;
            comm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Help.Helper.EncodeSHA1(account.PassWord);
            comm.Parameters.Add("@Email", SqlDbType.NVarChar).Value = account.Email;

            comm.Parameters.Add(new SqlParameter("@MaKhach", account.MaKhach ?? (object)DBNull.Value));

            SqlDataReader reader = comm.ExecuteReader();
            OAccount oAccount = new OAccount();
            
            conn.Close();
            return true;
        }
        public List<OAccount> GetAll()
        {
            conn.connect();
            var comm = new SqlCommand("Account_GetAll", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return null;
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            List<OAccount> list = new List<OAccount>();
            list = Help.DAL.ConvertDataTable<OAccount>(dt);
            return list;
        }
        public bool ChangePass(OAccount item, string newpass)
        {
            conn.connect();
            var comm = new SqlCommand("Account_ChangePass", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return false;
            comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = item.UserName;
            comm.Parameters.Add("@OldPass", SqlDbType.NVarChar).Value = Help.Helper.EncodeSHA1(item.PassWord);
            comm.Parameters.Add("@NewPass", SqlDbType.NVarChar).Value = Help.Helper.EncodeSHA1(newpass);
            comm.Parameters.Add(new SqlParameter("@MaKhach",item.MaKhach ?? (object)DBNull.Value));

            if (comm.ExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }
    }
}