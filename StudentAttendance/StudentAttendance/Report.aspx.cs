﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Drawing;

namespace StudentAttendance
{
    public partial class Report : System.Web.UI.Page
    {
        string sqlQueryString;
        string fromDate, toDate;
        string detailsQuery;
        Boolean enableBind = false;
        public string fromDt, toDt;
        public string sDate,eDate;
        public string fromdateValue;
        string selectAllQuery;
        string headerText;
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
                    fromdatepicker.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    //todatepicker.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    fromDt = fromdatepicker.Value;
                  //  toDt = todatepicker.Value;

                    sDate = fromDt;
                    eDate = toDt;



                    this.bindDistrict();
                    this.bindMandal();
                    Label1.Visible = false;
                    Label2.Visible = false;
                    Label3.Visible = false;
                    Label4.Visible = false;
                    //Label5.Visible = false;
                    Label6.Visible = false;
                    Label7.Visible = false;
                    Panel2.Visible = false;
                }
            }

        }

        public void bindDistrict()
        {
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);

            try
            {
                string districtQuery;
                string isadmin = Convert.ToString(Session["isAdmin"]);
                if (isadmin == "1")
                {
                    districtQuery = "select districtID,districtName from tblDistrict";
                }
                else
                {
                    districtQuery = "select districtID,districtName from tblDistrict where schoolID='" + Session["id"] + "'";
                }

                using (MySqlCommand cmd = new MySqlCommand(districtQuery))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                ddlDistrict.DataSource = dt;
                                ddlDistrict.DataTextField = "districtName";
                                ddlDistrict.DataValueField = "districtID";
                                ddlDistrict.DataBind();
                                //ddlDistrict.Items.Insert(0, new ListItem("All"));
                            }
                            else
                            {
                                ddlDistrict.DataSource = null;
                                ddlDistrict.DataBind();
                            }
                        }
                    }
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


        public void bindMandal()
        {
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);

            try
            {
                string mandalQuery;
                string isadmin = Convert.ToString(Session["isAdmin"]);
                if (isadmin == "1")
                {
                    mandalQuery = "select mandalID,mandalName from tblMandal";
                }
                else
                {
                    mandalQuery = "select mandalID,mandalName from tblMandal where schoolID='" + Session["id"] + "'";
                }
                using (MySqlCommand cmd = new MySqlCommand(mandalQuery))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                ddlMandal.DataSource = dt;
                                ddlMandal.DataTextField = "mandalName";
                                ddlMandal.DataValueField = "mandalID";
                                ddlMandal.DataBind();
                                ddlMandal.Items.Insert(0, new ListItem("All"));
                            }
                            else
                            {
                                ddlMandal.DataSource = null;
                                ddlMandal.DataBind();
                            }
                        }
                    }
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

        public void bindSchool()
        {
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("select schoolID,schoolName from tblschool where mandalID='" + ddlMandal.SelectedValue + "'"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                ddlSchool.DataSource = dt;
                                ddlSchool.DataTextField = "schoolName";
                                ddlSchool.DataValueField = "schoolID";
                                ddlSchool.DataBind();
                                ddlSchool.Items.Insert(0, new ListItem("All"));
                            }
                            else
                            {
                                ddlSchool.DataSource = null;
                                ddlSchool.DataBind();
                            }
                        }
                    }
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

        public void bindClass()
        {
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("select classID,className from tblclass where schoolID='" + ddlSchool.SelectedValue + "'"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                ddlClass.DataSource = dt;
                                ddlClass.DataTextField = "className";
                                ddlClass.DataValueField = "classID";
                                ddlClass.DataBind();
                                ddlClass.Items.Insert(0, new ListItem("All"));
                            }
                            else
                            {
                                ddlClass.DataSource = null;
                                ddlClass.DataBind();
                            }
                        }
                    }
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


        //public void bindGender()
        //{
        //    MySqlConnection con = new MySqlConnection(GlobalVariables.connString);

        //    try
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand("select genderID,genderName from tblgender"))
        //        {
        //            using (MySqlDataAdapter sda = new MySqlDataAdapter())
        //            {
        //                cmd.Connection = con;
        //                con.Open();
        //                sda.SelectCommand = cmd;

        //                using (DataTable dt = new DataTable())
        //                {
        //                    sda.Fill(dt);
        //                    if (dt.Rows.Count > 0)
        //                    {

        //                        ddlGender.DataSource = dt;
        //                        ddlGender.DataTextField = "genderName";
        //                        ddlGender.DataValueField = "genderID";
        //                        ddlGender.DataBind();
        //                        ddlGender.Items.Insert(0, new ListItem("All"));
        //                    }
        //                    else
        //                    {
        //                        ddlGender.DataSource = null;
        //                        ddlGender.DataBind();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        protected void btngeneratereport_Click(object sender, EventArgs e)
        {
            this.bindgridview();
        }

        protected void bindgridview()
        {
            grdDetailReport.DataSource = null;
            grdDetailReport.DataBind();
            fromDt = fromdatepicker.Value;
            //toDt = todatepicker.Value;

            string startDate = "";
            string endDate = "";
            if (!string.IsNullOrEmpty(fromdatepicker.Value.Trim()))
            {
                // CONVERT DATE FORMAT.
                startDate = DateTime.ParseExact(
                    fromdatepicker.Value, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            }

            //if (!string.IsNullOrEmpty(todatepicker.Value.Trim()))
            //{
            //    // CONVERT DATE FORMAT.
            //    endDate = DateTime.ParseExact(
            //        todatepicker.Value, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            //}

            string script = "$(document).ready(function () { $('[id*=btngeneratereport]').click(); });";

            string fromDate = fromdatepicker.Value.ToString();
            DateTime fDate = Convert.ToDateTime(startDate);

            this.fromdateValue = fDate.Year + "-" + fDate.Day.ToString("#00") + "-" + fDate.Month.ToString("#00") + " 00:00:00";

            //string todate = todatepicker.Value.ToString();
            //DateTime tDate = Convert.ToDateTime(endDate);
            //string toDateValue = tDate.Year + "-" + tDate.Month + "-" + tDate.Day + " 23:59:59";

            string strQuery = string.Empty;
            //zstring strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);

            string username = Session["username"].ToString();

            try
            {
                if (Session["isAdmin"].ToString() == "1")
                {

                    this.selectAllQuery = "SELECT total_students.mandalid,total_students.student_district AS District,total_students.student_mandal AS Mandal,total_students.student_Udise as udise, total_students.total_count AS TotalStudents,IFNULL(Present,0) AS TotalPresent, (total_students.total_count - IFNULL(Present,0) ) AS TotalAbsent  FROM ( SELECT student_district, student_Udise, student_mandal,mandalid, COUNT(*) total_count FROM student_registration GROUP BY student_district, student_mandal ) total_students LEFT join ( SELECT DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y') attDate, reg.student_district, reg.student_mandal, COUNT(reg.student_unique_id) Present FROM student_attendance att, student_registration reg WHERE reg.student_unique_id = att.student_unique_id AND att.AttendanceDate >='" + fromdateValue + "' AND att.AttendanceDate <='" + fromdateValue + "' GROUP BY DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y'), reg.student_district, reg.student_mandal ) Stu_att ON total_students.student_mandal = Stu_att.student_mandal ORDER BY total_students.mandalid  ";
                }
                else
                {

                    this.selectAllQuery = "SELECT total_students.student_district as District, total_students.student_mandal Mandal,total_students.student_Udise as udise, total_students.mandalid as mandalid,total_students.total_count AS TotalStudents,IFNULL(Present,0) AS TotalPresent, (total_students.total_count - IFNULL(Present,0) ) AS TotalAbsent FROM ( SELECT student_district, student_Udise, student_mandal,mandalid, COUNT(*) total_count FROM student_registration WHERE student_registration.student_mandal='" + username + "' GROUP BY student_district, student_mandal ) total_students LEFT join ( SELECT DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y') attDate, reg.student_district, reg.student_mandal, COUNT(reg.student_unique_id) Present FROM student_attendance att, student_registration reg WHERE reg.student_unique_id = att.student_unique_id AND att.AttendanceDate >='" + fromdateValue + "' AND att.AttendanceDate <='" + fromdateValue + "'   GROUP BY DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y'), reg.student_district, reg.student_mandal ) Stu_att ON total_students.student_mandal = Stu_att.student_mandal   ORDER BY total_students.mandalid";
                }

                //if user dont select anything 
                if (ddlDistrict.SelectedItem.ToString() == "Kadapa" && ddlMandal.SelectedItem.ToString() == "All" && ddlSchool.SelectedItem.ToString() == "All" && ddlClass.SelectedItem.ToString() == "All")
                {
                    sqlQueryString = selectAllQuery;
                }
                //user selected one on every dropdown menu
                if (ddlMandal.SelectedItem.ToString() != "All" && ddlSchool.SelectedItem.ToString() == "All"  && ddlClass.SelectedItem.ToString() == "All")
                {
                    sqlQueryString = "SELECT total_students.student_district as District, total_students.student_mandal Mandal,total_students.student_Udise as udise,total_students.mandalid as mandalid,total_students.total_count AS TotalStudents,IFNULL(Present,0) AS TotalPresent, (total_students.total_count - IFNULL(Present,0) ) AS TotalAbsent FROM ( SELECT student_district,student_Udise,  student_mandal,mandalid, COUNT(*) total_count FROM student_registration WHERE student_registration.student_mandal='" + ddlMandal.SelectedItem.ToString() + "' GROUP BY student_district, student_mandal ) total_students LEFT join ( SELECT DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y') attDate, reg.student_district, reg.student_mandal, COUNT(reg.student_unique_id) Present FROM student_attendance att, student_registration reg WHERE reg.student_unique_id = att.student_unique_id AND att.AttendanceDate >='" + fromdateValue + "' AND att.AttendanceDate <='" + fromdateValue + "'   GROUP BY DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y'), reg.student_district, reg.student_mandal ) Stu_att ON total_students.student_mandal = Stu_att.student_mandal   ORDER BY total_students.mandalid";
                }
                //user selected district,mandal school,class
                if (ddlMandal.SelectedItem.ToString() != "All" && ddlSchool.SelectedItem.ToString() != "All" && ddlClass.SelectedItem.ToString() == "All" )
                {
                    sqlQueryString = "SELECT total_students.student_district as District, total_students.student_mandal Mandal,total_students.student_Udise as udise,total_students.mandalid as mandalid,total_students.total_count AS TotalStudents,IFNULL(Present,0) AS TotalPresent, (total_students.total_count - IFNULL(Present,0) ) AS TotalAbsent FROM ( SELECT student_district,student_Udise,  student_mandal,mandalid, COUNT(*) total_count FROM student_registration WHERE student_registration.student_mandal='" + ddlMandal.SelectedItem.ToString() + "' GROUP BY student_district, student_mandal ) total_students LEFT join ( SELECT DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y') attDate, reg.student_district, reg.student_mandal, COUNT(reg.student_unique_id) Present FROM student_attendance att, student_registration reg WHERE reg.student_unique_id = att.student_unique_id AND att.AttendanceDate >='" + fromdateValue + "' AND att.AttendanceDate <='" + fromdateValue + "'   GROUP BY DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y'), reg.student_district, reg.student_mandal ) Stu_att ON total_students.student_mandal = Stu_att.student_mandal   ORDER BY total_students.mandalid";
                }
                //user selected district,mandal school
                if (ddlMandal.SelectedItem.ToString() != "All" && ddlSchool.SelectedItem.ToString() != "All" && ddlClass.SelectedItem.ToString() != "All" )
                {
                    sqlQueryString = "SELECT total_students.student_district as District, total_students.student_mandal Mandal,total_students.student_Udise as udise,total_students.mandalid as mandalid,total_students.total_count AS TotalStudents,IFNULL(Present,0) AS TotalPresent, (total_students.total_count - IFNULL(Present,0) ) AS TotalAbsent FROM ( SELECT student_district,student_Udise,  student_mandal,mandalid, COUNT(*) total_count FROM student_registration WHERE student_registration.student_mandal='" + ddlMandal.SelectedItem.ToString() + "' AND student_registration.student_class='"+ddlClass.SelectedItem.ToString()+"' GROUP BY student_district, student_mandal ) total_students LEFT join ( SELECT DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y') attDate, reg.student_district, reg.student_mandal, COUNT(reg.student_unique_id) Present FROM student_attendance att, student_registration reg WHERE reg.student_unique_id = att.student_unique_id AND att.AttendanceDate >='" + fromdateValue + "' AND att.AttendanceDate <='" + fromdateValue + "' AND reg.student_class='" + ddlClass.SelectedItem.ToString() + "'  GROUP BY DATE_FORMAT(att.AttendanceDate,'%d-%b-%Y'), reg.student_district, reg.student_mandal ) Stu_att ON total_students.student_mandal = Stu_att.student_mandal   ORDER BY total_students.mandalid";
                }
                //user selected district,mandal
                if (ddlDistrict.SelectedItem.ToString() != "Kadapa" && ddlMandal.SelectedItem.ToString() != "All" && ddlSchool.SelectedItem.ToString() == "All"  && ddlClass.SelectedItem.ToString() == "All")
                {
                    sqlQueryString = "select(sr.student_district) as 'District',(SELECT COUNT(Distinct(student_registration.student_mandal)) FROM student_registration WHERE student_registration.student_mandal='" + ddlMandal.SelectedItem.ToString() + "') AS   Mandal,student_registration.student_Udise as udise,(SELECT COUNT(Distinct(student_registration.student_school)) FROM student_registration ) AS   NOofSchools,(SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = '" + ddlDistrict.SelectedItem.ToString() + "' AND student_registration.student_mandal='" + ddlMandal.SelectedItem.ToString() + "') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration   WHERE student_registration.student_district = '" + ddlDistrict.SelectedItem.ToString() + "' AND student_registration.student_mandal='" + ddlMandal.SelectedItem.ToString() + "')-count(sa.student_unique_id)) as 'Total Absent'   from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id   AND sr.student_district = '" + ddlDistrict.SelectedItem.ToString() + "'   AND sr.student_mandal='" + ddlMandal.SelectedItem.ToString() + "' AND sa.AttendanceDate >='" + fromdateValue + "' AND sa.AttendanceDate <='" + fromdateValue + "' group by sr.student_district ";
                }
                //user selected district,mandal
                if (ddlDistrict.SelectedItem.ToString() != "Kadapa" && ddlMandal.SelectedItem.ToString() == "All" && ddlSchool.SelectedItem.ToString() == "All" && ddlClass.SelectedItem.ToString() == "All")
                {
                    sqlQueryString = "select(sr.student_district) as 'District',(SELECT COUNT(Distinct(student_registration.student_mandal)) FROM student_registration) AS   Mandal,(SELECT COUNT(Distinct(student_registration.student_school)) FROM student_registration ) AS   NOofSchools,(SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = '" + ddlDistrict.SelectedItem.ToString() + "') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration   WHERE student_registration.student_district = '" + ddlDistrict.SelectedItem.ToString() + "' )-count(sa.student_unique_id)) as 'Total Absent'   from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id   AND sr.student_district = '" + ddlDistrict.SelectedItem.ToString() + "' AND sa.AttendanceDate >='" + fromdateValue + "' AND sa.AttendanceDate <='" + fromdateValue + "' group by sr.student_district ";
                }
                using (MySqlCommand cmd = new MySqlCommand(sqlQueryString))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                grdreport.DataSource = dt;
                                grdreport.DataBind();
                                this.enableBind = true;
                                Label1.Visible = true;
                                Label2.Visible = true;
                                Label3.Visible = true;
                                Label4.Visible = true;
                               // Label5.Visible = true;
                                Label6.Visible = true;
                                Label7.Visible = true;
                                lbldistrict.Text = ddlDistrict.SelectedItem.ToString();
                                lblmandal.Text = ddlMandal.SelectedItem.ToString();
                                lblschool.Text = ddlSchool.SelectedItem.ToString();
                                lblclass.Text = ddlClass.SelectedItem.ToString();
                                //lblgender.Text = ddlGender.SelectedItem.ToString();
                                lblfromdate.Text = fromdatepicker.Value;
                                lbltodate.Text = fromdatepicker.Value;//todatepicker.Value;
                                Panel2.Visible = false;
                            }
                            else
                            {
                                grdreport.DataSource = null;
                                grdreport.DataBind();
                                this.enableBind = false;
                                Label1.Visible = false;
                                Label2.Visible = false;
                                Label3.Visible = false;
                                Label4.Visible = false;
                                //Label5.Visible = false;
                                Label6.Visible = false;
                                Label7.Visible = false;
                                lbldistrict.Text = "";
                                lblmandal.Text = "";
                                lblschool.Text = "";
                                lblclass.Text = "";
                                //lblgender.Text = "";
                                lblfromdate.Text = "";
                                lbltodate.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                grdreport.DataSource = null;
                grdreport.DataBind();
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindMandal();
            if (ddlDistrict.SelectedValue == "Kadapa")
            {
                ddlMandal.Text = "All";
                ddlSchool.Text = "All";
                ddlClass.Text = "All";
               // ddlGender.Text = "All";
                ddlMandal.Enabled = true;
                ddlSchool.Enabled = false;
                ddlClass.Enabled = false;
                //ddlGender.Enabled = false;
            }
            else
            {
                ddlMandal.Enabled = true;
            }
        }

        protected void ddlMandal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMandal.SelectedValue != "All")
            {
                ddlSchool.Enabled = true;
                ddlClass.SelectedIndex = 0;
                ddlClass.Enabled = false;
            }
            else
            {
                ddlSchool.SelectedIndex = 0;
                ddlClass.SelectedIndex = 0;
                ddlSchool.Enabled = false;
                ddlClass.Enabled = false;
            }
            this.bindSchool();
        }

       

        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchool.SelectedValue != "All")
            {
                ddlClass.Enabled = true;
            }
            else
            {
                ddlClass.SelectedIndex = 0;
            }
            this.bindClass();

        }

       

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClass.SelectedValue != "All")
            {
               // ddlGender.Enabled = true;
            }
            //this.bindGender();
        }

        protected void grdreport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string script = "$(document).ready(function () { $('[id*=lbltotalstudents]').click(); });";
            string startDate = "";
            string endDate = "";
            if (!string.IsNullOrEmpty(fromdatepicker.Value.Trim()))
            {
                // CONVERT DATE FORMAT.
                startDate = DateTime.ParseExact(
                    fromdatepicker.Value, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            }


            string fromDate = fromdatepicker.Value.ToString();
            DateTime fDate = Convert.ToDateTime(startDate);

            this.fromdateValue = fDate.Year + "-" + fDate.Day.ToString("#00") + "-" + fDate.Month.ToString("#00") + " 00:00:00";

            Panel2.Visible = true;
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);
            string isadminEnable = Session["isAdmin"].ToString();
            int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string index = Convert.ToString(e.CommandName);
            int mandalID = Convert.ToInt32(grdreport.DataKeys[RowIndex].Values[0]);

            if (isadminEnable == "1")
            {
                if (mandalID == 1)
                {
                    if (e.CommandName == "totalstudents")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT student_unique_id as ID,student_name as Name,student_class as Class,student_medium as Medium,student_school as School FROM student_registration  where student_mandal='Pulivendula'";
                        }
                        else
                        {
                            detailsQuery = "SELECT student_unique_id as ID,student_name as Name,student_class as Class,student_medium as Medium,student_school as School FROM student_registration  where student_mandal='Pulivendula' and student_class='"+ddlClass.SelectedItem.ToString() +"'";
                        }
                        
                        lbldetailreporthead.Text= "Total Students";
                    }
                    if (e.CommandName == "totalpresent")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Pulivendula'";
                        }
                        else
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Pulivendula' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                        lbldetailreporthead.Text = "Present List";
                    }
                    if (e.CommandName == "totalAbsent")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id NOT IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Pulivendula'";
                        }
                        else
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id NOT IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Pulivendula' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                        lbldetailreporthead.Text = "Absent List";
                    }


                }
                else
                {


                    if (e.CommandName == "totalstudents")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {

                            detailsQuery = "SELECT student_unique_id as ID,student_name as Name,student_class as Class,student_medium as Medium,student_school as School FROM student_registration  where student_mandal='badvel'";
                        }
                        else
                        {
                            detailsQuery = "SELECT student_unique_id as ID,student_name as Name,student_class as Class,student_medium as Medium,student_school as School FROM student_registration  where student_mandal='badvel' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                        lbldetailreporthead.Text = "Total Students";
                    }
                    if (e.CommandName == "totalpresent")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Badvel'";
                        }
                        else
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Badvel' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                        lbldetailreporthead.Text = "Present List";
                    }
                    if (e.CommandName == "totalAbsent")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id NOT IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Badvel'";
                        }
                        else
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id NOT IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Badvel' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                        lbldetailreporthead.Text = "Absent List";
                    }
                }
            }
            else
            {
                if (Session["username"].ToString() == "Pulivendula")
                {
                    if (e.CommandName == "totalstudents")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT student_unique_id as ID,student_name as Name,student_class as Class,student_medium as Medium,student_school as School FROM student_registration  where student_mandal='Pulivendula'";
                        }
                        else
                        {
                            detailsQuery = "SELECT student_unique_id as ID,student_name as Name,student_class as Class,student_medium as Medium,student_school as School FROM student_registration  where student_mandal='Pulivendula' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                        lbldetailreporthead.Text = "Total Students";
                    }
                    if (e.CommandName == "totalpresent")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Pulivendula'";
                        }
                        else
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Pulivendula' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                            lbldetailreporthead.Text = "Present List";
                    }
                    if (e.CommandName == "totalAbsent")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id NOT IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Pulivendula'";
                        }
                        else
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id NOT IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Pulivendula' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                        lbldetailreporthead.Text = "Absent List";
                    }

                }
                else
                {
                    if (e.CommandName == "totalstudents")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT student_unique_id as ID,student_name as Name,student_class as Class,student_medium as Medium,student_school as School FROM student_registration  where student_mandal='badvel'";
                        }
                        else
                        {
                            detailsQuery = "SELECT student_unique_id as ID,student_name as Name,student_class as Class,student_medium as Medium,student_school as School FROM student_registration  where student_mandal='badvel' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                            lbldetailreporthead.Text = "Total Students";
                    }
                    if (e.CommandName == "totalpresent")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Badvel'";
                        }
                        else
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Badvel' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                            lbldetailreporthead.Text = "Present List";
                    }
                    if (e.CommandName == "totalAbsent")
                    {
                        if (Convert.ToString(ddlClass.SelectedItem.ToString()) == "All")
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id NOT IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Badvel'";
                        }
                        else
                        {
                            detailsQuery = "SELECT sr.student_unique_id as ID,sr.student_name as Name,sr.student_class as Class,sr.student_medium as Medium,sr.student_school as School FROM student_registration sr WHERE sr.student_unique_id NOT IN (SELECT st.student_unique_id from student_attendance st  WHERE st.AttendanceDate >='" + this.fromdateValue + "' AND st.AttendanceDate <='" + this.fromdateValue + "') AND sr.student_mandal='Badvel' and student_class='" + ddlClass.SelectedItem.ToString() + "'";
                        }
                            lbldetailreporthead.Text = "Absent List";
                    }
                }



            }

            using (MySqlCommand cmd = new MySqlCommand(detailsQuery))
            {
                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    cmd.Connection = con;
                    con.Open();
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdDetailReport.DataSource = dt;
                            grdDetailReport.DataBind();
                        }
                        else
                        {
                            grdDetailReport.DataSource = null;
                            grdDetailReport.DataBind();
                        }
                    }
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grdreport.AllowPaging = false;
                this.bindgridview();

                grdreport.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdreport.HeaderRow.Cells)
                {
                    cell.BackColor = grdreport.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdreport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdreport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdreport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grdreport.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}