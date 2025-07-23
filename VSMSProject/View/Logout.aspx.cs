using System;

namespace VSMSProject.View
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AuditID"] != null)
            {
                LogAuditLogout(); // Update logout time
            }

            // Clear all sessions
            Session.Clear();
            Session.Abandon();

            // Redirect to login page
            Response.Redirect("Login.aspx");
        }

        private void LogAuditLogout()
        {
            try
            {
                if (Session["AuditID"] != null)
                {
                    int auditId = Convert.ToInt32(Session["AuditID"]);
                    string query = $"UPDATE Audit SET Logout_Time = GETDATE() WHERE AuditId = {auditId}";
                    DBHelper.SetData(query); // Assuming `SetData` executes non-select queries
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error
                // Response.Write($"Error updating logout time: {ex.Message}");
            }
        }
    }
}
