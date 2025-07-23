/*using System;
using System.Web.UI;

namespace VSMSProject.View.Admin
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetItems();
                SumSell();
                GetCustomers();
                GetCustomer();
            }
        }

        private void GetCustomer()
        {
            string Query = "Select * from CustomerTb1";
            var customerData = DBHelper.GetData(Query);
            CustomerCb.DataSource = customerData;
            CustomerCb.DataTextField = "CustName";
            CustomerCb.DataValueField = "CustId";
            CustomerCb.DataBind();
        }

        private void GetItems()
        {
            string Query = "Select Count(*) from ItemTb1";
            var result = DBHelper.GetData(Query);
            INumTb.InnerText = result.Rows[0][0].ToString();
        }

        private void GetCustomers()
        {
            string Query = "Select Count(*) from CustomerTb1";
            var result = DBHelper.GetData(Query);
            CustNumTb.InnerText = result.Rows[0][0].ToString();
        }

        private void SumSell()
        {
            string Query = "Select Sum(Amount) from BillTb1";
            var result = DBHelper.GetData(Query);
            FinanceTb.InnerText = "Rs " + (result.Rows[0][0] != DBNull.Value ? result.Rows[0][0].ToString() : "0");
        }

        private void SumSellByCustomer()
        {
            if (!string.IsNullOrEmpty(CustomerCb.SelectedValue))
            {
                string Query = $"Select Sum(Amount) from BillTb1 where Customer = {CustomerCb.SelectedValue}";
                var result = DBHelper.GetData(Query);
                TotalTb.InnerText = "Rs " + (result.Rows[0][0] != DBNull.Value ? result.Rows[0][0].ToString() : "0");
            }
            else
            {
                TotalTb.InnerText = "Rs 0";
            }
        }

        protected void CustomerCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SumSellByCustomer();
        }
    }
}*/

using System;
using System.Web.UI;

namespace VSMSProject.View.Admin
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }

        /// <summary>
        /// Load all dashboard data on initial load.
        /// </summary>
        private void LoadDashboardData()
        {
            GetItemsCount();
            GetTotalSales();
            GetCustomerCount();
            LoadCustomersDropdown();
        }

        /// <summary>
        /// Load the total number of items.
        /// </summary>
        private void GetItemsCount()
        {
            string query = "SELECT COUNT(*) FROM ItemTb1";
            var result = DBHelper.GetData(query);
            INumTb.InnerText = result.Rows[0][0].ToString();
        }

        /// <summary>
        /// Load the total sales amount.
        /// </summary>
        private void GetTotalSales()
        {
            string query = "SELECT SUM(Amount) FROM BillTb1";
            var result = DBHelper.GetData(query);
            FinanceTb.InnerText = "Rs " + (result.Rows[0][0] != DBNull.Value ? result.Rows[0][0].ToString() : "0");
        }

        /// <summary>
        /// Load the total number of customers.
        /// </summary>
        private void GetCustomerCount()
        {
            string query = "SELECT COUNT(*) FROM CustomerTb1";
            var result = DBHelper.GetData(query);
            CustNumTb.InnerText = result.Rows[0][0].ToString();
        }

        /// <summary>
        /// Populate the dropdown with customer names.
        /// </summary>
        private void LoadCustomersDropdown()
        {
            string query = "SELECT * FROM CustomerTb1";
            var customerData = DBHelper.GetData(query);
            CustomerCb.DataSource = customerData;
            CustomerCb.DataTextField = "CustName";
            CustomerCb.DataValueField = "CustId";
            CustomerCb.DataBind();
        }

        /// <summary>
        /// Calculate and display the total sales amount for the selected customer.
        /// </summary>
        private void GetTotalSalesByCustomer()
        {
            if (!string.IsNullOrEmpty(CustomerCb.SelectedValue))
            {
                string query = $"SELECT SUM(Amount) FROM BillTb1 WHERE Customer = {CustomerCb.SelectedValue}";
                var result = DBHelper.GetData(query);
                TotalTb.InnerText = "Rs " + (result.Rows[0][0] != DBNull.Value ? result.Rows[0][0].ToString() : "0");
            }
            else
            {
                TotalTb.InnerText = "Rs 0";
            }
        }

        protected void CustomerCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalSalesByCustomer();
        }
    }
}


