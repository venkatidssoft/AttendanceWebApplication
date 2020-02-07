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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            GlobalVariables.connString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            txtusername.Focus();
            string script = "$(document).ready(function () { $('[id*=btnlogin]').onload(); });";
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            string script = "$(document).ready(function () { $('[id*=btnlogin]').click(); });";
            MySqlConnection con = new MySqlConnection(GlobalVariables.connString);
            try
            {

                string uid = txtusername.Text;
                string pass = txtpassword.Text;
                con.Open();
                string qry = "select * from cbuser where username='" + uid + "' and password='" + pass + "' and userDisable=0";
                MySqlCommand cmd = new MySqlCommand(qry, con);
                MySqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    Session["id"] = sdr["userID"];
                    Session["username"] = sdr["username"];
                    Session["isAdmin"] = sdr["isAdmin"];
                    Response.Redirect("homepage.aspx");
                    sdr.Close();
                    con.Close();
                }
                else
                {
                    lblError.Text = "Wrong UserName & Password Try again..!!";

                }
                //con.Close();
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