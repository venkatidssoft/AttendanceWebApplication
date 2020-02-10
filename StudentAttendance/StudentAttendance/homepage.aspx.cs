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
        public string totalstregthpulivendula, presentpulivendula, absentpulivendula, totalstregthbadvel, presentbadvel, absentbadvel, totalstregth,fromDt;
        public string  present, absent;


        protected void btngeneratereport_Click(object sender, EventArgs e)
        {
            string username = Session["username"].ToString();
            fromDt = fromdatepicker.Value;
            string sDate = "";
            if (!string.IsNullOrEmpty(fromdatepicker.Value.Trim()))
            {
                // CONVERT DATE FORMAT.
                sDate = DateTime.ParseExact(
                    fromdatepicker.Value, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            }

            string script = "$(document).ready(function () { $('[id*=btngeneratereport]').click(); });";

            string fromDate = fromdatepicker.Value.ToString();
            DateTime fDate = Convert.ToDateTime(sDate);
            Label1.Text = fromDate;
            string fromdateValue = fDate.Year + "-" + fDate.Day.ToString("#00") + "-" + fDate.Month.ToString("#00") + " 00:00:00";

            string sqlQueryString = string.Empty;
            //zstring strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);
            try
            {
                //sqlQueryString = "select (SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa')-count(sa.student_unique_id)) as 'Total Absent' from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id AND sr.student_district = 'kadapa' AND sa.AttendanceDate >='" + fromdateValue + "' AND sa.AttendanceDate <='" + fromdatepicker + "'   group by sr.student_district  ";
                string isadminenable = Session["isAdmin"].ToString();
                if (isadminenable == "1")
                {
                    sqlQueryString = "SELECT total_students.total_count AS TotalStudents,IFNULL(Present,0) AS TotalPresent, (total_students.total_count - IFNULL(Present,0) ) AS TotalAbsent  FROM ( SELECT student_district, student_mandal,mandalid, COUNT(*) total_count FROM student_registration GROUP BY student_district, student_mandal ) total_students LEFT join ( SELECT DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y') attDate, reg.student_district, reg.student_mandal, COUNT(reg.student_unique_id) Present FROM student_attendance att, student_registration reg WHERE reg.student_unique_id = att.student_unique_id AND att.AttendanceDate >='" + fromdateValue + "' AND att.AttendanceDate <='" + fromdateValue + "' GROUP BY DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y'), reg.student_district, reg.student_mandal ) Stu_att ON total_students.student_mandal = Stu_att.student_mandal ORDER BY total_students.mandalid  ";
                }
                else
                {
                    sqlQueryString = "SELECT total_students.total_count AS TotalStudents,IFNULL(Present,0) AS TotalPresent, (total_students.total_count - IFNULL(Present,0) ) AS TotalAbsent FROM ( SELECT student_district, student_mandal,mandalid, COUNT(*) total_count FROM student_registration WHERE student_registration.student_mandal='"+username+"' GROUP BY student_district, student_mandal ) total_students LEFT join ( SELECT DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y') attDate, reg.student_district, reg.student_mandal, COUNT(reg.student_unique_id) Present FROM student_attendance att, student_registration reg WHERE reg.student_unique_id = att.student_unique_id AND att.AttendanceDate >='"+fromdateValue+ "' AND att.AttendanceDate <='" + fromdateValue + "'   GROUP BY DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y'), reg.student_district, reg.student_mandal ) Stu_att ON total_students.student_mandal = Stu_att.student_mandal   ORDER BY total_students.mandalid";
                }
                //sqlQueryString = "select (SELECT (SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa')-COUNT(sa.student_unique_id)) as 'Total Absent' from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id AND sr.student_district = 'kadapa' AND sa.AttendanceDate >='" + fromdateValue + "' AND sa.AttendanceDate <='" + fromdatepicker + "'   group by sr.student_district  ";
                using (MySqlCommand cmd = new MySqlCommand(sqlQueryString))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (sdr.Read())
                        {
                            if (isadminenable == "0")
                            {
                                Panel1.Visible = false;
                                Panel2.Visible = true;
                                totalstregth = sdr["TotalStudents"].ToString();
                                present = sdr["TotalPresent"].ToString();
                                absent = sdr["TotalAbsent"].ToString();
                            }
                            else
                            {
                                Panel1.Visible = true;
                                Panel2.Visible = false;
                                if (i == 0)
                                {
                                    totalstregthpulivendula = sdr["TotalStudents"].ToString();
                                    presentpulivendula = sdr["TotalPresent"].ToString();
                                    absentpulivendula = sdr["TotalAbsent"].ToString();
                                    i++;
                                }
                                else
                                {
                                    totalstregthbadvel = sdr["TotalStudents"].ToString();
                                    presentbadvel = sdr["TotalPresent"].ToString();
                                    absentbadvel= sdr["TotalAbsent"].ToString();
                                }
                            }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            string sDate = "";
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
                    present = "0";
                    absent = "100";
                    GlobalVariables.connString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                    this.fromDate = DateTime.Now.ToString("dd/MM/yyyy");
                    fromdatepicker.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    sDate = DateTime.ParseExact(this.fromDate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    DateTime fDate = Convert.ToDateTime(sDate);
                    string fromdateValue = fDate.Year + "-" + fDate.Day.ToString("#00") + "-" + fDate.Month.ToString("#00") + " 00:00:00";
                    Label1.Text =  fromdatepicker.Value;
                    fromDt = fromdatepicker.Value;
                    string username = Session["username"].ToString();

                    MySqlConnection con = new MySqlConnection(GlobalVariables.connString);
                    try
                    {
                        string isadminenable = Session["isAdmin"].ToString();
                        if (isadminenable == "1")
                        {
                            sqlQueryString = "SELECT total_students.total_count AS TotalStudents,IFNULL(Present,0) AS TotalPresent, (total_students.total_count - IFNULL(Present,0) ) AS TotalAbsent  FROM ( SELECT student_district, student_mandal,mandalid, COUNT(*) total_count FROM student_registration GROUP BY student_district, student_mandal ) total_students LEFT join ( SELECT DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y') attDate, reg.student_district, reg.student_mandal, COUNT(reg.student_unique_id) Present FROM student_attendance att, student_registration reg WHERE reg.student_unique_id = att.student_unique_id AND att.AttendanceDate >='" + fromdateValue + "' AND att.AttendanceDate <='" + fromdateValue + "' GROUP BY DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y'), reg.student_district, reg.student_mandal ) Stu_att ON total_students.student_mandal = Stu_att.student_mandal ORDER BY total_students.mandalid";
                        }
                        else
                        {
                            sqlQueryString = "SELECT total_students.total_count AS TotalStudents,IFNULL(Present,0) AS TotalPresent, (total_students.total_count - IFNULL(Present,0) ) AS TotalAbsent FROM ( SELECT student_district, student_mandal,mandalid, COUNT(*) total_count FROM student_registration WHERE student_registration.student_mandal='"+username+"' GROUP BY student_district, student_mandal ) total_students LEFT join ( SELECT DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y') attDate, reg.student_district, reg.student_mandal, COUNT(reg.student_unique_id) Present FROM student_attendance att, student_registration reg WHERE reg.student_unique_id = att.student_unique_id AND att.AttendanceDate >='" + fromdateValue + "' AND att.AttendanceDate <='" + fromdateValue + "'   GROUP BY DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y'), reg.student_district, reg.student_mandal ) Stu_att ON total_students.student_mandal = Stu_att.student_mandal   ORDER BY total_students.mandalid";
                        }
                           
                        using (MySqlCommand cmd = new MySqlCommand(sqlQueryString))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            con.Open();
                            using (MySqlDataReader sdr = cmd.ExecuteReader())
                            {
                                int i = 0;
                                while (sdr.Read())
                                {
                                    if (isadminenable == "0")
                                    {
                                        Panel1.Visible = false;
                                        Panel2.Visible = true;
                                        totalstregth = sdr["TotalStudents"].ToString();
                                        present = sdr["TotalPresent"].ToString();
                                        absent = sdr["TotalAbsent"].ToString();
                                    }
                                    else
                                    {
                                        Panel1.Visible = true;
                                        Panel2.Visible = false;
                                        if (i == 0)
                                        {
                                            //totalstregthpulivendula = sdr["TotalStudents"].ToString();
                                            presentpulivendula = sdr["TotalPresent"].ToString();
                                            absentpulivendula = sdr["TotalAbsent"].ToString();
                                            i++;
                                        }
                                        else
                                        {
                                            //totalstregthbadvel = sdr["TotalStudents"].ToString();
                                            presentbadvel = sdr["TotalPresent"].ToString();
                                            absentbadvel = sdr["TotalAbsent"].ToString();
                                        }
                                    }
                                    //totalstregth = sdr["TotalStudents"].ToString();
                                    //present = sdr["TotalPresent"].ToString();
                                    //absent = sdr["TotalAbsent"].ToString();
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