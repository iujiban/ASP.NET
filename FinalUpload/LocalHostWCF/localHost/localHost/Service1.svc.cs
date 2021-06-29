using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace localHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {


        public string Update(UpdateUser u)
        {
            string Message = "";
            string connectionString = ConfigurationManager.ConnectionStrings["ERP"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("Update UserTab Set Name = @Name, Email = @Email Where userID = @UserID", con);
            

            int g = cmd.ExecuteNonQuery();

            if(g ==1)
            {
                Message = "Update Successfully";
            } else
            {
                Message = "Failed";
            }
            con.Close();
            return Message;
        }

        gettestdata IService1.GetInfo(string phoneNumber, string dashNumber)
        {
            string PhoneNumber = string.Empty;
            string connectionString = ConfigurationManager.ConnectionStrings["ERP"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            gettestdata data = new gettestdata();

            con.Open();
            string query = string.Format("SELECT DISTINCT TOP 1 replace(A.TelNo, '-', '') as phoneNumber, A.CustSeq, B.CustName, C.PJTSeq, C.PJTName, ISNULL(MinorName, '') as SLALevel FROM _TSIASClientInfo AS A WITH(NOLOCK) LEFT OUTER JOIN _TDACust AS B WITH(NOLOCK) ON A.CompanySeq = B.CompanySeq AND A.CustSeq = B.CustSeq LEFT OUTER JOIN _TPJTProject AS C WITH(NOLOCK) ON A.CompanySeq = C.CompanySeq AND A.CustSeq = C.CustSeq LEFT OUTER JOIN _TPJTBaseMngItem AS D WITH(NOLOCK) ON C.CompanySeq = D.CompanySeq AND C.PJTSeq = D.SomeSeq AND C.PJTTypeSeq = D.PJTTypeSeq AND D.PgmSeq = 1958 JOIN _TDAUMinor AS E WITH(NOLOCK) ON D.CompanySeq = E.CompanySeq AND D.RemValSeq = E.MinorSeq AND Majorseq = 2000021 WHERE A.TelNo= '{0}' OR A.TelNo ='{1}' ORDER BY C.PJTSeq DESC;", phoneNumber, dashNumber);
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter ERPData = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ERPData.Fill(ds);
            data.GettingData = ds;
            con.Close();

            return data;
        }
    }
}

