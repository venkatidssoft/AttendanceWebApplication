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
    public partial class StudentsUpload : System.Web.UI.Page
    {
        string district, mandal, school, location, udise, uniqueid, name, gender, classes, medium, sections,districtID, mandalID, schoolID, locationID, genderID, classesID, mediumID, sectionsID;
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string script = "$(document).ready(function () { $('[id*=btnUpload]').click(); });";
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);

            //Upload and save the file.
            string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[16] { new DataColumn("student_unique_id", typeof(string)),
            new DataColumn("student_name", typeof(string)),
            new DataColumn("student_sex", typeof(string)),
            new DataColumn("student_class", typeof(string)),
            new DataColumn("student_medium", typeof(string)),
            //new DataColumn("student_section", typeof(string)),
            new DataColumn("student_district", typeof(string)),
            new DataColumn("student_mandal", typeof(string)),
            new DataColumn("student_Udise", typeof(string)),
            new DataColumn("student_school", typeof(string)),
            new DataColumn("student_location", typeof(string)),
            new DataColumn("districtID", typeof(string)),
            new DataColumn("mandalID", typeof(string)),
            new DataColumn("schoolID", typeof(string)),
            //new DataColumn("locationID", typeof(string)),
            new DataColumn("genderID", typeof(string)),
            new DataColumn("classID", typeof(string)),
            new DataColumn("mediumID", typeof(string))
            //new DataColumn("sectionID",typeof(string)) 
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


                    this.uniqueid = Server.HtmlEncode(row["student_unique_id"].ToString());
                    this.name = Server.HtmlEncode(row["student_name"].ToString());
                    this.gender= Server.HtmlEncode(row["student_sex"].ToString());
                    this.classes = Server.HtmlEncode(row["student_class"].ToString());
                    this.medium = Server.HtmlEncode(row["student_medium"].ToString());
                    //this.sections= Server.HtmlEncode(row["student_section"].ToString());
                    this.district= Server.HtmlEncode(row["student_district"].ToString());
                    this.mandal = Server.HtmlEncode(row["student_mandal"].ToString());
                    this.udise= Server.HtmlEncode(row["student_Udise"].ToString());
                    this.school= Server.HtmlEncode(row["student_school"].ToString());
                    this.location = Server.HtmlEncode(row["student_location"].ToString());
                    this.districtID = Server.HtmlEncode(row["districtID"].ToString());
                    this.mandalID = Server.HtmlEncode(row["mandalID"].ToString());
                    this.schoolID= Server.HtmlEncode(row["schoolID"].ToString());
                    //this.locationID = Server.HtmlEncode(row["locationID"].ToString());
                    this.genderID = Server.HtmlEncode(row["genderID"].ToString());
                    this.classesID= Server.HtmlEncode(row["classID"].ToString());
                    this.mediumID= Server.HtmlEncode(row["mediumID"].ToString());
                    //this.sectionsID = Server.HtmlEncode(row["sectionID"].ToString());


                    //string strInsert = "insert into student_registration(student_unique_id,student_name,student_sex,student_class,student_medium,student_section,student_district,student_mandal,student_Udise,student_school,student_location,districtID,mandalID,schoolID,locationID,genderID,classID,mediumID,sectionID) ";
                    //strInsert += "values(@student_unique_id,@student_name,@student_sex,@student_class,@student_medium,@student_section,@student_district,@student_mandal,@student_Udise,@student_school,@student_location,@districtID,@mandalID,@schoolID,@locationID,@genderID,@classID,@mediumID,@sectionID)";

                    string qry = "select * from student_registration where student_unique_id='" + this.uniqueid + "'";
                    MySqlCommand cmd = new MySqlCommand(qry, con);
                    MySqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        sdr.Close();
                    }
                    else
                    {
                        sdr.Close();
                        string strInsert = "insert into student_registration(student_unique_id,student_name,student_sex,student_class,student_medium,student_district,student_mandal,student_Udise,student_school,student_location,districtID,mandalID,schoolID,genderID,classID,mediumID) ";
                        strInsert += "values(@student_unique_id,@student_name,@student_sex,@student_class,@student_medium,@student_district,@student_mandal,@student_Udise,@student_school,@student_location,@districtID,@mandalID,@schoolID,@genderID,@classID,@mediumID)";
                        MySqlCommand cmdInsert = new MySqlCommand(strInsert, con);

                        cmdInsert.CommandType = CommandType.Text;

                        //--------------------------------------------------------------------------
                        TimeZoneInfo timeZoneInfo;
                        DateTime dateTime;
                        //Set the time zone information to US Mountain Standard Time 
                        timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        //Get date and time in US Mountain Standard Time 
                        dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

                        cmdInsert.Parameters.AddWithValue("@student_unique_id", this.uniqueid);
                        cmdInsert.Parameters.AddWithValue("@student_name", this.name);
                        cmdInsert.Parameters.AddWithValue("@student_sex", this.gender);
                        cmdInsert.Parameters.AddWithValue("@student_class", this.classes);
                        cmdInsert.Parameters.AddWithValue("@student_medium", this.medium);
                        //cmdInsert.Parameters.AddWithValue("@student_section",this.sections );
                        cmdInsert.Parameters.AddWithValue("@student_district", this.district);
                        cmdInsert.Parameters.AddWithValue("@student_mandal", this.mandal);
                        cmdInsert.Parameters.AddWithValue("@student_Udise", this.udise);
                        cmdInsert.Parameters.AddWithValue("@student_school", this.school);
                        cmdInsert.Parameters.AddWithValue("@student_location", this.location);
                        cmdInsert.Parameters.AddWithValue("@districtID", this.districtID);
                        cmdInsert.Parameters.AddWithValue("@mandalID", this.mandalID);
                        cmdInsert.Parameters.AddWithValue("@schoolID", this.schoolID);
                        //cmdInsert.Parameters.AddWithValue("@locationID",this.locationID );
                        cmdInsert.Parameters.AddWithValue("@genderID", this.genderID);
                        cmdInsert.Parameters.AddWithValue("@classID", this.classesID);
                        cmdInsert.Parameters.AddWithValue("@mediumID", this.mediumID);
                        //cmdInsert.Parameters.AddWithValue("@sectionID",this.sectionsID );
                        cmdInsert.ExecuteNonQuery();
                    }
                }
                
                //Bind the DataTable.
                GridView1.DataSource = dt;
                GridView1.DataBind();
                con.Close();
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