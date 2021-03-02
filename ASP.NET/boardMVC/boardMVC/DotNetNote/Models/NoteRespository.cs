using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;
using System.Data;
using System.Web.Security;

namespace boardMVC.DotNetNote.Models
{
    public class NoteRespository
    {
        private string str = "Data Source = spectra; User Id = spectra; Password = artceps";

        public NoteRespository()
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
        }
        public int SaveOrUpdate(Note n, BoardWriteFormType formType)
        {
            int r = 0;

            //Connection
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            //Command
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;


            switch (formType)
            {
                case BoardWriteFormType.Write:
                    //글 쓰기 전용
                    
                    cmd.Parameters.Add("p_notes_name", OracleDbType.NVarchar2).Value = n.Name;
                    cmd.Parameters.Add("p_notes_email", OracleDbType.NVarchar2).Value = n.Email;
                    cmd.Parameters.Add("p_notes_title", OracleDbType.NVarchar2).Value = n.Title;
                    cmd.Parameters.Add("p_notes_postip", OracleDbType.NVarchar2).Value = n.PostIp;
                    cmd.Parameters.Add("p_notes_content", OracleDbType.NVarchar2).Value = n.Content;
                    cmd.Parameters.Add("p_notes_password", OracleDbType.NVarchar2).Value = n.Password;
                    cmd.Parameters.Add("p_notes_encoding", OracleDbType.NVarchar2).Value = n.Encoding;
                    cmd.Parameters.Add("p_notes_homepage", OracleDbType.NVarchar2).Value = n.Homepage;
                    cmd.Parameters.Add("p_notes_filename", OracleDbType.NVarchar2).Value = n.FileName;
                    cmd.Parameters.Add("p_notes_filesize", OracleDbType.Int32).Value = n.FileSize;

                    cmd.CommandText = "WriteNotes";
                    cmd.CommandType = CommandType.StoredProcedure;


                    con.Open();
                    r = cmd.ExecuteNonQuery();
                    con.Close();
                    break;

                case BoardWriteFormType.Modify:
                    //수정하기
                    cmd.Parameters.Add("p_notes_name", OracleDbType.NVarchar2).Value = n.Name;
                    cmd.Parameters.Add("p_notes_email", OracleDbType.NVarchar2).Value = n.Email;
                    cmd.Parameters.Add("p_notes_title", OracleDbType.NVarchar2).Value = n.Title;
                    cmd.Parameters.Add("p_notes_modifyIp", OracleDbType.NVarchar2).Value = n.ModifyIp;
                    cmd.Parameters.Add("p_notes_content", OracleDbType.NVarchar2).Value = n.Content;
                    cmd.Parameters.Add("p_notes_password", OracleDbType.NVarchar2).Value = n.Password;
                    cmd.Parameters.Add("p_notes_encoding", OracleDbType.NVarchar2).Value = n.Encoding;
                    cmd.Parameters.Add("p_notes_homepage", OracleDbType.NVarchar2).Value = n.Homepage;
                    cmd.Parameters.Add("p_notes_filename", OracleDbType.NVarchar2).Value = n.FileName;
                    cmd.Parameters.Add("p_notes_filesize", OracleDbType.Int32).Value = n.FileSize;
                    cmd.Parameters.Add("p_notes_id", OracleDbType.Int32).Value = n.Id;

                    cmd.CommandText = "ModifyNote";
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    r = cmd.ExecuteNonQuery();
                    con.Close();
                    break;

                case BoardWriteFormType.Reply:
                    //답변 쓰기
                    cmd.Parameters.Add("p_notes_name", OracleDbType.NVarchar2).Value = n.Name;
                    cmd.Parameters.Add("p_notes_email", OracleDbType.NVarchar2).Value = n.Email;
                    cmd.Parameters.Add("p_notes_title", OracleDbType.NVarchar2).Value = n.Title;
                    cmd.Parameters.Add("p_notes_postIp", OracleDbType.NVarchar2).Value = n.PostIp;
                    cmd.Parameters.Add("p_notes_content", OracleDbType.NVarchar2).Value = n.Content;
                    cmd.Parameters.Add("p_notes_password", OracleDbType.NVarchar2).Value = n.Password;
                    cmd.Parameters.Add("p_notes_encoding", OracleDbType.NVarchar2).Value = n.Encoding;
                    cmd.Parameters.Add("p_notes_homepage", OracleDbType.NVarchar2).Value = n.Homepage;
                    cmd.Parameters.Add("p_notes_parentNum", OracleDbType.Int32).Value = n.ParentNum;
                    cmd.Parameters.Add("p_notes_filename", OracleDbType.NVarchar2).Value = n.FileName;
                    cmd.Parameters.Add("p_notes_filesize", OracleDbType.Int32).Value = n.FileSize;

                    cmd.CommandText = "ReplyNotes";
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    r = cmd.ExecuteNonQuery();
                    con.Close();
                    break;
            }
            return r;
        }

        //게시판 글 쓰기
        public void Add(Note vm)
        {
            try
            {
                SaveOrUpdate(vm, BoardWriteFormType.Write);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        //수정하기
        public int UpdateNote(Note vm)
        {
            int r = 0;
            try
            {
                r = SaveOrUpdate(vm, BoardWriteFormType.Modify);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return r;
        }
        //답변 글쓰기
        public void ReplyNote(Note vm)
        {
            try
            {
              SaveOrUpdate(vm, BoardWriteFormType.Reply);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }
        //게시판 리스트

        public List<Note> GetAll(int in_page)
        {

            Note n = new Note();

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            /*
            cmd.CommandText = string.Format("SELECT id, name, email, title, postdate, readcount, ref, step, reforder, " +
                "answerNum, parentnum, commentcount, FileName, filesize, " +
                "downcount FROM Notes WHERE " +
                "ref BETWEEN {0} * 10 +1 AND ({0}+1) *10 Order by ref desc", in_page);
            */
            cmd.CommandText = string.Format("SELECT * FROM (SELECT ID,NAME, EMAIL, TITLE, Postdate, readcount, ref, step, reforder, answernum, parentnum, commentcount, filename, filesize, downcount, row_number() over (order by ref desc, reforder asc)rownumber from notes) where rownumber between {0}*10+1 and ({0}+1)*10", in_page);
            cmd.CommandType = CommandType.Text;

            /*
                    cmd.CommandText = "ListNotes"; //Cursor의 값이 안나옴 
                    cmd.CommandType = CommandType.StoredProcedure;
             */
            //parameters
            cmd.Parameters.Add("p_page", OracleDbType.Int32).Value = in_page;
            con.Open();
            List<Note> list = new List<Note>();
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Note
                    {
                        Id = Convert.ToInt32(reader[0].ToString()),
                        Name = reader[1].ToString(),
                        Email = reader[2].ToString(),
                        Title = reader[3].ToString(),
                        PostDate = Convert.ToDateTime(reader[4].ToString()),
                        ReadCount = Convert.ToInt32(reader[5].ToString()),
                        Ref = Convert.ToInt32(reader[6].ToString()),
                        Step = Convert.ToInt32(reader[7].ToString()),
                        RefOrder = Convert.ToInt32(reader[8].ToString()),
                        AnswerNum = Convert.ToInt32(reader[9].ToString()),
                        ParentNum = Convert.ToInt32(reader[10].ToString()),
                        CommentCount = Convert.ToInt32(reader[11].ToString()),
                        FileName = reader[12].ToString(),
                        FileSize = Convert.ToInt32(reader[13].ToString()),
                        DownCount = Convert.ToInt32(reader[14].ToString()),
                        Rownumber = Convert.ToInt32(reader[15].ToString())
                        
                    });

                }
                reader.Close();

            }
            con.Close();



            return list.ToList();


        }
        //검색 카운트
        
        public int GetCountBySearch(string SearchField, string SearchQuery)
        {
            int r = 0;

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            if (SearchField == "Name")
            {
                cmd.CommandText = string.Format("Select count(*) from notes where name like '%{0}%'",SearchQuery);

                cmd.CommandType = CommandType.Text;


            }
            else if (SearchField == "Title")
            {
                cmd.CommandText = string.Format("Select count(*) from notes where title like '%{0}%'", SearchQuery);
                cmd.CommandType = CommandType.Text;


            }
            else if (SearchField == "Content")
            {
                cmd.CommandText = string.Format("Select count(*) from notes where name like '%{0}%'", SearchQuery);
                cmd.CommandType = CommandType.Text;

            }
            else
            {
                cmd.CommandText = "Select  id, name, email, title, postdate, readcount, ref, step, reforder, answernum, parentnum, commentcount, filename, filesize, downcount" +
                    "from notes";
                cmd.CommandType = CommandType.Text;

            }
            con.Open();
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    r = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
            }
            con.Close();
            return r;
        }
      
        //Notes 테이블의 모든 record 수
        public int GetCountAll()
        {
            int r = 0;
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select Count(*) from notes";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();


            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    r = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();

            }
            con.Close();

            return r;
        }
        //검색결과 리스트
        public List<Note> GetSearchAll (int page, string SearchField, string SearchQuery)
        {
            List<Note> list = new List<Note>();

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            if (SearchField == "Name")
            {
                cmd.CommandText = string.Format("Select id, name, email, title, postdate,readcount, ref, step, reforder, answernum, parentnum, commentcount, filename, filesize, downcount, rownumber from(select id, name, email, title, postdate, readcount, ref, step, reforder, answernum, parentnum, commentcount, filename, filesize, downcount, row_number() over (order by id desc, reforder asc)rownumber from notes where name like '%{0}%') where rownumber between {1}*10+1 and ({1}+1)*10 order by id desc", SearchQuery, page);

                cmd.CommandType = CommandType.Text;
               

            } else if (SearchField == "Title")
            {
                cmd.CommandText = string.Format("Select id, name, email, title, postdate,readcount, ref, step, reforder, answernum, parentnum, commentcount, filename, filesize, downcount, rownumber from(select id, name, email, title, postdate, readcount, ref, step, reforder, answernum, parentnum, commentcount, filename, filesize, downcount, row_number() over (order by id desc, reforder asc)rownumber from notes where title like '%{0}%') where rownumber between {1}*10+1 and ({1}+1)*10 order by id desc", SearchQuery, page);
                cmd.CommandType = CommandType.Text;


            } else if (SearchField == "Content")
            {
                cmd.CommandText = string.Format("Select id, name, email, title, postdate,readcount, ref, step, reforder, answernum, parentnum, commentcount, filename, filesize, downcount, rownumber from(select id, name, email, title, postdate, readcount, ref, step, reforder, answernum, parentnum, commentcount, filename, filesize, downcount, row_number() over (order by id desc, reforder asc)rownumber from notes where Content like '%{0}%') where rownumber between {1}*10+1 and ({1}+1)*10 order by id desc", SearchQuery, page);
                cmd.CommandType = CommandType.Text;
                
            } else
            {
                cmd.CommandText = "Select  id, name, email, title, postdate, readcount, ref, step, reforder, answernum, parentnum, commentcount, filename, filesize, downcount" +
                    "from notes";
                cmd.CommandType = CommandType.Text;
                
            }
            cmd.Connection = con;
            con.Open();

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    list.Add(new Note
                    {
                        Id = Convert.ToInt32(reader[0].ToString()),
                        Name = reader[1].ToString(),
                        Email = reader[2].ToString(),
                        Title = reader[3].ToString(),
                        PostDate = Convert.ToDateTime(reader[4].ToString()),
                        ReadCount = Convert.ToInt32(reader[5].ToString()),
                        Ref = Convert.ToInt32(reader[6].ToString()),
                        Step = Convert.ToInt32(reader[7].ToString()),
                        RefOrder = Convert.ToInt32(reader[8].ToString()),
                        AnswerNum = Convert.ToInt32(reader[9].ToString()),
                        ParentNum = Convert.ToInt32(reader[10].ToString()),
                        CommentCount = Convert.ToInt32(reader[11].ToString()),
                        FileName = reader[12].ToString(),
                        FileSize = Convert.ToInt32(reader[13].ToString()),
                        DownCount = Convert.ToInt32(reader[14].ToString()),
                        Rownumber = Convert.ToInt32(reader[15].ToString())
                        
                    });
                    
                }
                reader.Close();
            }
            con.Close();

            return list.ToList();
        }
        //Id에 해당하는 파일명 반환
        public string GetFileNameById(int id)
        {
            string result = "";

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Select filename from notes where id = {0}", id);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;


            con.Open();

            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                while (odr.Read())
                {
                    result = odr[0].ToString();
                }
                odr.Close();
            }
            con.Close();

            return result;
        }
        // DownCount 1 증가
        public void UpdateDownCount(string fileName)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Update Notes Set downcount = downcount + 1 where filename = '{0}'", fileName);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("filename", OracleDbType.NVarchar2).Value = fileName;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateDownCountById(int id)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Update Notes Set downcount = downcount +1" +
                "where id = {0}", id);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //상세보기
        public Note GetNoteById(int id)
        {

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = string.Format("Update Notes SET readcount = readcount+1 where id = {0}", id);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
           // cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            con.Open();
            cmd.ExecuteNonQuery();
            
            /*
            cmd.CommandText = "viewNotes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add("p_notes_id", OracleDbType.Int32).Value = id;
            */
            //Display
            cmd.CommandText = string.Format("select * from notes where id = {0}", id);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
           
            Note n = new Note();

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        Console.WriteLine("Nothing");
                    }
                    else
                    {
                        n.Id = Convert.ToInt32(reader[0].ToString());
                    }
                    if (reader.IsDBNull(1))
                    {
                        Console.WriteLine("Nothing");
                    }
                    else
                    {
                        n.Name = reader[1].ToString();
                    }
                    if (reader.IsDBNull(2))
                    {
                        n.Email = reader[2].ToString();
                    }
                    else
                    {
                        n.Email = reader[2].ToString();
                    }
                    if (reader.IsDBNull(3))
                    {
                        Console.WriteLine("Nothing");
                    }
                    else
                    {
                        n.Title = reader[3].ToString();
                    }
                    if (reader.IsDBNull(4))
                    {
                        Console.WriteLine("Nothing");
                    }
                    else
                    {
                        n.PostDate = Convert.ToDateTime(reader[4].ToString());
                    }
                    if (reader.IsDBNull(5))
                    {
                        Console.WriteLine("Nothing");
                    }
                    else
                    {
                        n.PostIp = reader[5].ToString();
                    }
                    if (reader.IsDBNull(6))
                    {
                        n.Content = reader[6].ToString();
                    }
                    else
                    {
                        n.Content = reader[6].ToString();
                    }
                    if (reader.IsDBNull(7))
                    {
                        Console.WriteLine("Nothing");
                    }
                    else
                    {
                        n.Password = reader[7].ToString();
                    }
                    if (reader.IsDBNull(8))
                    {
                        n.ReadCount = Convert.ToInt32(reader[8].ToString());
                    }
                    else
                    {
                        n.ReadCount = Convert.ToInt32(reader[8].ToString());
                    }
                    if (reader.IsDBNull(9))
                    {
                        Console.WriteLine("Nothing");
                    }
                    else
                    {
                        n.Encoding = reader[9].ToString();
                    }
                    if (reader.IsDBNull(10))
                    {
                        n.Homepage = reader[10].ToString();
                    }
                    else
                    {
                        n.Homepage = reader[10].ToString();
                    }
                    if (reader.IsDBNull(11))
                    {
                        n.ModifyIp = reader[11].ToString();
                    }
                    else
                    {
                        n.ModifyIp = reader[11].ToString();
                    }
                    if (reader.IsDBNull(12))
                    {
                        n.FileName = reader[12].ToString();
                    }
                    else
                    {
                        n.FileName = reader[12].ToString();
                    }
                    if (reader.IsDBNull(13))
                    {
                        n.FileSize = Convert.ToInt32(reader[13].ToString());
                    }
                    else
                    {
                        n.FileSize = Convert.ToInt32(reader[13].ToString());
                    }
                    if (reader.IsDBNull(14))
                    {
                        n.DownCount = Convert.ToInt32(reader[14].ToString());
                    }
                    else
                    {
                        n.DownCount = Convert.ToInt32(reader[14].ToString());
                    }
                    if (reader.IsDBNull(15))
                    {
                        Console.WriteLine("Nothing");
                    }
                    else
                    {
                        n.Ref = Convert.ToInt32(reader[15].ToString());
                    }
                    if (reader.IsDBNull(16))
                    {
                        n.Step = Convert.ToInt32(reader[16].ToString());
                    }
                    else
                    {
                        n.Step = Convert.ToInt32(reader[16].ToString());
                    }
                    if (reader.IsDBNull(17))
                    {
                        n.RefOrder = Convert.ToInt32(reader[17].ToString());
                    }
                    else
                    {
                        n.RefOrder = Convert.ToInt32(reader[17].ToString());
                    }
                    if (reader.IsDBNull(18))
                    {
                        n.AnswerNum = Convert.ToInt32(reader[18].ToString());
                    }
                    else
                    {
                        n.AnswerNum = Convert.ToInt32(reader[18].ToString());
                    }
                    if (reader.IsDBNull(19))
                    {
                        n.ParentNum = Convert.ToInt32(reader[19].ToString());
                    }
                    else
                    {
                        n.ParentNum = Convert.ToInt32(reader[19].ToString());
                    }
                    if (reader.IsDBNull(20))
                    {
                        n.Category = reader[20].ToString();
                    }
                    else
                    {
                        n.Category = reader[20].ToString();
                    }
                    if (reader.IsDBNull(21))
                    {
                        n.CommentCount = Convert.ToInt32(reader[21].ToString());
                    }
                    else
                    {
                        n.CommentCount = Convert.ToInt32(reader[21].ToString());
                    }
                    if (reader.IsDBNull(22))
                    {
                        Console.WriteLine("Nothing");
                    }
                    else
                    {
                        n.ModifyDate = Convert.ToDateTime(reader[22].ToString());
                    }
                }
                reader.Close();
            }

            con.Close();

            return n;
        }
        public void adminDelete (int id)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Delete from notes where id = {0}", id);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
        public void Delete (int id, string password)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Delete from notes where id = {0} and password = '{1}'", id, password);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //삭제
        public int DeleteNotes(int id, string password)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "deleteNotes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add("p_notes_id", OracleDbType.Int32).Value = id;
            cmd.Parameters.Add("p_notes_password", OracleDbType.NVarchar2).Value = password;


            con.Open();
            return cmd.ExecuteNonQuery();
            
        }
        //최근 올라온 사진 리스트 4개 출력
        public List<Note> GetNewPhotos()
        {
            List<Note> n = new List<Note>();

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select * from (select row_number() over (order by notes_id desc) rnm, title, filename," +
                "filesize from notes where filename Like '%.png' or filename Like '%.jpg') where rnm <=4";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    n.Add(new Note
                    {
                        Id = Convert.ToInt32(reader[0].ToString()),
                        Title = reader[1].ToString(),
                        FileName = reader[2].ToString(),
                        FileSize = Convert.ToInt32(reader[3].ToString())
                    });
                }
                reader.Close();

            }
            con.Close();
            return n.ToList();
        }
        //최근 글 리스트 
        public List<Note> GetNoteSummaryByCategory(string category)
        {
            List<Note> n = new List<Note>();

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Select * from (select row_number() over (order by id desc) rnm, title, name," +
                "postdate, filename, filesize, readcount, commentcount, step from notes" +
                "where category = '{0}') rnm <=3", category);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();

            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    n.Add(new Note
                    {
                        Id = Convert.ToInt32(reader[0].ToString()),
                        Title = reader[1].ToString(),
                        Name = reader[2].ToString(),
                        PostDate = Convert.ToDateTime(reader[3].ToString()),
                        FileName = reader[4].ToString(),
                        FileSize = Convert.ToInt32(reader[5].ToString()),
                        ReadCount = Convert.ToInt32(reader[6].ToString()),
                        CommentCount = Convert.ToInt32(reader[7].ToString()),
                        Step = Convert.ToInt32(reader[8].ToString())
                    });
                }

                reader.Close();
            }

            con.Close();

            return n.ToList();
        }
        //최근 글 리스트 전체
        public List<Note> GetRecentPosts()
        {
            List<Note> n = new List<Note>();

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Select * from (select row_number () over (order by notes_id desc) rnm, " +
                "Title, name, postdate from notes) where rnm <=3";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    n.Add(new Note
                    {
                        Id = Convert.ToInt32(reader[0].ToString()),
                        Title = reader[1].ToString(),
                        Name = reader[2].ToString(),
                        PostDate = Convert.ToDateTime(reader[3].ToString())
                    });
                }
                reader.Close();
            }

            con.Close();

            return n.ToList();

        }
        //최근 글 리스트 n개
        public List<Note> GetRecentPosts(int numberOfNotes)
        {
            List<Note> n = new List<Note>();

            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("Select * from (select row_number() over (order by id desc) rnm, title," +
                "name, postdate from notes) where rnm <= {0}", numberOfNotes);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    n.Add(new Note
                    {
                        Id = Convert.ToInt32(reader[0].ToString()),
                        Title = reader[1].ToString(),
                        Name = reader[2].ToString(),
                        PostDate = Convert.ToDateTime(reader[3].ToString())
                    });
                }
                reader.Close();
            }

            con.Close();

            return n.ToList();

        }

    }


}

