using System;
using System.Data;

namespace VSMSProject.View.Customer
{
    public partial class PrintBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Redirect if session data is missing
                if (Session["BillDetails"] == null || Session["GrandTotal"] == null || Session["BillId"] == null)
                {
                    Response.Redirect("~/View/Customer/Billing.aspx");
                }
            }
        }
    }
}
