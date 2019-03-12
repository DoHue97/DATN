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
            SqlDataReader reader = comm.ExecuteReader();
            OAccount oAccount = new OAccount();
            while (reader.Read())
            {
                oAccount.MaTK = Guid.Parse(reader["MaTK"].ToString());
                oAccount.UserName = reader["UserName"].ToString();
                oAccount.PassWord = reader["Password"].ToString();
                oAccount.Email = reader["Email"].ToString();
                oAccount.MaQuyen = Convert.ToInt32(reader["MaQuyen"].ToString());
               // oAccount.MaKhach = Convert.ToInt32(reader["MaKhach"].ToString());
            }
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
            
            SqlDataReader reader = comm.ExecuteReader();
            OAccount oAccount = new OAccount();
            
            conn.Close();
            return true;
        }
    }
}