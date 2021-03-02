using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;
using System.Data;

namespace boardMVC.DotNetNote.Models
{
    public class usersrepository
    {
        private string str = "Data Source = spectra; User Id = spectra; Password = artceps";

        public usersrepository()
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
        }

        public void AddUser(string username, string userid, string password)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Insert into loggedin_(user_id, user_uid, user_name, user_password, user_level) values (loggedin_seq.nextval,'{0}','{1}','{2}','200A')", userid, username,password);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public bool CorrectSession (string userid)
        {
            bool result = false;

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "CorrectSession";
            cmd.Parameters.Add("p_user_uid", OracleDbType.NVarchar2).Value = userid;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    result = true;
                }
            }


            return result;
        }
        public string findingLevel (string userId)
        {
            string result = "";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("select user_level from loggedin_ where user_uid = '{0}'", userId);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    result = reader[0].ToString();
                }
            }
            return result;
        }
        public users GetUserByUserId(string userId)
        {
            users user = new users();

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Select * from loggedin_ where users_uid = '{0}'",userId);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    user.Id = Convert.ToInt32(reader[0].ToString());
                    user.userId = reader[1].ToString();
                    user.userName = reader[2].ToString();
                    user.password = reader[3].ToString();
                    user.userLevel = reader[4].ToString();

                }
                reader.Close();
            }
            con.Close();
            return user;
            
        }
        public void adminModify (int id, string userid, string userpw, string level)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            /*  cmd.CommandText = string.Format("update loggedin_ set user_password= '{0}', user_level= '{1}' where user_uid = '{2}' and user_id = {3}", userpw, level, userid, id);
              cmd.CommandType = CommandType.Text;
              */
            cmd.CommandText = "ModifyUsers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Value = id;
            cmd.Parameters.Add("p_user_uid", OracleDbType.NVarchar2).Value = userid;
            cmd.Parameters.Add("p_user_password", OracleDbType.NVarchar2).Value = userpw;
            cmd.Parameters.Add("p_user_level", OracleDbType.NVarchar2).Value = level;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void ModifyUser(int uid, string userid, string userpw, string username)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Update loggedin_ set user_uid = '{0}' and users_pw = '{1}' where users_uid = {2} and users_name = '{3}'", userid, userpw, uid,username);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public bool IsCorrectUser(string userid, string password)
        {
            bool result = false;

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("select * from loggedin_ where user_uid = '{0}' and user_password = '{1}'",userid,password);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = true;
                }
                reader.Close();
            }
            con.Close();
            return result;

        }
    }
}