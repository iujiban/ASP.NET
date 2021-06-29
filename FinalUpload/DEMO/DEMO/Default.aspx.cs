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
using CodeDLL;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat;

using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.Web;
using System.Xml.Linq;
using System.Configuration;

namespace DEMO
{
    public partial class Default : System.Web.UI.Page
    {
        public static string ITMSstring = "Data Source= ITMS; User ID = itmsview; Password = itmsview;";
        public static string phoneString = "Data Source = ITMS; User ID = phone; Password = phone;";
        public static ConnectionStringSettings cts = ConfigurationManager.ConnectionStrings["ConnectionStrings"];
        public static string changedNameValue = null;
        public static string date = null;
        public static int UserKey = 0;
        public static bool userStatement = false;
        public static bool adminStatement = false;
        public static bool adminTeamStatement = false;
        public static string UserName = string.Empty;
        public static string receiveDate = null;
        public static string endDate = null;
        private string _BaseDir = string.Empty;
        private string xmlPath = string.Empty;
        public static int adminType = 0;

        public static int adminAuthority(int UserKey)
        {
            int admin_type = 0;
            string query = string.Format("SELECT admin_type FROM ITMS.V_ITMS_ADMIN WHERE USER_KEY = {0}", UserKey);
            OracleConnection con = new OracleConnection(ITMSstring);
            try
            {
                OracleCommand cmd = new OracleCommand(query, con);
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        admin_type = Convert.ToInt32(reader[0]);
                    }
                }
                return admin_type;

            }
            catch (Exception)
            {
                admin_type = 0;
            }
            finally
            {
                con.Close();
            }
            return admin_type;

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                try
                {
                    //라이선스
                    Code code = new Code("charlie");

                    // ITMS

                    string decodeKey = Request.QueryString["UK"];
                    //string test = code.EncryptEncode("MsPvDlSinFrnR1UGp%2fR41hd%2bxy1BnroijjNnh15YlDw%3d", "itms");
                    // string temp = HttpUtility.UrlEncode(decodeKey);
                    //string tempKey = code.DecryptDecode(decodeKey, "itms");

                    string tempKey2 = EncryptDecryptCode.DecryptString(decodeKey, "itms");
                    string[] parts = tempKey2.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != 2)
                    {
                        string strJs = @"<script>window.alert('권한이 없습니다.');</script>";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "notAuthorized", strJs);
                        Response.End();
                    }
                    else
                    {
                        Session["UserKey"] = parts[0];
                        string dayofyear = parts[1];
                        if (dayofyear != DateTime.Now.DayOfYear.ToString())
                        {
                            string strJs = @"<script>window.alert('권한이 없습니다.');</script>";
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "NotAuthority", strJs);
                            Response.End();
                        }
                        else
                        {
                            UserKey = Convert.ToInt32(Session["UserKey"].ToString());
                        }
                    }

                    adminType = adminAuthority(UserKey);

                    if (adminType < 4 && adminType >= 1)
                    {
                        adminStatement = true;
                    }
                    else if (adminType == 4)
                    {

                        adminTeamStatement = true;
                    }
                    else
                    {
                        userStatement = true;
                    }

                    //Focusing
                    DateTime Today = DateTime.Now;
                    string today = string.Format("{0:yyyy-MM}", Today);
                    dateEdit.Text = today;
                    Session["dateEdit"] = dateEdit.Text;
                    UserName = findUserName(UserKey);
                    ASPxLabel1.Text = "(" + UserName + "님" + ")";

                    if (userStatement)
                    {
                        Name_Combobox.SelectedIndex = 0;
                        Name_Combobox.DataSource = UserSet.FindUsers(UserKey);
                        Name_Combobox.ValueField = "USER_NAME";
                        Name_Combobox.TextField = "USER_NAME";
                        Name_Combobox.DataBind();
                    }
                    else if (adminStatement)
                    {
                        Name_Combobox.SelectedIndex = -1;
                        Name_Combobox.DataSource = UserSet.Getusers();
                        Name_Combobox.ValueField = "USER_NAME";
                        Name_Combobox.TextField = "USER_NAME";
                        Name_Combobox.DataBind();

                    }

                    else if (adminTeamStatement)
                    {
                        Name_Combobox.SelectedIndex = 0;
                        Name_Combobox.DataSource = UserSet.FindTeamUsers(UserKey);
                        Name_Combobox.ValueField = "USER_NAME";
                        Name_Combobox.TextField = "USER_NAME";
                        Name_Combobox.DataBind();
                    }

                    else
                    {
                        string strJs = @"<script type>window.alert('RegisterClientScriptBlock');</script>";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "권한이 없습니다.", strJs);
                    }

                }
                catch (Exception)
                {
                    string strJs = @"<script type'>window.alert('권한이없습니다');</script>";
                    Response.Write(strJs);

                    for (int i = 0; i < gridView.Columns.Count; i++)
                    {

                        gridView.Columns[i].Visible = false;
                    }

                    ASPxFormLayout1_E1.Enabled = false;

                }
            }

            try
            {
                if (IsCallback)
                {
                    string query = string.Empty;
                    //관리자들
                    if (adminStatement)
                    {
                        int userKey = findUserKey(Name_Combobox.Text);
                        Session["findUser"] = userKey;
                        if (changedNameValue != null)
                        {
                            if (date != null || dateEdit.Text != string.Empty)
                            {
                                if (receiveDateEdit.Text != string.Empty)
                                {
                                    if (endDateEdit.Text != string.Empty)
                                    {
                                        gridView.DataSourceID = "SqlDataSource2";
                                        gridView.DataBind();
                                    }
                                    else
                                    {
                                        gridView.DataSourceID = "SqlDataSource4";
                                        gridView.DataBind();
                                    }
                                }
                                else
                                {
                                    gridView.DataSourceID = "SqlDataSource3";
                                    gridView.DataBind();

                                }
                            }
                            else
                            {
                                gridView.DataSourceID = "SqlDataSource5";
                                gridView.DataBind();
                            }
                        }
                        else
                        {
                            gridView.DataSourceID = "SqlDataSource5";
                            gridView.DataBind();
                        }
                    }
                    //팀장
                    else if (adminTeamStatement)
                    {
                        int userKey = findUserKey(Name_Combobox.Text);
                        Session["findUser"] = userKey;
                        if (changedNameValue != null || Name_Combobox.Text != null)
                        {
                            if (date != null || dateEdit.Text != string.Empty)
                            {
                                if (receiveDateEdit.Text != string.Empty)
                                {
                                    if (endDateEdit.Text != string.Empty)
                                    {
                                        gridView.DataSourceID = "SqlDataSource2";
                                        gridView.DataBind();
                                    }
                                    else
                                    {
                                        gridView.DataSourceID = "SqlDataSource4";
                                        gridView.DataBind();
                                    }
                                }
                                else
                                {
                                    gridView.DataSourceID = "SqlDataSource3";
                                    gridView.DataBind();

                                }
                            }
                            else
                            {
                                gridView.DataSourceID = "SqlDataSource5";
                                gridView.DataBind();
                            }
                        }
                        else
                        {
                            gridView.DataSourceID = "SqlDataSource5";
                            gridView.DataBind();
                        }

                    }
                    //사용자
                    else
                    {
                        OracleConnection con = new OracleConnection(phoneString);
                        if (changedNameValue == UserName || Name_Combobox.Text != null)
                        {

                            if (date != null)
                            {
                                if (receiveDateEdit.Text != string.Empty)
                                {
                                    if (endDateEdit.Text != string.Empty)
                                    {

                                        gridView.DataSourceID = "sds6";
                                        gridView.DataBind();
                                    }
                                    else
                                    {
                                        gridView.DataSourceID = "sds7";
                                        gridView.DataBind();
                                    }
                                }
                                else
                                {
                                   
                                    gridView.DataSourceID = "sds8";
                                    gridView.DataBind();
                                }
                            }
                            else
                            {                         
                                Session["dateEdit"] = dateEdit.Text;
                                gridView.DataSourceID = "sds9";
                                gridView.DataBind();
                            }
                        }
                        else
                        {
                            gridView.DataSourceID = "SqlDataSource1";
                            gridView.DataBind();
                        }
                    }
                }

            }
            catch (Exception)
            {
                string strJs = @"<script>window.alert('기간의 범위가 안 맞습니다.');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RangeException", strJs);
            }

        }

        public static int findUserKey(string User_Name)
        {
            int result = 0;
            string query = string.Format("Select USER_KEY FROM ITMS.V_ITMS_USERS1 WHERE USER_NAME = '{0}'", User_Name);
            OracleConnection con = new OracleConnection(ITMSstring);
            OracleCommand cmd = new OracleCommand(query, con);
            con.Open();

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
            }
            con.Close();
            return result;
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
        public class UserSet
        {
            public int USER_KEY { get; set; }
            public string USER_NAME { get; set; }
            public string DEPT_NAME { get; set; }
            public string EMAIL { get; set; }
            public string PHONE_NUM { get; set; }
            public string CELL_PHONE { get; set; }
            public string EXTENSION { get; set; }
            public DateTime RETIRE_DATE { get; set; }
            public static List<UserSet> Getusers()
            {

                string query = "Select user_name from ITMS.V_ITMS_USERS1";
                OracleConnection con = new OracleConnection(ITMSstring);
                OracleCommand cmd = new OracleCommand(query, con);
                var list = new List<UserSet>();
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UserSet
                        {
                            USER_NAME = reader[0].ToString()
                        });
                    }
                    reader.Close();
                }
                con.Close();
                return list.ToList();
            }
            public static List<UserSet> FindUsers(int userKey)
            {
                string query = string.Format("SELECT user_name from ITMS.V_ITMS_USERS1 where user_key = {0}", UserKey);
                List<UserSet> user = new List<UserSet>();
                OracleConnection con = new OracleConnection(ITMSstring);
                OracleCommand cmd = new OracleCommand(query, con);
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Add(new UserSet
                        {
                            USER_NAME = reader[0].ToString()
                        }
                        );
                    }
                    reader.Close();
                }
                con.Close();
                return user.ToList();
            }
            public static List<UserSet> FindTeamUsers(int userKey)
            {
                string query = string.Format("SELECT * FROM ITMS.V_ITMS_USERS1 WHERE DEPT_NAME = (SELECT DEPT_NAME FROM ITMS.V_ITMS_USERS1 WHERE USER_KEY = {0})", UserKey);
                List<UserSet> user = new List<UserSet>();
                OracleConnection con = new OracleConnection(ITMSstring);
                OracleCommand cmd = new OracleCommand(query, con);
                con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Add(new UserSet
                        {
                            USER_NAME = reader[1].ToString()
                        });
                    }
                    reader.Close();
                }
                con.Close();
                return user.ToList();
            }
        }


        protected void userNameCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            changedNameValue = e.Parameter;
        }

        private int totalCallSeconds;
        private List<bool> statementList = new List<bool>();
        private bool statement;
        private int i = 0;
        private int totalCall;
        private int totalCost;
        protected void grid_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            int sumDuartionCall = Convert.ToInt32((e.Item as ASPxSummaryItem).Tag);
            int s = Convert.ToInt32((e.Item as ASPxSummaryItem).Index);
            //Initialization
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                if ((e.Item as ASPxSummaryItem).Tag == "1") totalCallSeconds = 0;
                if ((e.Item as ASPxSummaryItem).Tag == "2") statement = false;
            }
            //Calculation
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {

                if ((e.Item as ASPxSummaryItem).Tag == "1")
                {
                    if (statementList[i] == true && i <= statementList.Count - 1)
                    {
                        totalCallSeconds += Convert.ToInt32(e.FieldValue);
                        i += 1;
                    }
                    else
                    {
                        if (i <= statementList.Count - 1)
                        {
                            totalCall += Convert.ToInt32(e.FieldValue);
                            i += 1;
                        }

                    }
                    
                }
                if ((e.Item as ASPxSummaryItem).Tag == "2")
                {
                    if (e.FieldValue.ToString() == "Y")
                    {
                        statement = true;
                        statementList.Add(statement);
                    }
                    else
                    {
                        statement = false;
                        statementList.Add(statement);
                    }
                }

            }
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                DateTime Now = DateTime.Now;
                DateTime date = DateTime.Now.AddSeconds(Convert.ToDouble(totalCallSeconds));
                TimeSpan result = date.Subtract(Now);

                int totalCallResult = totalCall + totalCallSeconds;
                DateTime totalCallDate = DateTime.Now.AddSeconds(Convert.ToDouble(totalCallResult));
                TimeSpan result2 = totalCallDate.Subtract(Now);

                //합산 표 초당 3원
                totalCost = totalCallSeconds * 3;

                e.TotalValue = string.Format("<b><FONT COLOR= 'DarkRed' font-weight='bold' text-align:Right overflow:hidden > &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp " +
                    "[총 업무외 통화 시간: {0}일 {1}시간 {2}분 {3}초] </FONT></b>" +
                    "&nbsp &nbsp &nbsp " +
                    " <b><Font font-weight= 'bold' text-align:Right Overflow:hidden>  [총 업무 통화 시간: {4}일 {5}시간 {6}분 {7}초] </FONT></b>" +
                    "&nbsp &nbsp &nbsp <b><FONT COLOR= 'DarkRed' font-weight='bold' text-align:Right overflow:hidden > [예상 정산 비용: {8:c0}]" +
                    "</FONT></b>", result.Days, result.Hours, result.Minutes, result.Seconds,
                    result2.Days, result2.Hours, result2.Minutes, result2.Seconds, totalCost);


            }

        }
        protected void ASPxCallback1_Callback(object source, CallbackEventArgs e)
        {
            date = string.Format("{0:yyyy/MM}", new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(e.Parameter)).ToLocalTime());
            Session["date"] = date;
        }
        protected void ReceiveCallback_Callback(object source, CallbackEventArgs e)
        {
            DateTime s = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(e.Parameter)).ToLocalTime();
            receiveDate = string.Format("{0:yyyy/MM/dd}", s);
            Session["receiveDate"] = receiveDate;
        }
        protected void EndCallback_Callback(object source, CallbackEventArgs e)
        {
            DateTime sb = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(e.Parameter)).ToLocalTime();
            endDate = string.Format("{0:yyyy/MM/dd}", sb);

            Session["endDate"] = endDate;
        }

        protected void gridView_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "NOTE")
            {
                e.Editor.Focus();
            }
            else
            {
                e.Editor.ReadOnly = true;
            }
        }

        protected void gridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            Session["phone_key"] = Convert.ToInt32(e.Keys[0]);
            Session["textchanged"] = e.NewValues[11].ToString();
            Session["update_uk"] = UserKey;
        }


        protected void sds6_Updated1(object sender, SqlDataSourceStatusEventArgs e)
        {
            string query = string.Format("UPDATE PHONE_DATA_REPORT SET NOTE = '{0}' WHERE PHONE_KEY = {1} and USER_KEY = {2} ", Session["textchanged"].ToString(), Convert.ToInt32(Session["phone_key"]), Convert.ToInt32(Session["update_uk"]));
            OracleConnection con = new OracleConnection(phoneString);
            OracleCommand cmd = new OracleCommand(query, con);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

}