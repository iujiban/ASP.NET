using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OracleClient;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace UploadDemo
{
    public partial class IExportUpload : System.Web.UI.Page
    {
        //Downolad Important coding.Link Given in Description
        static string ExcelPath;
        private string _BaseDir = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            //UploadFileName
            string path = Path.GetFileName(FileUpload1.FileName);
            //Upload directory
            _BaseDir = Server.MapPath("~/ExcelFile/");
            string dateTime = DateTime.Now.ToString("dd-MM-yy");

            string pathstring = Path.Combine(_BaseDir, Session["userID"].ToString());
            Directory.CreateDirectory(pathstring);
            string datePathstring = Path.Combine(pathstring, dateTime);
            Directory.CreateDirectory(datePathstring);

            //Checking the duplicates of the file and Save the file directory
            string name = "";
            int i = 0;
            if (File.Exists(Path.Combine(datePathstring, path)))
            {
                //NewFile if its File exists
                name = Path.GetFileNameWithoutExtension(FileUpload1.FileName) + "(" + ++i + ")" + Path.GetExtension(FileUpload1.FileName);
                path = path.Replace(" ", "");
                FileUpload1.SaveAs(Path.Combine(datePathstring, name));
                ExcelPath = Path.Combine(datePathstring, name);
            }
            else
            {   //NewFile
                path = path.Replace(" ", ""); // Replace -> Trim()같은 효과
                FileUpload1.SaveAs(Path.Combine(datePathstring, path));
                ExcelPath = Path.Combine(datePathstring, path);
            }
            /*
            //Connection with excelFile
            OleDbConnection mycon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source= " + ExcelPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"");
            mycon.Open();
            //Worksheet 페이지 지정
            OleDbCommand cmd = new OleDbCommand("select * from [2020-10월$]", mycon);
            //DataFill
            OleDbDataAdapter oda = new OleDbDataAdapter();
            oda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            oda.Fill(ds);
            mycon.Close();
            */
            Label2.Text = "Excel File Has Been Upload and Data Captured";
            Button2.Visible = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int id;
            String Receiver;
            String Call;
            String Phone;
            String ConnectionName;
            String ServiceLevel;
            String ReceiveDate;
            String ReceiveTime;
            String EndDate;
            String EndCall;
            String OffDutyTime;
            String subtractCall;


            OleDbConnection mycon = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source = " + ExcelPath + "; Extended Properties=Excel 12.0; Persist Security Info = False");
            mycon.Open();
            OleDbCommand cmd = new OleDbCommand("select * from [2020-10월$]", mycon);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id = Convert.ToInt32(dr[0].ToString());
                Receiver = dr[1].ToString();
                Call = dr[2].ToString();
                Phone = dr[3].ToString();
                ConnectionName = dr[4].ToString();
                ServiceLevel = dr[5].ToString();
                ReceiveDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(dr[6].ToString()));
                ReceiveTime = string.Format("{0:HH:mm}", Convert.ToDateTime(dr[7].ToString()));
                EndDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(dr[8].ToString()));
                EndCall = string.Format("{0:HH:mm}", Convert.ToDateTime(dr[9].ToString()));
                OffDutyTime = dr[10].ToString();

                if (Convert.ToDateTime(ReceiveDate).Date == Convert.ToDateTime(EndDate).Date)
                {
                    System.TimeSpan diff1 = Convert.ToDateTime(EndCall).Subtract(Convert.ToDateTime(ReceiveTime));
                    subtractCall = String.Format("{0:HH:mm}", Convert.ToDateTime(diff1.ToString()));
                }
                else
                {
                    TimeSpan diff2 = Convert.ToDateTime(EndDate).Subtract(Convert.ToDateTime(ReceiveDate));
                    TimeSpan diff1 = Convert.ToDateTime(EndCall).Subtract(Convert.ToDateTime(ReceiveTime));
                    subtractCall = string.Format("{0:HH:mm", diff2.Hours + diff1.Minutes);
                }

            saveData(id, Receiver, Call, Phone, ConnectionName, ServiceLevel, ReceiveDate, ReceiveTime, EndDate, EndCall, OffDutyTime, subtractCall);

        }

        mycon.Close();
            
        }
    private void saveData(int id, string receiver, string call, string phone, string connectionName, string serviceLevel, string receiveDate, string receiveTime, string endDate, string endCall, string offDutyTime, string subtractCall)
    {
        string query = string.Format("insert into phoneData values (phoneData_seq.nextval,'{1}','{2}','{3}','{4}','{5}', '{6}', '{7}', '{8}', '{9}','{10}', '{11}')", id, receiver, call, phone, connectionName, serviceLevel, receiveDate, receiveTime, endDate, endCall, offDutyTime, subtractCall);
        string mycon = "Data Source=spectra; User ID = spectra; Password = artceps;";
        OracleConnection con = new OracleConnection(mycon);
        con.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.CommandText = query;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
        con.Close();
    }




}
}