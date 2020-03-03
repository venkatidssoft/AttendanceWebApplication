using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace StudentAttendance
{
    public partial class studentDetailsUpdate : System.Web.UI.Page
    {
        string fatherName, casteid,DOB, student_unique_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["id"] == null)
                {
                    Session.Clear();
                    Response.Redirect("login.aspx");
                }
                else
                {
                    GlobalVariables.connString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string script = "$(document).ready(function () { $('[id*=btnUpload]').click(); });";
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);

            //Upload and save the file.
            string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] {new DataColumn("fatherName",typeof(string)) ,
            new DataColumn("casteid", typeof(string)),
            new DataColumn("student_unique_id",typeof(string))
             });

            string csvData = File.ReadAllText(csvPath);
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }

            try
            {
                con.Open();
                foreach (DataRow row in dt.Rows)
                {
                    string attDate = "";
                    this.fatherName = Server.HtmlEncode(row["fatherName"].ToString()).Replace("&quot;", "");
                    this.casteid = Server.HtmlEncode(row["casteid"].ToString()).Replace("&quot;", "");
                    this.student_unique_id = Server.HtmlEncode(row["student_unique_id"].ToString().Trim()).Replace("&quot;", "");

                    //attDate = DateTime.ParseExact(this.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                    string strUpdate = "update student_registration set fatherName=@fatherName,casteid=@casteid where student_unique_id=@student_unique_id";
                    MySqlCommand cmdInsert = new MySqlCommand(strUpdate, con);
                    cmdInsert.CommandType = CommandType.Text;

                    cmdInsert.Parameters.AddWithValue("@fatherName", this.fatherName);
                    cmdInsert.Parameters.AddWithValue("@casteid", this.casteid);
                    cmdInsert.Parameters.AddWithValue("@student_unique_id", this.student_unique_id);
                    cmdInsert.ExecuteNonQuery();

                    //Bind the DataTable.
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}