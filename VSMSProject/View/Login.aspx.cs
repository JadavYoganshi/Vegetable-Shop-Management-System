using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace VSMSProject.View
{
    public partial class Login : System.Web.UI.Page
    {
        public static string CustomerName = ""; // Updated naming for clarity
        public static int CustomerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialization logic, if required.
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            string email = EmailId.Value.Trim();
            string password = UserPasswordTb.Value.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                InfoMsg.InnerText = "Please fill all the fields.";
                return;
            }
            if (AdminRadio.Checked)
            {
                if (email == "Admin@gmail.com" && password == "Admin")
                {
                    DBHelper.LogLogin(email); // Log admin login
                    Session["Email"] = email;
                    Session["AdminName"] = "Admin";
                    Response.Redirect("~/View/Admin/Customers.aspx");
                }
                else
                {
                    InfoMsg.InnerText = "Invalid Admin credentials!";
                }
            }
            else if (CustomerRadio.Checked)
            {
                string query = $"SELECT CustId, CustName FROM CustomerTb1 WHERE CustEmail = '{email}' AND CustPassword = '{password}'";
                DataTable dt = DBHelper.GetData(query);

                if (dt.Rows.Count == 0)
                {
                    InfoMsg.InnerText = "Invalid Customer credentials or you are not registered!";
                }
                else
                {
                    string customerName = dt.Rows[0]["CustName"].ToString();
                    int customerId = Convert.ToInt32(dt.Rows[0]["CustId"]);

                    DBHelper.LogLogin(email); // Log customer login
                    Session["CustomerId"] = customerId; // Save Customer ID in session
                    Session["CustomerName"] = customerName; // Save Customer Name in session
                    Response.Redirect("~/View/Customer/Billing.aspx");
                }
            }

            else
            {
                InfoMsg.InnerText = "Please select a role to log in.";
            }
        }

        // Method to log the login time into the Audit table
        private void LogAudit(string email, string role)
        {
            try
            {
                // Insert audit log with the email, role, and login time
                string query = $"INSERT INTO Audit (Email, Login_Time) VALUES ('{email}', GETDATE())";
                DBHelper.SetData(query); // Assuming DBHelper.SetData executes the query
            }
            catch (Exception ex)
            {
                // Log or handle exceptions appropriately
                InfoMsg.InnerText = $"Error logging audit: {ex.Message}";
            }
        }

        // Method to log the logout time in the Audit table
        private void LogAuditLogout(string email)
        {
            try
            {
                // Update only the latest record for the user where Logout_Time is NULL
                string query = @"UPDATE Audit 
                         SET Logout_Time = GETDATE() 
                         WHERE Email = @Email AND Logout_Time IS NULL 
                         AND Login_Time = (
                             SELECT MAX(Login_Time) 
                             FROM Audit 
                             WHERE Email = @Email AND Logout_Time IS NULL
                         )";
                SqlParameter[] parameters = {
            new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email }
        };
                DBHelper.SetData(query, parameters);
            }
            catch (Exception ex)
            {
                InfoMsg.InnerText = $"Error updating logout time: {ex.Message}";
            }
        }

        // Logout function
        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Email"] != null)
                {
                    string email = Session["Email"].ToString();

                    // Log the logout time in the Audit table
                    LogAuditLogout(email);

                    // Clear session variables
                    Session.Clear();
                    Session.Abandon();

                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    InfoMsg.InnerText = "You are not logged in!";
                }
            }
            catch (Exception ex)
            {
                InfoMsg.InnerText = $"Error during logout: {ex.Message}";
            }
        }

    }
}


