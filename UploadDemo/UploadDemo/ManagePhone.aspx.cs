using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;
using System.Data;
using System.Web.Configuration;
using System.IO;
namespace UploadDemo
{
    public partial class ManagePhone : System.Web.UI.Page
    {
        public static string str = "Data Source= spectra; User ID = spectra; Password = artceps;";
        DataSet ds = null;

        public bool SearchMode { get; set; } = false;
        public string SearchField { get; set; }
        public string SearchQuery { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            SearchMode = (!string.IsNullOrEmpty(Request.QueryString["SearchField"]) && !string.IsNullOrEmpty(Request.QueryString["SearchQuery"]));

            if (SearchMode)
            {
                SearchField = Request.QueryString["SearchField"];
                SearchQuery = Request.QueryString["SearchQuery"];

                if (Session["userID"] != null)
                {
                    DisplayData();
                }
                else
                {
                    Response.Redirect("UserLogin.aspx");
                }
            }
            if (Page.IsCallback)
            {

            }

            if (!Page.IsPostBack)
            {
                if (Session["userID"].ToString() == "admin")
                {   //관리자
                    ASPxComboBox1.DataSource = UserSet.Getusers();
                    ASPxComboBox1.ValueField = "UserName";
                    ASPxComboBox1.TextField = "UserName";
                    ASPxComboBox1.DataBind();

                }
                else
                {   //관리자 아닌 유저
                    ASPxComboBox1.DataSource = UserSet.GetIdUsers(Session["userID"].ToString());
                    ASPxComboBox1.ValueField = "UserName";
                    ASPxComboBox1.TextField = "UserName";
                    ASPxComboBox1.DataBind();
                    //관리자 아닌 유저를 comboBox의 초기값으로 불러오는 것
                    ASPxComboBox1.SelectedIndex = 1;
                }


            }


        }
        public class PhoneData
        {
            public int 번호 { get; set; }
            public string 수신인 { get; set; }
            public string 발신인 { get; set; }
            public string 전화번호 { get; set; }
            public string 거래처명 { get; set; }
            public string ServiceLevel { get; set; }
            public string 수신월일 { get; set; }
            public string 수신시간 { get; set; }
            public string 통화종료월일 { get; set; }
            public string 통화종료시간 { get; set; }
            public string 근무외시간여부 { get; set; }
            public string 통화시간 { get; set; }

            public static List<PhoneData> GetSearchPD(string SearchField, string SearchQuery, string originalSearch)
            {

                List<PhoneData> list = new List<PhoneData>();

                OracleConnection con = new OracleConnection(str);
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                if (SearchField == "발신자")
                {
                    cmd.CommandText = string.Format("Select * from phoneData where 발신인 like '%{0}%'", SearchQuery);
                    cmd.CommandType = CommandType.Text;
                }
                else if (SearchField == "수신자")
                {
                    cmd.CommandText = string.Format("Select * from phoneData where 수신인 like '%{0}%' and 발신인 like '%{1}%'", SearchQuery, originalSearch);
                    cmd.CommandType = CommandType.Text;
                }
                else if (SearchField == "거래처명")
                {
                    cmd.CommandText = string.Format("Select * from phoneData where 거래처명 like '%{0}%' and 발신인 like '%{1}%'", SearchQuery, originalSearch);
                    cmd.CommandType = CommandType.Text;
                }
                else
                {
                    cmd.CommandText = "select * from phoneData";
                    cmd.CommandType = CommandType.Text;
                }
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhoneData
                        {
                            번호 = Convert.ToInt32(reader[0].ToString()),
                            수신인 = reader[1].ToString(),
                            발신인 = reader[2].ToString(),
                            전화번호 = reader[3].ToString(),
                            거래처명 = reader[4].ToString(),
                            ServiceLevel = reader[5].ToString(),
                            수신월일 = reader[6].ToString(),
                            수신시간 = reader[7].ToString(),
                            통화종료월일 = reader[8].ToString(),
                            통화종료시간 = reader[9].ToString(),
                            근무외시간여부 = reader[10].ToString(),
                            통화시간 = reader[11].ToString()
                        });
                    }
                    reader.Close();
                }
                con.Close();
                return list.ToList();
            }
            public static List<PhoneData> GetDateDatas(string dateString, string name)
            {

                string query = string.Format("SELECT * FROM PHONEDATa WHERE TO_CHAR(TO_Date(수신월일), 'YYYY-MM') = '{0}' and 발신인 = '{1}'", dateString, name);
                OracleConnection con = new OracleConnection(str);
                OracleCommand cmd = new OracleCommand(query, con);
                List<PhoneData> list = new List<PhoneData>();
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhoneData
                        {
                            번호 = Convert.ToInt32(reader[0].ToString()),
                            수신인 = reader[1].ToString(),
                            발신인 = reader[2].ToString(),
                            전화번호 = reader[3].ToString(),
                            거래처명 = reader[4].ToString(),
                            ServiceLevel = reader[5].ToString(),
                            수신월일 = reader[6].ToString(),
                            수신시간 = reader[7].ToString(),
                            통화종료월일 = reader[8].ToString(),
                            통화종료시간 = reader[9].ToString(),
                            근무외시간여부 = reader[10].ToString(),
                            통화시간 = reader[11].ToString()
                        });

                    }
                    reader.Close();
                }
                con.Close();
                return list.ToList();
            }

            public static List<PhoneData> GetAll(string orignalSearch)
            {
                string query = string.Format("select * from phoneData where 발신인 like '{0}'", orignalSearch);
                OracleConnection con = new OracleConnection(str);
                OracleCommand cmd = new OracleCommand(query, con);
                List<PhoneData> list = new List<PhoneData>();
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhoneData
                        {
                            번호 = Convert.ToInt32(reader[0].ToString()),
                            수신인 = reader[1].ToString(),
                            발신인 = reader[2].ToString(),
                            전화번호 = reader[3].ToString(),
                            거래처명 = reader[4].ToString(),
                            ServiceLevel = reader[5].ToString(),
                            수신월일 = reader[6].ToString(),
                            수신시간 = reader[7].ToString(),
                            통화종료월일 = reader[8].ToString(),
                            통화종료시간 = reader[9].ToString(),
                            근무외시간여부 = reader[10].ToString(),
                            통화시간 = reader[11].ToString()

                        });
                    }
                    reader.Close();
                }
                con.Close();
                return list.ToList();
            }


        }
        public void DisplayData()
        {
            string orignalSearch = Session["valueofSearch"].ToString();
            if (SearchMode == false)
            {
                ASPxGridView1.DataSource = PhoneData.GetAll(orignalSearch);
            }
            else
            {
                ASPxGridView1.DataSource = PhoneData.GetSearchPD(SearchField, SearchQuery, orignalSearch);
            }
            ASPxGridView1.DataBind();

        }

        public class UserSet
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string UserID { get; set; }
            public string UserPassword { get; set; }
            public string UserLevel { get; set; }

            public static List<UserSet> Getusers()
            {
                string query = "Select * from phoneUser";
                OracleConnection con = new OracleConnection(str);
                OracleCommand cmd = new OracleCommand(query, con);
                var list = new List<UserSet>();
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UserSet
                        {
                            Id = Convert.ToInt32(reader[0].ToString()),
                            UserName = reader[1].ToString(),
                            UserID = reader[2].ToString(),
                            UserPassword = reader[3].ToString(),
                            UserLevel = reader[4].ToString()
                        });
                    }
                    reader.Close();
                }
                con.Close();
                return list.ToList();
            }
            public static string FindUsers(string userID)
            {
                string query = string.Format("SELECT userName from phoneUser where userID= '{0}'", userID);
                OracleConnection con = new OracleConnection(str);
                OracleCommand cmd = new OracleCommand(query, con);
                con.Open();
                string userName = "";
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userName = reader[0].ToString();
                    }
                    reader.Close();
                }
                con.Close();
                return userName;
            }
            public static List<UserSet> GetIdUsers(string userID)
            {
                string query = string.Format("Select * from phoneUser where userID = '{0}'", userID);
                OracleConnection con = new OracleConnection(str);
                OracleCommand cmd = new OracleCommand(query, con);
                var list = new List<UserSet>();
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UserSet
                        {
                            Id = Convert.ToInt32(reader[0].ToString()),
                            UserName = reader[1].ToString(),
                            UserID = reader[2].ToString(),
                            UserPassword = reader[3].ToString(),
                            UserLevel = reader[4].ToString()
                        });
                    }
                    reader.Close();
                }
                con.Close();
                return list.ToList();

            }

        }
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            int numberKey = ASPxGridView1.FindVisibleIndexByKeyValue(e.Keys[ASPxGridView1.KeyFieldName]);
            string lblFindReceiver = Convert.ToString(ASPxGridView1.GetRowValues(numberKey, "수신인"));
            string lblFindReceiveCall = Convert.ToString(ASPxGridView1.GetRowValues(numberKey, "수신시간"));
            string lblFindEndCall = Convert.ToString(ASPxGridView1.GetRowValues(numberKey, "통화종료시간"));

            try
            {
                string findID = string.Format("Select 번호 from phoneData where 수신인 = '{0}' and 수신시간='{1}' and 통화종료시간='{2}'", lblFindReceiver, lblFindReceiveCall, lblFindEndCall);
                OracleConnection con = new OracleConnection(str);
                OracleCommand cmd = new OracleCommand(findID, con);
                int id = 0;
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                }
                con.Close();

                string deleteKey = string.Format("delete from phoneData where 번호 = {0}", id);
                cmd = new OracleCommand(deleteKey, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ASPxGridView1.DataBind();
                HttpContext.Current.Response.Redirect("~/ManagePhone.aspx");
            }
            catch (ApplicationException)
            {
                HttpContext.Current.Response.RedirectLocation = System.Web.VirtualPathUtility.ToAbsolute("~/ManagePhone.aspx");
            }

        }
        public void displayCallback()
        {

            string date = Session["dateValue"].ToString();
            if (Session["valueofSearch"] != null)
            {
                ASPxGridView1.DataSource = PhoneData.GetDateDatas(date, Session["valueofSearch"].ToString());
                ASPxGridView1.DataBind();
            }
            else
            {
                string strJs = "<script>alert ('먼저 수신인을 조회해주세요.');location.href='ManagePhone.aspx';</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", strJs);
            }

        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            //Convert the date string
            string date = string.Format("{0:yyyy/MM}", new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(e.Parameter)).ToLocalTime());

            Session["dateValue"] = date;

        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            displayCallback();
        }

        protected void ASPxButton2_Click(object sender, EventArgs e)
        {
            string valueOfDropdownlist = ASPxComboBox1.SelectedItem.Text;
            Session["valueofSearch"] = valueOfDropdownlist;
            string query = string.Format("select * from phoneData where 발신인 = '{0}'", valueOfDropdownlist);

            OracleConnection con = new OracleConnection(str);
            con.Open();
            OracleCommand cmd = new OracleCommand(query, con);
            DataSet ds = new DataSet();

            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            oda.Fill(ds, "phoneData");
            Session["DataSet"] = ds;
            ASPxGridView1.DataSource = ds;
            ASPxGridView1.DataBind();
            con.Close();
        }

        protected void ASPxButton3_Click(object sender, EventArgs e)
        {
            string userName = Session["userID"].ToString();
            string FileDate = Session["dateValue"].ToString();
            string query = string.Format("SELECT * FROM TC_FILES where user_name = '{0}' and TO_CHAR(TO_Date(File_DATE), 'YYYY-MM') = '{1}'", userName, FileDate);
            OracleConnection con = new OracleConnection(str);
            OracleCommand cmd = new OracleCommand(query, con);
            con.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    OracleClob myLob = reader.GetOracleClob(reader.GetOrdinal("FILE_DATA"));
                    if (!myLob.IsNull)
                    {
                        string FileName = reader.GetString(reader.GetOrdinal("FILE_NAME"));

                        //Create file one disk (하드에 파일 만들기)
                        FileStream fs = new FileStream("D:\\ASP.NET\\FILEDOWNLOAD\\" + FileName, FileMode.Create);

                        //Use buffer to transfer data (데이터 전송에 버퍼를 사용한다)
                        byte[] b = new byte[myLob.Length];

                        //Read Data from database (데이터베이스에서 데이터 읽기)
                        myLob.Read(b, 0, (int)myLob.Length);

                        //Write data to file (파일에 데이터를 쓴다)
                        fs.Write(b, 0, (int)myLob.Length);
                        fs.Close();
                    }
                    else
                    {
                        string strJs = "<script>alert('접근 권한이 없습니다.');location.href='ManagePhone.aspx';</script>";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", strJs);
                    }
                }

            }

            finally
            {
                reader.Close();
                con.Close();
            }
        }


    }
}