using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
namespace StudentAttendance
{
    public partial class AttendanceUpload : System.Web.UI.Page
    {
        string AttendanceDate, inTime, outTime, student_unique_id;
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
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("AttendanceDate", typeof(string)),
            new DataColumn("inTime", typeof(string)),
            new DataColumn("outTime", typeof(string)),
            new DataColumn("student_unique_id",typeof(string)) });


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
                //  char[] charsToTrim = { "&#160;", ' ', '\'' };
                foreach (DataRow row in dt.Rows)
                {

                    con.Open();

                    this.AttendanceDate = Server.HtmlEncode(row["AttendanceDate"].ToString());
                    this.inTime= Server.HtmlEncode(row["inTime"].ToString());
                    this.outTime= Server.HtmlEncode(outTime);
                    this.student_unique_id= Server.HtmlEncode(row["student_unique_id"].ToString());


                        string strInsert = "insert into student_attendance(AttendanceDate,inTime,outTime,student_unique_id) ";
                        //strInsert += " values('" + Session["id"] + "','" + this.firstName + "','" + this.lastName + "','" + this.comName + "','" + this.departmentName + "','" + this.email + "','" + this.codeone + "','" + this.officeNo + "','" + this.codetwo + "','" + this.mobiel + "','" + Regex.Escape(this.address) + "','" + this.city + "','" + this.state + "','" + this.zipCode + "','" + this.country + "','" + this.website + "','" + this.statusID + "')";
                        strInsert += "values(@AttendanceDate,@inTime,@outTime,@student_unique_id)";
                        MySqlCommand cmdInsert = new MySqlCommand(strInsert, con);
                        
                    cmdInsert.CommandType = CommandType.Text;

                    //--------------------------------------------------------------------------
                    TimeZoneInfo timeZoneInfo;
                    DateTime dateTime;
                    //Set the time zone information to US Mountain Standard Time 
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    //Get date and time in US Mountain Standard Time 
                    dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

                    cmdInsert.Parameters.AddWithValue("@AttendanceDate", dateTime.ToString("dd-MM-yyyy HH-mm-ss"));
                        cmdInsert.Parameters.AddWithValue("@inTime", this.inTime);
                        cmdInsert.Parameters.AddWithValue("@outTime", this.outTime);
                        cmdInsert.Parameters.AddWithValue("@student_unique_id", this.student_unique_id);
                        cmdInsert.ExecuteNonQuery();
                        con.Close();
                }
                //Bind the DataTable.
                GridView1.DataSource = dt;
                GridView1.DataBind();

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