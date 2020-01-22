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
                using (MySqlCommand cmd = new MySqlCommand("select sr.student_district as 'District',sr.student_mandal as 'Mandal' ,Count(sr.student_school) as 'No of Schools',count(sr.student_unique_id) as 'Total Students',count(sa.student_unique_id) as 'Total Present',(count(sr.student_unique_id)-count(sa.student_unique_id)) as 'Total Absent'from student_registration sr,student_attendance sa where sa.inTime is NOT null and sr.student_unique_id=sa.student_unique_id and student_mandal='" + ddlMandal.SelectedItem + "'  AND  sr.student_medium='" + ddlMedium.SelectedItem + "' group by sr.student_district,sr.student_mandal,sr.student_school"))
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
                                //this.grdreport.Columns[0].Visible = false;
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