using System;
using System.Data;

namespace VSMSProject.View
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public static string CName = "";
        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(NameTb.Value) ||
                    string.IsNullOrWhiteSpace(EmailTb.Value) ||
                    string.IsNullOrWhiteSpace(PasswordTb.Value) ||
                    string.IsNullOrWhiteSpace(ConfirmPasswordTb.Value) ||
                    string.IsNullOrWhiteSpace(PhoneTb.Text) ||  // Use Text property
                    string.IsNullOrWhiteSpace(AddressTb.Value))
                {
                    ErrorMsg.InnerText = "All fields are required.";
                    return;
                }

                // Validate password and confirm password match
                if (PasswordTb.Value != ConfirmPasswordTb.Value)
                {
                    ErrorMsg.InnerText = "Passwords do not match.";
                    return;
                }

                // Validate phone number
                if (PhoneTb.Text.Length != 10 || !long.TryParse(PhoneTb.Text, out _))  // Use Text property
                {
                    ErrorMsg.InnerText = "Phone number must be exactly 10 digits.";
                    return;
                }

                // Get input values
                string customerName = NameTb.Value.Trim();
                string customerEmail = EmailTb.Value.Trim();
                string customerPassword = PasswordTb.Value.Trim();
                string customerPhone = PhoneTb.Text.Trim();  // Use Text property
                string customerAddress = AddressTb.Value.Trim();

                // Check if the email is already registered
                string checkQuery = $"SELECT * FROM CustomerTb1 WHERE CustEmail = '{customerEmail}'";
                DataTable data = DBHelper.GetData(checkQuery);
                if (data.Rows.Count > 0)
                {
                    ErrorMsg.InnerText = "Email is already registered.";
                    return;
                }

                // Insert the new customer into the database
                string query = $"INSERT INTO CustomerTb1 (CustName, CustEmail, CustPassword, CustPhone, CustAddress) " +
                               $"VALUES ('{customerName}', '{customerEmail}', '{customerPassword}', '{customerPhone}', '{customerAddress}')";
                int rowsAffected = DBHelper.ExecuteQuery(query);

                if (rowsAffected > 0)
                {
                    // Redirect to login page after successful registration
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    ErrorMsg.InnerText = "Registration failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg.InnerText = "Error: " + ex.Message;
            }
        }
    }
}
