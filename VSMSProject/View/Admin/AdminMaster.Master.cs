using System;
using System.Web;

namespace VSMSProject.View.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the session contains the Admin's name
                if (Session["AdminName"] != null)
                {
                    NameTb.Text = $"Welcome, {Session["AdminName"].ToString()}";
                }
                else
                {
                    NameTb.Text = "Welcome, Admin";
                }
            }
        }
    }
}
