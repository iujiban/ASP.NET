using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;

namespace UploadDemo
{
    public partial class Download : System.Web.UI.Page
    {
        private string fileName = "";
        private string dir = "";
        private string str = "Data Source = spectra; User Id = spectra; Password = artceps";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            fileName = GetFileNameById((Convert.ToInt32(Request["Id"])));
            string dateTime = DateTime.Now.ToString("dd-MM-yy");

            dir = Server.MapPath("./ExcelFile/" + Session["userID"] + "/" + dateTime + "/");
            if (IsCorrectAuthor(fileName))
            {
                if (fileName == null)
                {
                    Response.Clear();
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlPathEncode((fileName.Length > 50) ?
                        fileName.Substring(fileName.Length - 50, 50) : fileName));
                    Response.WriteFile(dir + fileName);
                    Response.End();
                }
            } else
            {
                string strJs = "<script>alert ('권한이 없습니다.');location.href='IExportUpload.aspx';</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", strJs);
            }
            
        }
        public string findinguserName ()
        {
            string result = "";
            string query = string.Format("Select userName from phoneUser where userid = '{0}'", Session["userID"].ToString());
            OracleConnection con = new OracleConnection(str);
            OracleCommand cmd = new OracleCommand(query, con);
            con.Open();
            using(OracleDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    result = reader[0].ToString();
                }
                reader.Close();
            }
            con.Close();
            return result;

        }
       public Boolean IsCorrectAuthor( string fileName)
        {
            Session["file_userName"] = findinguserName();

            bool result = false;
            string query = string.Format("Select * from fileUploadsave where postName ='{0}' and fileName ='{1}'", Session["file_userNAME"].ToString(), fileName);

            OracleConnection con = new OracleConnection(str);
            OracleCommand cmd = new OracleCommand(query, con);
            con.Open();
            using(OracleDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    result = true;
                }
                reader.Close();
            }
            con.Close();
            return result;
        }
        public string GetFileNameById(int id)
        {
            string result = "";
            string query = string.Format("Select fileName from fileUploadsave where id = '{0}'", Convert.ToInt32(id));

            OracleConnection con = new OracleConnection(str);
            OracleCommand cmd = new OracleCommand(query, con);
            con.Open();
            using(OracleDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    result = reader[0].ToString();
                }
                reader.Close();
            }
            con.Close();
            return result;
            
        }
    }
}