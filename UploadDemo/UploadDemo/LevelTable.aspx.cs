using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;

namespace UploadDemo
{
    public partial class LevelTable : System.Web.UI.Page
    {
        private string str = "Data Source = spectra; User ID = spectra; Password = artceps; Unicode = True";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Showing DataBase GridView
            OracleConnection con = new OracleConnection();
            OracleCommand command = new OracleCommand();
            command.CommandText = "select * from phoneData";
            command.CommandType = CommandType.Text;
            command.Connection = con;
            con.Open();

            OracleDataAdapter oda = new OracleDataAdapter("select * from phoneData", con);
            DataSet ds = new DataSet();
            oda.Fill(ds);
            command.ExecuteNonQuery();


            GridView1.DataSource = ds;
            GridView1.DataBind();

            con.Close();

            GridView1.Visible = true;    
            

        }
    }
}