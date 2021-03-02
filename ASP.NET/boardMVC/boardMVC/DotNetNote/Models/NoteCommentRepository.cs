using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;


namespace boardMVC.DotNetNote.Models
{
    public class NoteCommentRepository
    {
        private string str = "Data Source = spectra; User Id = spectra; Password = artceps";
        public NoteCommentRepository()
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
        }
        //특정 게시물에 댓글 추가
        public void AddNoteComment(NoteComment model)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "AddNoteComment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.Add("p_boardid", OracleDbType.Int32).Value = model.BoardId;
            cmd.Parameters.Add("p_name", OracleDbType.NVarchar2).Value = model.Name;
            cmd.Parameters.Add("p_opinion", OracleDbType.NVarchar2).Value = model.Opinion;
            cmd.Parameters.Add("p_password", OracleDbType.NVarchar2).Value = model.Password;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //특정 게시물에 해당하는 댓글 리스트
        public List<NoteComment> GetNoteComments(int boardId)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = String.Format("SELECT * FROM NOTECOMMENTS WHERE BOARDID = {0}", boardId);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("boardid", OracleDbType.NVarchar2).Value = boardId;
            cmd.Connection = con;

            con.Open();
            List<NoteComment> list = new List<NoteComment>();
            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                while (odr.Read())
                {
                    list.Add(new NoteComment
                    {
                        Id = Convert.ToInt32(odr[0].ToString()),
                        BoardName = odr[1].ToString(),
                        BoardId = Convert.ToInt32(odr[2].ToString()),
                        Name = odr[3].ToString(),
                        Opinion = odr[4].ToString(),
                        PostDate = Convert.ToDateTime(odr[5].ToString()),
                        Password = odr[6].ToString()
                    });

                }

                odr.Close();

            }
            con.Close();

            return list.ToList();
        }
        //특정 게시물의 특정 Id에 해당하는 댓글 카운트
        public int GetCountBy(int boardid, int id, string password)
        {
            int count = 0;

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = String.Format("SELECT COUNT(*) FROM NOTECOMMENTS WHERE BOARDID = {0} AND ID = {1} AND PASSWORD = '{2}'", boardid, id, password);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("boardid", OracleDbType.Int32).Value = boardid;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
            cmd.Parameters.Add("password", OracleDbType.NVarchar2).Value = password;

            cmd.Connection = con;
            con.Open();

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    count = Convert.ToInt32(reader[0].ToString());
                }

                reader.Close();
                con.Close();
            }
            con.Close();

            return count;
        }
        //댓글 삭제
        public void DeleteNoteComment(int boardid, int id, string password)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            //Delete
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = String.Format("Delete NoteComments where boardid = {0} and id = {1} and password = '{2}'", boardid, id, password);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();


            cmd.Parameters.Add("boardid", OracleDbType.Int32).Value = boardid;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
            cmd.Parameters.Add("password", OracleDbType.NVarchar2).Value = password;

            cmd.ExecuteNonQuery();

            //Update CommentCount
            cmd.CommandText = string.Format("Update Notes Set commentcount = commentcount -1 where id = {0}", id);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            con.Close();
        }

        //최근 댓글 리스트 전체
        public List<NoteComment> GetRecentComments()
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Select * from (select row_number() over (order by notes_id desc) rnm, boardid, opinion, postdate from notecomments) where rnm <=3";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            List<NoteComment> list = new List<NoteComment>();

            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                while (odr.Read())
                {
                    list.Add(new NoteComment
                    {
                        Id = Convert.ToInt32(odr[0].ToString()),
                        BoardId = Convert.ToInt32(odr[1].ToString()),
                        Opinion = odr[2].ToString(),
                        PostDate = Convert.ToDateTime(odr[3].ToString())
                    });
                }
                odr.Close();
            }
            
            con.Close();
            return list.ToList();

        }
    }
}