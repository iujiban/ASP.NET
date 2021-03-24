using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;
using System.Data;

namespace UploadDemo
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputFile fiLMyFile;
        static string ExcelPath;
        private string _BaseDir = string.Empty;
        private string connectionstring = "Data Source = spectra; User ID = spectra; Password = artceps;";

        static Excel.Application excelApp = null;
        static Excel.Workbook workBook = null;
        static Excel.Worksheet workSheet = null;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
        }
       

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {
                fileDirectory();
            }
        }
        private void fileDirectory()
        {
            //HttpPostedFile 개체를 받아 옴
            HttpPostedFile myFile = FileUpload1.PostedFile;
            //파일 사이즈를 받아옴
            int intFileLength = System.Convert.ToInt32(myFile.ContentLength);

            if (intFileLength == 0)
            {
                return;
            }

            //파일 사이즈 만큼 바이트 배열을 잡는다.
            byte[] myData = new byte[intFileLength];

            //스트림에서 파일을 읽어 바이트 배열에 담는 것
            myFile.InputStream.Read(myData, 0, intFileLength);

            //UploadFileName
            string path = Path.GetFileName(FileUpload1.FileName);
            //Upload Directory
            _BaseDir = Server.MapPath("~/ExcelFile/");
            string dateTime = DateTime.Now.ToString("dd-MM-yy");

            string pathstring = Path.Combine(_BaseDir, Session["userID"].ToString());
            Directory.CreateDirectory(pathstring);
            string datePathstring = Path.Combine(pathstring, dateTime);
            Directory.CreateDirectory(datePathstring);

            string name = "";
            int i = 0;

            if (File.Exists(Path.Combine(datePathstring, path)))
            {
                //NewFile if its File exists
                name = Path.GetFileNameWithoutExtension(FileUpload1.FileName) + "(" + ++i + ")" + Path.GetExtension(FileUpload1.FileName);
                path = path.Replace(" ", "");
               // FileUpload1.SaveAs(Path.Combine(datePathstring, name));
                ExcelPath = Path.Combine(datePathstring, name);
                ViewState["FileName"] = name;
                ViewState["FileSize"] = FileUpload1.FileBytes.Length;
                //파일생성
                FileStream newFile = new FileStream(ExcelPath, FileMode.Create);
                //byte 배열의 내용을 파일에 씁니다.
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();
            }
            else
            {   //NewFile
                path = path.Replace(" ", ""); // Replace -> Trim()같은 효과
                //FileUpload1.SaveAs(Path.Combine(datePathstring, path));
                ExcelPath = Path.Combine(datePathstring, path);
                ViewState["FileName"] = path;
                ViewState["FileSize"] = FileUpload1.FileBytes.Length;
                //파일생성
                FileStream newFile = new FileStream(ExcelPath, FileMode.Create);
                //byte 배열의 내용을 파일에 씁니다.
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();
            }

            OracleConnection con = new OracleConnection(connectionstring);
            OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM TC_FILES", con);
            OracleCommandBuilder oCB = new OracleCommandBuilder(oda);
            DataSet ds = new DataSet();

            //Fill the dataset with the schema but not the data
            oda.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            con.Open();
            oda.FillSchema(ds, SchemaType.Source, "TC_FILES");

            byte[] byteData = new byte[intFileLength];
            myFile.InputStream.Read(byteData, 0, intFileLength);

            /*
            dr = ds.Tables["TC_FILES"].NewRow();
            dr["File_ID"] = 1; //TRIGGER AUTO INCREMENT BY 1
            dr["User_Name"] = Session["userID"].ToString();
            dr["File_DATA"] = myData;
            dr["File_Name"] = System.IO.Path.GetFileName(myFile.FileName);
            //Append the new row to the dataset
            ds.Tables["TC_FILES"].Rows.Add(dr);

            //Update the table using our dataset against our data adapter
            oda.Update(ds, "TC_FILES");

             */
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string query = string.Format("Insert into TC_FILES values (TC_FILES_seq.nextval, '{0}', '{1}', '{2}', '{3}')", Session["userID"].ToString(), ViewState["FileName"].ToString(), myData, date );
            OracleCommand cmd = new OracleCommand(query, con);

            cmd.ExecuteNonQuery();

            con.Close();

            //Insert the file content
            fileSave(ExcelPath);
        }
        private void fileSave(string path)
        {
            int id;
            string Receiver, Call, Phone, ConnectionName, ServiceLevel, ReceiveDate, ReceiveTime, EndDate, EndTime, OffDutyTime;
            string subtractCall = null;
            try
            {
                excelApp = new Excel.Application();
                workBook = excelApp.Workbooks.Open(path);
                workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;
                //사용중인 셀 범위를 가져오기
                Excel.Range range = workSheet.UsedRange;

                //row 3부터 데이터 값 인식.
                for (int row = 3; row <= range.Rows.Count; row++)
                {

                    //셀 데이터 가져옴
                    id = (int)(range.Cells[row, 1] as Excel.Range).Value2;
                    Receiver = (string)(range.Cells[row, 2] as Excel.Range).Value2;
                    Call = (string)(range.Cells[row, 3] as Excel.Range).Value2;
                    Phone = (string)(range.Cells[row, 4] as Excel.Range).Value2;
                    ConnectionName = (string)(range.Cells[row, 5] as Excel.Range).Value2;
                    ServiceLevel = (string)(range.Cells[row, 6] as Excel.Range).Value2;
                    ReceiveDate = string.Format("{0:yyyy/MM/dd}", DateTime.FromOADate((range.Cells[row, 7] as Excel.Range).Value2));
                    ReceiveTime = string.Format("{0:HH:mm}", DateTime.FromOADate((range.Cells[row, 8] as Excel.Range).Value2));
                    EndDate = string.Format("{0:yyyy/MM/dd}", DateTime.FromOADate((range.Cells[row, 9] as Excel.Range).Value2));
                    EndTime = string.Format("{0:HH:mm}", DateTime.FromOADate((range.Cells[row, 10] as Excel.Range).Value2));
                    if ((string)(range.Cells[row, 11] as Excel.Range).Value2 != null)
                    {
                        OffDutyTime = (string)(range.Cells[row, 11] as Excel.Range).Value2;
                    }
                    else
                    {
                        if ((Convert.ToDateTime(ReceiveDate).DayOfWeek == DayOfWeek.Saturday) || (Convert.ToDateTime(ReceiveDate).DayOfWeek == DayOfWeek.Sunday))
                        {
                            OffDutyTime = 'X'.ToString();
                            
                        } else
                        {
                            OffDutyTime = 'O'.ToString();
                        }
                    }

                    if (Convert.ToDateTime(ReceiveDate).Date == Convert.ToDateTime(EndDate).Date)
                    {
                        TimeSpan diff1 = Convert.ToDateTime(EndTime).Subtract(Convert.ToDateTime(ReceiveTime));
                        subtractCall = string.Format("{0:HH:mm}", Convert.ToDateTime(diff1.ToString()));
                    }
                    else
                    {
                        //24시간 - need thinking
                        TimeSpan dateDiffer = Convert.ToDateTime(EndDate).Subtract(Convert.ToDateTime(ReceiveTime));
                        string dateResult = string.Format("{0:HH:mm}", Convert.ToDateTime(dateDiffer));
                        string End = string.Format("{0:HH:mm}", Convert.ToDateTime(EndTime));
                        string Receive = string.Format("{0:HH:mm}", Convert.ToDateTime(EndTime));

                    }

                    saveData(id, Receiver, Call, Phone, ConnectionName, ServiceLevel, ReceiveDate, ReceiveTime, EndDate, EndTime, OffDutyTime, subtractCall);

                }
                workBook.Close(true);
                excelApp.Quit();
                SucceedMessage.Text = "Excel File Has Been Upload";
            }
            finally
            {
                ReleaseObject(workSheet);
                ReleaseObject(workBook);
                ReleaseObject(excelApp);
            }

        }
        private void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    //액셀 개체 해제 => 액셀 객체를 사용하고 난 후 반드시 액셀 객체를 해제(Release)해 주어야 함.
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect(); //가비지 수집
            }
        }
        private void saveData(int id, string receiver, string call, string phone, string connectionName, string serviceLevel, string receiveDate, string receiveTime, string endDate, string endCall, string offDutyTime, string subtractCall)
        {
            string query = string.Format("insert into phoneData values (phoneData_seq.nextval,'{1}','{2}','{3}','{4}','{5}', '{6}', '{7}', '{8}', '{9}','{10}', '{11}')", id, receiver, call, phone, connectionName, serviceLevel, receiveDate, receiveTime, endDate, endCall, offDutyTime, subtractCall);
            OracleConnection con = new OracleConnection(connectionstring);
            con.Open();
            OracleCommand cmd = new OracleCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}