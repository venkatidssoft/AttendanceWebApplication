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
    public partial class Report : System.Web.UI.Page
    {
        string sqlQueryString;
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

        protected void btngeneratereport_Click(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
            //zstring strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);
            try
            {
                //if user dont select anything 
                if (ddlDistrict.SelectedItem.ToString() == "All" && ddlMandal.SelectedItem.ToString() == "All" && ddlSchool.SelectedItem.ToString() == "All" && ddlGender.SelectedItem.ToString() == "All" && ddlClass.SelectedItem.ToString() == "All")
                {
                    sqlQueryString = "select(sr.student_district) as 'District',(SELECT COUNT(Distinct(student_registration.student_mandal)) FROM student_registration) AS   Mandal,(SELECT COUNT(Distinct(student_registration.student_school)) FROM student_registration) AS   NOofSchools,(SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = 'kadapa')-count(sa.student_unique_id)) as 'Total Absent' from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id AND sr.student_district = 'kadapa' group by sr.student_district ";
                }
                //user selected one on every dropdown menu
                if (ddlDistrict.SelectedItem.ToString() != "All" && ddlMandal.SelectedItem.ToString() != "All" && ddlSchool.SelectedItem.ToString() != "All" && ddlGender.SelectedItem.ToString() != "All" && ddlClass.SelectedItem.ToString() != "All")
                {
                    sqlQueryString = "select(sr.student_district) as 'District',(SELECT COUNT(Distinct(student_registration.student_mandal)) FROM student_registration WHERE student_registration.student_mandal='" + ddlMandal.SelectedValue + "') AS   Mandal,(SELECT COUNT(Distinct(student_registration.student_school)) FROM student_registration WHERE student_registration.student_school='" + ddlSchool.SelectedValue + "') AS   NOofSchools,(SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = '" + ddlDistrict.SelectedValue + "' AND student_registration.student_school='" + ddlSchool.SelectedValue + "' AND student_registration.student_sex='" + ddlGender.SelectedValue + "') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration   WHERE student_registration.student_district = '" + ddlDistrict.SelectedValue + "' AND student_registration.student_school='" + ddlSchool.SelectedValue + "' AND student_registration.student_sex='" + ddlGender.SelectedValue + "')-count(sa.student_unique_id)) as 'Total Absent'   from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id   AND sr.student_district = '" + ddlDistrict.SelectedValue + "'   AND sr.student_mandal='" + ddlMandal.SelectedValue + "'   AND sr.student_school='" + ddlSchool.SelectedValue + "' AND sr.student_class='" + ddlClass.SelectedValue + "' AND sr.student_sex='" + ddlGender.SelectedValue + "'     group by sr.student_district ";
                }
                //user selected district,mandal school,class
                if (ddlDistrict.SelectedItem.ToString() != "All" && ddlMandal.SelectedItem.ToString() != "All" && ddlSchool.SelectedItem.ToString() != "All" && ddlClass.SelectedItem.ToString() != "All" && ddlGender.SelectedItem.ToString() == "All")
                {
                    sqlQueryString = "select(sr.student_district) as 'District',(SELECT COUNT(Distinct(student_registration.student_mandal)) FROM student_registration WHERE student_registration.student_mandal='" + ddlMandal.SelectedValue + "') AS   Mandal,(SELECT COUNT(Distinct(student_registration.student_school)) FROM student_registration WHERE student_registration.student_school='" + ddlSchool.SelectedValue + "') AS   NOofSchools,(SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = '" + ddlDistrict.SelectedValue + "' AND student_registration.student_school='" + ddlSchool.SelectedValue + "'AND student_registration.student_mandal='" + ddlMandal.SelectedValue + "'AND student_registration.student_class='" + ddlClass.SelectedValue + "' ) as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration   WHERE student_registration.student_district = '" + ddlDistrict.SelectedValue + "' AND student_registration.student_school='" + ddlSchool.SelectedValue + "'AND student_registration.student_mandal='" + ddlMandal.SelectedValue + "'AND student_registration.student_class='" + ddlClass.SelectedValue + "' )-count(sa.student_unique_id)) as 'Total Absent'   from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id   AND sr.student_district = '" + ddlDistrict.SelectedValue + "'   AND sr.student_mandal='" + ddlMandal.SelectedValue + "'   AND sr.student_school='" + ddlSchool.SelectedValue + "' AND sr.student_class='" + ddlClass.SelectedValue + "'     group by sr.student_district ";
                }
                //user selected district,mandal school
                if (ddlDistrict.SelectedItem.ToString() != "All" && ddlMandal.SelectedItem.ToString() != "All" && ddlSchool.SelectedItem.ToString() != "All" && ddlGender.SelectedItem.ToString() == "All" && ddlClass.SelectedItem.ToString() == "All")
                {
                    sqlQueryString = "select(sr.student_district) as 'District',(SELECT COUNT(Distinct(student_registration.student_mandal)) FROM student_registration WHERE student_registration.student_mandal='" + ddlMandal.SelectedValue + "') AS   Mandal,(SELECT COUNT(Distinct(student_registration.student_school)) FROM student_registration WHERE student_registration.student_school='" + ddlSchool.SelectedValue + "') AS   NOofSchools,(SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = '" + ddlDistrict.SelectedValue + "' AND student_registration.student_school='" + ddlSchool.SelectedValue + "') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration   WHERE student_registration.student_district = '" + ddlDistrict.SelectedValue + "' AND student_registration.student_school='" + ddlSchool.SelectedValue + "')-count(sa.student_unique_id)) as 'Total Absent'   from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id   AND sr.student_district = '" + ddlDistrict.SelectedValue + "'   AND sr.student_mandal='" + ddlMandal.SelectedValue + "'   AND sr.student_school='" + ddlSchool.SelectedValue + "'  group by sr.student_district ";
                }
                //user selected district,mandal
                if (ddlDistrict.SelectedItem.ToString() != "All" && ddlMandal.SelectedItem.ToString() != "All" && ddlSchool.SelectedItem.ToString() == "All" && ddlGender.SelectedItem.ToString() == "All" && ddlClass.SelectedItem.ToString() == "All")
                {
                    sqlQueryString = "select(sr.student_district) as 'District',(SELECT COUNT(Distinct(student_registration.student_mandal)) FROM student_registration WHERE student_registration.student_mandal='" + ddlMandal.SelectedValue + "') AS   Mandal,(SELECT COUNT(Distinct(student_registration.student_school)) FROM student_registration ) AS   NOofSchools,(SELECT COUNT(student_registration.student_unique_id) FROM student_registration  WHERE student_registration.student_district = '" + ddlDistrict.SelectedValue + "' AND student_registration.student_mandal='" + ddlMandal.SelectedValue + "') as 'Total Students', count(sa.student_unique_id) as 'Total Present',((SELECT COUNT(student_registration.student_unique_id) FROM student_registration   WHERE student_registration.student_district = '" + ddlDistrict.SelectedValue + "' AND student_registration.student_mandal='" + ddlMandal.SelectedValue + "')-count(sa.student_unique_id)) as 'Total Absent'   from student_registration sr,student_attendance sa where sa.inTime is NOT NULL and sr.student_unique_id = sa.student_unique_id   AND sr.student_district = '" + ddlDistrict.SelectedValue + "'   AND sr.student_mandal='" + ddlMandal.SelectedValue + "' group by sr.student_district ";
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
                            }
                            else
                            {
                                grdreport.DataSource = null;
                                grdreport.DataBind();
                            }
                        }
                    }
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