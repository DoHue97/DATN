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
        #region tài khoản
        public OAccount Login(string username, string password)
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
            comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = account.TenDN;
            comm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Help.Helper.EncodeSHA1(account.MatKhau);
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
            var comm = new SqlCommand("Account_ChangePass_Admin", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return false;
            comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = item.TenDN;
            comm.Parameters.Add("@OldPass", SqlDbType.NVarChar).Value = Help.Helper.EncodeSHA1(item.MatKhau);
            comm.Parameters.Add("@NewPass", SqlDbType.NVarChar).Value = Help.Helper.EncodeSHA1(newpass);

            if (comm.ExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }
        public bool Active(OAccount item)
        {
            conn.connect();
            var comm = new SqlCommand("Account_Active", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return false;
            comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = item.TenDN;
            comm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Help.Helper.EncodeSHA1(item.MatKhau);

            if (comm.ExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }
        public OAccount GetById(string matk)
        {
            conn.connect();
            var comm = new SqlCommand("Account_GetByMaTK", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@MaTK", SqlDbType.UniqueIdentifier).Value = Guid.Parse(matk);
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());
            OAccount oAccount = new OAccount();
            oAccount = Help.DAL.ConvertDataTable<OAccount>(dt).FirstOrDefault();
            conn.Close();
            return oAccount;
        }
        public void Add(OAccount account)
        {
            conn.connect();
            var comm = new SqlCommand("Account_Add", conn.db);
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add("@TenDN", SqlDbType.NVarChar).Value = account.TenDN;
            comm.Parameters.Add("@MatKhau", SqlDbType.NVarChar).Value = Help.Helper.EncodeSHA1(account.MatKhau);
            comm.Parameters.Add("@Email", SqlDbType.NVarChar).Value = account.Email;
            comm.Parameters.Add("@MaQuyen", SqlDbType.Int).Value = account.MaQuyen;

            comm.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = account.TrangThai;

            comm.Parameters.Add(new SqlParameter("@MaKhach", account.MaKhach ?? (object)DBNull.Value));

            SqlDataReader reader = comm.ExecuteReader();
            OAccount oAccount = new OAccount();

            conn.Close();
        }
        public bool Lock(OAccount item)
        {
            conn.connect();
            var comm = new SqlCommand("Account_Lock", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return false;
            comm.Parameters.Add("@MaTK", SqlDbType.UniqueIdentifier).Value = item.MaTK;

            if (comm.ExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }
        public bool UnLock(OAccount item)
        {
            conn.connect();
            var comm = new SqlCommand("Account_UnLock", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return false;
            comm.Parameters.Add("@MaTK", SqlDbType.UniqueIdentifier).Value = item.MaTK;

            if (comm.ExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }
        public bool Update(OAccount item)
        {
            conn.connect();
            var comm = new SqlCommand("Account_Update", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return false;
            comm.Parameters.Add("@MaTK", SqlDbType.UniqueIdentifier).Value = item.MaTK;
            comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = item.TenDN;
            comm.Parameters.Add(new SqlParameter("@Email", item.Email ?? (object)DBNull.Value));

            if (comm.ExecuteNonQuery() != 0)
            {
                return true;
            }
            return false;
        }
        #endregion


        #region nhóm quyền
        public List<ONhomQuyen> GetAllRole()
        {
            conn.connect();
            var comm = new SqlCommand("NhomQuyen_GetAll", conn.db);
            comm.CommandType = CommandType.StoredProcedure;
            if (comm == null) return null;
            DataTable dt = new DataTable();
            dt.Load(comm.ExecuteReader());

            List<ONhomQuyen> list = new List<ONhomQuyen>();
            list = Help.DAL.ConvertDataTable<ONhomQuyen>(dt);
            return list;
        }
        #endregion
    }
}