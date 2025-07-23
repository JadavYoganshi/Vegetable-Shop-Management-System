using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSMSProject.View.Customer
{
    public partial class CustomerMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CustomerName"] != null)
                {
                    NameTb.Text = $"Welcome, {Session["CustomerName"]}";
                }
                else
                {
                    NameTb.Text = "Welcome, Guest";
                }
            }
        }

    }
}