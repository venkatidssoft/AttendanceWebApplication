using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentAttendance
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["id"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    Label1.Text = Session["username"].ToString();
                    string isadminStatus = Session["isAdmin"].ToString();
                    if (isadminStatus =="1")
                    {
                        studentUpload.Visible = true;
                    }
                    else
                    {
                        studentUpload.Visible = false;
                        AttendanceUpload.Visible = false;
                    }
                    
                }
            }
            
        }
        public void onclick()
        {
            Session.Clear();
            Response.Redirect("login.aspx");
            
        }
    }
}