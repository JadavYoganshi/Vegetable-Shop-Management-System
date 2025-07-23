using System;
using System.Web.UI.WebControls;

namespace VSMSProject.View.Admin
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect if not logged in or not admin
           // if (Session["Email"] == null || Session["Role"]?.ToString() != "Admin")
            //{
             //   Response.Redirect("../Login.aspx");
            //}

            if (!IsPostBack)
            {
                ShowCustomers();
            }
        }

        private void ShowCustomers()
        {
            string query = "SELECT * FROM CustomerTb1";
            CustomerGV.DataSource = DBHelper.GetData(query);
            CustomerGV.DataBind();
        }

        protected void CustomerGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int customerId = Convert.ToInt32(CustomerGV.DataKeys[e.RowIndex].Value);
                string query = $"DELETE FROM CustomerTb1 WHERE CustId = {customerId}";
                DBHelper.SetData(query);
                ShowCustomers();
                ErrMsg.InnerText = "Customer deleted successfully!";
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = "Error while deleting customer: " + ex.Message;
            }
        }

        protected void CustomerGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Text = "******";
                if (e.Row.Cells[2].Text.Contains("@gmail.com"))
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                }

                if (e.Row.Cells[0].Text == "1001")
                {
                    Button deleteButton = e.Row.FindControl("DeleteButton") as Button;
                    if (deleteButton != null)
                    {
                        deleteButton.Enabled = false;
                        deleteButton.ToolTip = "This customer cannot be deleted";
                    }
                }
            }
        }
    }
}

