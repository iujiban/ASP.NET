using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using System.Data;
using System.IO;

using System.Text;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.Web;

using System.Xml;

namespace DEMO
{
    public partial class UploadPopup : System.Web.UI.Page
    {
        public static string ITMSstring = "Data Source= ITMS; User ID = itmsview; Password = itmsview;";
        public static string phoneString = "Data Source = ITMS; User ID = phone; Password = phone;";
        public static int UserKey = 0;
        public static string UserName = string.Empty;
        public static string date = null;
        private string _BaseDir = string.Empty;
        private string xmlPath = string.Empty;
        private bool existed = false;
        private string errorMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    
                    UserKey = Convert.ToInt32(Session["UserKey"].ToString());
                    UserName = findUserName(UserKey);

                    if (!Page.IsCallback)
                    {
                        UploadControl.Visible = false;

                    }
                }
                else
                {
                    UploadControl.Visible = true;
                    Page.Title = "UploadPopUp";
                    
                }
            }
            catch (Exception)
            {
                string strJs = @"<script>window.alert('ITMS 로그인이 필요합니다');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NotAuthorized", strJs);
                Response.End();

            }
        }

        public static string findUserName(int UserKey)
        {
            string result = string.Empty;
            string query = string.Format("Select user_name from ITMS.V_ITMS_USERS1 where User_Key = '{0}'", UserKey);

            OracleConnection con = new OracleConnection(ITMSstring);
            OracleCommand cmd = new OracleCommand(query, con);
            con.Open();

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = reader[0].ToString();
                }
                reader.Close();
            }
            con.Close();

            return result;

        }
        protected void uploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
           
            string FileName = Path.GetFileName(e.UploadedFile.FileName);
            int duration = 0, CallStatus = 0, status = 0;
            string nameValue = string.Empty, phoneNumber = string.Empty;
            DateTime datexml = DateTime.MinValue;
            XmlDocument document = new XmlDocument();

            _BaseDir = Server.MapPath("~/xmlFile/");
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string pathstring = Path.Combine(_BaseDir, UserName);
            Directory.CreateDirectory(pathstring);
            string datePathString = Path.Combine(pathstring, dateTime);
            Directory.CreateDirectory(datePathString);
            try
            {                
                    FileName = FileName.Replace(" ", "");
                    xmlPath = Path.Combine(datePathString, FileName);
                    e.UploadedFile.SaveAs(Path.Combine(datePathString, FileName));
                    Session["xmlPath"] = xmlPath;
                    ViewState["FileName"] = FileName;
                    ViewState["FileSize"] = e.UploadedFile.FileBytes.Length;

                    document.Load(xmlPath);

                    //xml 형식 체크 
                    phoneNumber = document.DocumentElement.ChildNodes[0].Attributes[0].Value;
                    datexml = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(document.DocumentElement.ChildNodes[0].Attributes[2].Value) / 1000).ToLocalTime();
                    CallStatus = Convert.ToInt32(document.DocumentElement.ChildNodes[0].Attributes[3].Value);
                    nameValue = document.DocumentElement.ChildNodes[0].Attributes[4].Value;
                    status = Convert.ToInt32(document.DocumentElement.ChildNodes[0].Attributes[5].Value);
                    duration = Convert.ToInt32(document.DocumentElement.ChildNodes[0].Attributes[6].Value);

                    e.IsValid = true;

            }
            catch (Exception)
            {
                e.IsValid = false;
                System.IO.File.Delete(Session["xmlPath"].ToString());
                Directory.Delete(datePathString, true);
               
            }
        }
        public Boolean ExistUCAUK()
        {
            bool result = false;
            string query = string.Format("Select * from user_check WHERE TO_CHAR(TARGET_DATE, 'yyyy-MM-dd') = '{0}' and INSERT_DATE IS NOT NULL AND DEL_DATE IS NULL and USER_KEY = {1}", date, UserKey);
            OracleConnection con = new OracleConnection(phoneString);
            OracleCommand cmd = new OracleCommand(query, con);

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
        public class SQLDATA
        {
            public int Sequence { get; set; }
            public string Name { get; set; }
            public string email { get; set; }
            public string number { get; set; }
            public string vacationType { get; set; }
            public string Startdate { get; set; }
            public string Enddate { get; set; }
            public string type { get; set; }

            public static List<SQLDATA> getHanBiroDATA(string email)
            {
                List<SQLDATA> list = new List<SQLDATA>();
                try
                {
                    ServiceReference2.Service2Client client = new ServiceReference2.Service2Client();
                    ServiceReference2.HanbiroDemo hanbiro = new ServiceReference2.HanbiroDemo();

                    hanbiro = client.HanbiroGetInfo(email);

                    DataSet ds = new DataSet();
                    ds = hanbiro.HanbiroGetDemo;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SQLDATA sql = new SQLDATA();
                        sql.Startdate = ds.Tables[0].Rows[i]["start_date"].ToString();
                        sql.Enddate = ds.Tables[0].Rows[i]["end_date"].ToString();
                        sql.type = ds.Tables[0].Rows[i]["type"].ToString();
                        list.Add(sql);

                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                return list;
            }
        }

        protected void aspxCallback_Callback(object source, CallbackEventArgs e)
        {

            OracleConnection con = new OracleConnection(phoneString);
            OracleCommand cmd = new OracleCommand();

            date = string.Format("{0:yyyy/MM/01}", new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(e.Parameter)).ToLocalTime());
            Session["date"] = date;
            DateTime dayPick = Convert.ToDateTime(date);
            date = string.Format("{0:yyyy/MM/dd}", dayPick);

            if (ExistUCAUK())
            {
                e.Result = "Existsed";
            }
            else
            {
                try
                {
                    string Targetdate = date;
                    OracleDate TargetDateValue = OracleDate.Parse(Targetdate);

                    cmd.Connection = con;
                    cmd.CommandText = "User_check_insert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_key", OracleDbType.Int32).Value = UserKey;
                    cmd.Parameters.Add("p_Target_date", OracleDbType.Date).Value = TargetDateValue;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    xmlPath = Session["xmlPath"].ToString();
                    existed = false;

                    UploadData(xmlPath);

                    //GROUPWARE 휴가 업데이트
                    string email = Session["email"].ToString();
                    List<SQLDATA> list2 = SQLDATA.getHanBiroDATA(email);
                    string startDate = string.Empty;
                    string endDATE2 = string.Empty;
                    int type = 0;
                    for (int i = 0; i <= list2.Count - 1; i++)
                    {

                        startDate = list2[i].Startdate;
                        endDATE2 = list2[i].Enddate;
                        type = Convert.ToInt32(list2[i].type);

                        DateTime startDatePick = DateTime.ParseExact(startDate, "yyyyMMdd", null);
                        DateTime endDatePick = DateTime.ParseExact(endDATE2, "yyyyMMdd", null);
                        string start = string.Format("{0:yyyy/MM/dd}", startDatePick);
                        string end = string.Format("{0:yyyy/MM/dd}", endDatePick);

                        OracleDate startVacation = OracleDate.Parse(start);
                        OracleDate endVacation = OracleDate.Parse(end);

                        OracleCommand updateCommand = new OracleCommand();
                        updateCommand.Connection = con;

                        updateCommand.CommandText = "updateVacation";
                        updateCommand.CommandType = CommandType.StoredProcedure;

                        updateCommand.Parameters.Add("p_startVacation", OracleDbType.Date).Value = startVacation;
                        updateCommand.Parameters.Add("p_endVaction", OracleDbType.Date).Value = endVacation;
                        updateCommand.Parameters.Add("p_type", OracleDbType.Int32).Value = type;
                        updateCommand.Parameters.Add("p_email", OracleDbType.NVarchar2).Value = email;


                        con.Open();
                        updateCommand.ExecuteNonQuery();
                        con.Close();
                    }

                    //Underbar Report INSERTION
                    OracleCommand reportCMD = new OracleCommand();
                    reportCMD.Connection = con;
                    reportCMD.CommandText = "PhoneData_ReportIn";
                    reportCMD.CommandType = CommandType.StoredProcedure;

                    reportCMD.Parameters.Add("p_user_key", OracleDbType.Int32).Value = UserKey;
                    reportCMD.Parameters.Add("p_Target_Date", OracleDbType.Date).Value = TargetDateValue;

                    con.Open();
                    reportCMD.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception)
                {
                    
                    e.Result = "error";
                                     
                }

            }

        }
       
        public class ERPDATA
        {
            public string TelNo { get; set; }
            public int CustSeq { get; set; }
            public string CustName { get; set; }
            public int PJTSeq { get; set; }
            public string PJTName { get; set; }
            public string SLALevel { get; set; }

            public static List<ERPDATA> getERP(string phoneNumber, string dashPhone)
            {
                List<ERPDATA> list = new List<ERPDATA>();
                try
                {
                    ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                    ServiceReference1.gettestdata g = new ServiceReference1.gettestdata();

                    g = client.GetInfo(phoneNumber, dashPhone);

                    DataSet ds = new DataSet();
                    ds = g.GettingData;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ERPDATA data = new ERPDATA();
                        data.TelNo = ds.Tables[0].Rows[i]["phoneNumber"].ToString();
                        data.CustSeq = Convert.ToInt32(ds.Tables[0].Rows[i]["CustSeq"]);
                        data.CustName = ds.Tables[0].Rows[i]["CustName"].ToString();
                        data.PJTSeq = Convert.ToInt32(ds.Tables[0].Rows[i]["PJTSeq"]);
                        data.PJTName = ds.Tables[0].Rows[i]["PJTName"].ToString();
                        data.SLALevel = ds.Tables[0].Rows[i]["SLALevel"].ToString();
                        list.Add(data);
                    }


                }
                catch (Exception)
                {
                    string msg = "ERP 연결 부분에 문제가 있습니다. 업무 효율화팀에 문의주시기 바랍니다.";

                    Console.WriteLine(msg);

                }

                return list;

            }
        }
        //Making Dashed
        private string Dash(string phoneNumber)
        {
            string Number = string.Empty;
            if (phoneNumber.Substring(0, 2) == "02")
            {
                if (phoneNumber.Length == 9)
                {
                    Number = phoneNumber.Substring(0, 2) + "-" + phoneNumber.Substring(2, 3) + "-" + phoneNumber.Substring(5, 4);
                }
                else
                {
                    Number = phoneNumber.Substring(0, 2) + "-" + phoneNumber.Substring(2, 4) + "-" + phoneNumber.Substring(6, 4);
                }
            }
            else
            {
                if (phoneNumber.Length == 10)
                {
                    Number = phoneNumber.Substring(0, 3) + "-" + phoneNumber.Substring(3, 3) + "-" + phoneNumber.Substring(6, 4);
                }
                else if (phoneNumber.Length == 11)
                {
                    Number = phoneNumber.Substring(0, 3) + "-" + phoneNumber.Substring(3, 4) + "-" + phoneNumber.Substring(7, 4);
                }
                else
                {
                    return phoneNumber;
                }
            }
            return Number;
        }
        private string getITMSEmail(int Key)
        {
            string email = string.Empty;
            OracleConnection con = new OracleConnection(ITMSstring);
            try
            {
                string query = string.Format("select email from ITMS.V_ITMS_USERS1 WHERE user_key= {0}", Key);
                OracleCommand cmd = new OracleCommand(query, con);

                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        email = reader[0].ToString();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return email;

        }
        private void UploadData(string path)
        {
                int duration = 0, CallStatus = 0, status = 0;
                string name = string.Empty, phoneNumber = string.Empty;
                DateTime datexml = DateTime.MinValue;

                XmlDocument document = new XmlDocument();
                document.Load(path);


                for (int i = 0; i <= document.DocumentElement.ChildNodes.Count - 1; i++)
                { 
                    phoneNumber = document.DocumentElement.ChildNodes[i].Attributes[0].Value;
                    datexml = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(document.DocumentElement.ChildNodes[i].Attributes[2].Value) / 1000).ToLocalTime();
                    CallStatus = Convert.ToInt32(document.DocumentElement.ChildNodes[i].Attributes[3].Value);
                    name = document.DocumentElement.ChildNodes[i].Attributes[4].Value;
                    status = Convert.ToInt32(document.DocumentElement.ChildNodes[i].Attributes[5].Value);
                    duration = Convert.ToInt32(document.DocumentElement.ChildNodes[i].Attributes[6].Value);

                    DateTime month = Convert.ToDateTime(date);
                    string comparsionDate = string.Format("{0: yyyy/MM/01}", month);
                    string D = string.Format("{0: yyyy/MM/01}", Convert.ToDateTime(datexml));

                    if (D != comparsionDate)
                    {
                        continue;
                    }
                    else
                    {
                        saveData(name, phoneNumber, datexml, CallStatus, duration, status);
                    }
                }
        }
        private void saveData(string name, string phoneNumber, DateTime datexml, int CallStatus, int duration, int status)
        {
            string Status = string.Empty;
            string resultCall = string.Empty;
            string offTime = string.Empty;
            string query = string.Empty;
            string dashPhone = string.Empty;

            //ERP
            string businessName = string.Empty;
            string phone = string.Empty;
            string ServiceLevel = string.Empty;
            string ERPName = string.Empty;
            string ITMSphoneNumber = string.Empty;
            string TargetDate = string.Empty;

            dashPhone = Dash(phoneNumber);

            DateTime month = Convert.ToDateTime(date);
            string comparsionDate = string.Format("{0: yyyy/MM/01}", month);
            string D = string.Format("{0: yyyy/MM/01}", Convert.ToDateTime(datexml));
            DateTime endDate = datexml.AddSeconds(Convert.ToDouble(duration));

            name = name.Replace("#", string.Empty);

            DateTime dayPick = Convert.ToDateTime(date);
            date = string.Format("{0:yyyy/MM/dd}", dayPick);
            OracleDate od = OracleDate.Parse(date);

            string email = getITMSEmail(UserKey);
            Session["email"] = email;

            //먼저전화를 걸 때는 1 아닐때는 0D
            if (CallStatus == 1)
            {
                //수신
                Status = "수신";

            }
            else if (CallStatus == 2)
            {
                //발신
                Status = "발신";
            }
            else
            {
                // 부재중 (3)
                Status = "부재중";
                duration = 0;
            }
            if (status == 1)
            {
                resultCall = "Y";
            }
            else
            {
                resultCall = "N";
            }

            if ((datexml.DayOfWeek == DayOfWeek.Saturday) || (datexml.DayOfWeek == DayOfWeek.Sunday))
            {
                offTime = 'Y'.ToString();
            }
            else
            {
                offTime = 'N'.ToString();
            }
           
            //ERPDATA
            List<ERPDATA> list = ERPDATA.getERP(phoneNumber, dashPhone);
            if (list.Count > 0)
            {
                businessName = list[0].CustName;
                ServiceLevel = list[0].SLALevel;
            }
            else //ERP폰 번호 조회가 안 됬을 때
            {
                businessName = null;
                ServiceLevel = null;
            }
      

            if (existed == false)
            {

                OracleConnection con = new OracleConnection(phoneString);
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "SaveData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_key", OracleDbType.Int32).Value = UserKey;
                cmd.Parameters.Add("p_name", OracleDbType.NVarchar2).Value = name;
                cmd.Parameters.Add("p_phoneNumber", OracleDbType.NVarchar2).Value = phoneNumber;
                cmd.Parameters.Add("p_receivedate", OracleDbType.Date).Value = datexml;
                cmd.Parameters.Add("p_enddate", OracleDbType.Date).Value = endDate;
                cmd.Parameters.Add("p_offTime", OracleDbType.NVarchar2).Value = offTime;
                cmd.Parameters.Add("p_durationcall", OracleDbType.Int32).Value = duration;
                cmd.Parameters.Add("p_CallStatus", OracleDbType.NVarchar2).Value = Status;
                cmd.Parameters.Add("p_resultCall", OracleDbType.NVarchar2).Value = resultCall;
                cmd.Parameters.Add("p_businessName", OracleDbType.NVarchar2).Value = businessName;
                cmd.Parameters.Add("p_serviceLevel", OracleDbType.NVarchar2).Value = ServiceLevel;
                cmd.Parameters.Add("p_TargetDate", OracleDbType.Date).Value = od;
                cmd.Parameters.Add("p_email", OracleDbType.NVarchar2).Value = email;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                OracleConnection con = new OracleConnection(phoneString);
                OracleCommand cmd = new OracleCommand();

                cmd.Connection = con;
                cmd.CommandText = "XmlSaveData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_key", OracleDbType.Int32).Value = UserKey;
                cmd.Parameters.Add("p_name", OracleDbType.NVarchar2).Value = name;
                cmd.Parameters.Add("p_PhoneNumber", OracleDbType.NVarchar2).Value = phoneNumber;
                cmd.Parameters.Add("p_receivedate", OracleDbType.Date).Value = datexml;
                cmd.Parameters.Add("p_enddate", OracleDbType.Date).Value = endDate;
                cmd.Parameters.Add("p_offTime", OracleDbType.NVarchar2).Value = offTime;
                cmd.Parameters.Add("p_durationcall", OracleDbType.Int32).Value = duration;
                cmd.Parameters.Add("p_CallStatus", OracleDbType.NVarchar2).Value = Status;
                cmd.Parameters.Add("p_resultCall", OracleDbType.NVarchar2).Value = resultCall;
                cmd.Parameters.Add("p_businessName", OracleDbType.NVarchar2).Value = businessName;
                cmd.Parameters.Add("p_serviceLevel", OracleDbType.NVarchar2).Value = ServiceLevel;
                cmd.Parameters.Add("p_TargetDate", OracleDbType.NVarchar2).Value = od;
                cmd.Parameters.Add("p_email", OracleDbType.NVarchar2).Value = email;


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        protected void aspxCallbackNo_Callback(object source, CallbackEventArgs e)
        {
            try
            {
                xmlPath = Session["xmlPath"].ToString();
                OracleDate TargetDateValue = OracleDate.Parse(date);
                OracleConnection con = new OracleConnection(phoneString);
                OracleCommand cmd = new OracleCommand();

                cmd.Connection = con;
                cmd.CommandText = "User_check_DelAUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_key", OracleDbType.Int32).Value = UserKey;
                cmd.Parameters.Add("p_Target_Date", OracleDbType.Date).Value = TargetDateValue;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                existed = true;
                UploadData(xmlPath);

                string email = Session["email"].ToString();
                List<SQLDATA> list2 = SQLDATA.getHanBiroDATA(email);
                string startDate = string.Empty;
                string endDATE2 = string.Empty;
                int type = 0;
                for (int i = 0; i <= list2.Count - 1; i++)
                {

                    startDate = list2[i].Startdate;
                    endDATE2 = list2[i].Enddate;
                    type = Convert.ToInt32(list2[i].type);

                    DateTime startDatePick = DateTime.ParseExact(startDate, "yyyyMMdd", null);
                    DateTime endDatePick = DateTime.ParseExact(endDATE2, "yyyyMMdd", null);
                    string start = string.Format("{0:yyyy/MM/dd}", startDatePick);
                    string end = string.Format("{0:yyyy/MM/dd}", endDatePick);

                    OracleDate startVacation = OracleDate.Parse(start);
                    OracleDate endVacation = OracleDate.Parse(end);

                    OracleCommand updateCommand = new OracleCommand();
                    updateCommand.Connection = con;

                    updateCommand.CommandText = "updateVacation";
                    updateCommand.CommandType = CommandType.StoredProcedure;

                    updateCommand.Parameters.Add("p_startVacation", OracleDbType.Date).Value = startVacation;
                    updateCommand.Parameters.Add("p_endVaction", OracleDbType.Date).Value = endVacation;
                    updateCommand.Parameters.Add("p_type", OracleDbType.Int32).Value = type;
                    updateCommand.Parameters.Add("p_email", OracleDbType.NVarchar2).Value = email;


                    con.Open();
                    updateCommand.ExecuteNonQuery();
                    con.Close();

                   
                }
                //Underbar Report
                OracleCommand reportCMD = new OracleCommand();
                reportCMD.Connection = con;
                reportCMD.CommandText = "PhoneData_ReportInDu";
                reportCMD.CommandType = CommandType.StoredProcedure;

                reportCMD.Parameters.Add("p_user_key", OracleDbType.Int32).Value = UserKey;
                reportCMD.Parameters.Add("p_Target_Date", OracleDbType.Date).Value = TargetDateValue;

                con.Open();
                reportCMD.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception)
            {
                e.Result = "error";

            }
        }
    }
    //testing the hours
    class MyLog
    {
        public static void Write(string value)
        {
            try
            {
                string logDir = string.Empty;
                //logDir = string.Format(@"C:\FPSAdminTest", Directory.GetCurrentDirectory());
                logDir = string.Format(@"C:\templog");
                if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
                string strLogFile = string.Format("{0}\\FPS_{1}.log", logDir, DateTime.Now.ToString("yyyyMMddHH"));

                using (StreamWriter log = new StreamWriter(strLogFile, true, Encoding.Default))  //default = utf8           
                {
                    string datetimetag = DateTime.Now.ToString("HHmmss");
                    datetimetag += "[" + datetimetag + "] " + value;
                    //log.WriteLine(value);
                    log.WriteLine(datetimetag);
                    log.Dispose();
                    log.Close();
                }
            }
            catch (Exception e)
            {
                string s = e.Message.ToString();
            }
            finally
            {
            }
        }
    }
}