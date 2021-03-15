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
    public partial class adminTable : System.Web.UI.Page
    {
        private string str = "Data Source = spectra; User ID = spectra; Password = artceps;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                gridService.Visible = false;
            }
            
        }

        public void BindData()
        {   
            //변수 선언
            var level = Session["clickLevel"].ToString();

            OracleConnection con = new OracleConnection(str);   

            OracleDataAdapter oda = new OracleDataAdapter(string.Format("select * from phoneData where servicelevel = '{0}' order by 번호 asc", level), con);
            DataSet ods = new DataSet();

            oda.Fill(ods, "Data");
            
            gridService.DataSource = ods;
            gridService.DataBind();

            gridService.Visible = true;
          
        }
        protected void PREMIUM_Click(object sender, EventArgs e)
        {
            Session["clickLevel"] = "PREMIUM";
            BindData();

        }

        protected void SMART_Click(object sender, EventArgs e)
        {
            Session["clickLevel"] = "SMART";
            BindData();
        }

        protected void BASIC_Click(object sender, EventArgs e)
        {
            Session["clickLevel"] = "BASIC";
            BindData();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gridService.EditIndex = e.NewEditIndex;
                BindData();
                
            } catch (Exception)
            {
                throw;
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            OracleConnection con = new OracleConnection(str);

            Label lblID = (Label)gridService.Rows[e.RowIndex].FindControl("lblID");
            TextBox txtBusinessName = (TextBox)gridService.Rows[e.RowIndex].FindControl("txtBusinessName");
            TextBox txtReceiveDate = (TextBox)gridService.Rows[e.RowIndex].FindControl("txtReceiveDate");
            TextBox txtReceiveTime = (TextBox)gridService.Rows[e.RowIndex].FindControl("txtReceiveTime");
            TextBox txtEndDate = (TextBox)gridService.Rows[e.RowIndex].FindControl("txtEndDate");
            TextBox txtEndTime = (TextBox)gridService.Rows[e.RowIndex].FindControl("txtEndTime");
            TextBox txtException = (TextBox)gridService.Rows[e.RowIndex].FindControl("txtException");

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = string.Format("UPDATE phoneData Set  거래처명 = '{0}', 수신월일 = '{1}', 수신시간= '{2}', 통화종료월일 ='{3}', 통화종료시간 = '{4}', 근무외시간여부 ='{5}' where 번호 = {6}",
                txtBusinessName.Text, txtReceiveDate.Text, txtReceiveTime.Text, txtEndDate.Text, txtEndTime.Text, txtException.Text, Convert.ToInt32(lblID.Text));
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();

            gridService.EditIndex = -1;
            BindData();

            con.Close();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridService.EditIndex = -1;
            BindData();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            Label lblDeleteID = (Label)gridService.Rows[e.RowIndex].FindControl("lblID");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = string.Format("Delete from phoneData where 번호 = {0}", lblDeleteID.Text);
            cmd.CommandType = CommandType.Text;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindData();
        }

        
    }
}