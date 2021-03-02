using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Configuration;
using System.Data.SqlClient;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;
using System.Data;
using System.Web.Security;
using System.Globalization;
using System.Windows.Forms.ComponentModel;
using boardMVC.DotNetNote.Models;

namespace boardMVC.DotNetNote.admin
{
    public partial class Default : System.Web.UI.Page
    {
       
        private string str = "Data Source= spectra; User Id= spectra; Password = artceps";
        public void BindData()
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "Select * from loggedin_";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            OracleDataAdapter da = new OracleDataAdapter("select * from loggedin_", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Open();
            cmd.ExecuteNonQuery();
            gridService.DataSource = ds;
            gridService.DataBind();
            con.Close();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        protected void gridService_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gridService.EditIndex = e.NewEditIndex;
                BindData();

            }catch (Exception)
            {
                throw;
            }
        }

        protected void gridService_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;
       
            Label lblID = (Label)gridService.Rows[e.RowIndex].FindControl("lblID");
            Label lblUserID = (Label)gridService.Rows[e.RowIndex].FindControl("lblUserID");
            Label lblName = (Label)gridService.Rows[e.RowIndex].FindControl("lblName");
            TextBox lblPassword = (TextBox)gridService.Rows[e.RowIndex].FindControl("txtPassword");
            TextBox lblLevel = (TextBox)gridService.Rows[e.RowIndex].FindControl("txtLevel");

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "ModifyUsers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Value = lblID.Text;
            cmd.Parameters.Add("p_user_uid", OracleDbType.NVarchar2).Value = lblUserID.Text;
            cmd.Parameters.Add("p_user_password", OracleDbType.NVarchar2).Value = lblPassword.Text;
            cmd.Parameters.Add("p_user_level", OracleDbType.NVarchar2).Value = lblLevel.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            gridService.EditIndex = -1;
            BindData();
            con.Close();

        }

        protected void gridService_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            
            gridService.EditIndex = -1;
            BindData();

        }

        protected void gridService_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = str;

            Label lbldeleteID = (Label)gridService.Rows[e.RowIndex].FindControl("lblID");
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = string.Format("Delete from loggedin_ where user_id= {0}", lbldeleteID.Text);
            cmd.CommandType = CommandType.Text;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindData();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Create_Click(object sender, EventArgs e)
        {

        }
    }
}