using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace StudentAttendance
{
    public partial class homepage : System.Web.UI.Page
    {
        string fromDate;
        public string totalstregth,present,absent,fromDt;

        protected void btngeneratereport_Click(object sender, EventArgs e)
        {
            fromDt = fromdatepicker.Value;
            string sDate = "";
            if (!string.IsNullOrEmpty(fromdatepicker.Value.Trim()))
            {
                // CONVERT DATE FORMAT.
                sDate = DateTime.ParseExact(
                    fromdatepicker.Value, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                //selectedDate.InnerHtml = sDate;       // SHOW THE FORMATED DATE.
            }



            string script = "$(document).ready(function () { $('[id*=btngeneratereport]').click(); });";

            string fromDate = fromdatepicker.Value.ToString();
            DateTime fDate = Convert.ToDateTime(sDate);


            Label1.Text = fromDate;
            string fromdateValue = fDate.Year + "-" + fDate.Month + "-" + fDate.Day + " 00:00:00";

            string sqlQueryString = string.Empty;
            //zstring strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);
            try
            {
                sqlQueryString = "select (SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa')-count(sa.student_unique_id)) as 'Total Absent' from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id AND sr.student_district = 'kadapa' AND sa.AttendanceDate >='" + fromdateValue + "' AND sa.AttendanceDate <='" + fromdatepicker + "'   group by sr.student_district  ";
                //sqlQueryString = "select (SELECT (SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa')-COUNT(sa.student_unique_id)) as 'Total Absent' from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id AND sr.student_district = 'kadapa' AND sa.AttendanceDate >='" + fromdateValue + "' AND sa.AttendanceDate <='" + fromdatepicker + "'   group by sr.student_district  ";
                using (MySqlCommand cmd = new MySqlCommand(sqlQueryString))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        totalstregth= sdr["Total Students"].ToString();
                        present = sdr["Total Present"].ToString();
                        absent = sdr["Total Absent"].ToString();
                      
                    }
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }





        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlQueryString = string.Empty;
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
                    //totalstregth = "501.9";
                    //present = "301.9";
                    //absent = "201.1";
                    this.fromDate = DateTime.Now.ToString("dd/MM/yyyy");
                    fromdatepicker.Value = this.fromDate;
                    Label1.Text =  fromdatepicker.Value;
                    fromDt = fromdatepicker.Value;

                    MySqlConnection con = new MySqlConnection(GlobalVariables.connString);
                    try
                    {
                        string isadminenable = Session["isAdmin"].ToString();
                        if (isadminenable == "1")
                        {
                            sqlQueryString = "select (SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa') as 'TotalStudents', count(sa.student_unique_id) as 'TotalPresent',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa')-count(sa.student_unique_id)) as 'TotalAbsent' from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id AND sr.student_district = 'kadapa' AND sa.AttendanceDate >='" + fromdatepicker.Value + "' AND sa.AttendanceDate <='" + fromdatepicker.Value + "'   group by sr.student_district  ";
                        }
                        else
                        {
                            sqlQueryString = "select (SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa') as 'TotalStudents', count(sa.student_unique_id) as 'TotalPresent',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa')-count(sa.student_unique_id)) as 'TotalAbsent' from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id AND sr.student_district = 'kadapa' AND sa.AttendanceDate >='" + fromdatepicker.Value + "' AND sa.AttendanceDate <='" + fromdatepicker.Value + "' and sr.student_unique_id='"+Session["id"]+"'  group by sr.student_district  ";
                        }
                           
                        //sqlQueryString = "select (SELECT (SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa')-COUNT(sa.student_unique_id)) as 'Total Absent' from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id AND sr.student_district = 'kadapa' AND sa.AttendanceDate >='" + fromdateValue + "' AND sa.AttendanceDate <='" + fromdatepicker + "'   group by sr.student_district  ";
                        using (MySqlCommand cmd = new MySqlCommand(sqlQueryString))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            con.Open();
                            using (MySqlDataReader sdr = cmd.ExecuteReader())
                            {
                                if (sdr.HasRows)
                                {
                                    sdr.Read();
                                    totalstregth = sdr["TotalStudents"].ToString();
                                    present = sdr["TotalPresent"].ToString();
                                    absent = sdr["TotalAbsent"].ToString();
                                }
                                else
                                {
                                    //totalstregth = "501.9";
                                    present = "0";
                                    absent = "1641";
                                }
                            }
                            con.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        con.Close();
                    }

                }
            }
            
        }
    }
}