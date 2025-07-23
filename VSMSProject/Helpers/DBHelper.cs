using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;

public static class DBHelper
{
    // Read connection string from web.config
    private static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    // Execute non-query SQL commands like INSERT, UPDATE, DELETE
    public static int ExecuteQuery(string query, SqlParameter[] parameters = null)
    {
        int rowsAffected = 0;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 180;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogError(query, parameters, ex);
                throw new Exception("Error while executing query: " + ex.Message);
            }
        }

        return rowsAffected;
    }

    public static DataTable ExecuteDataTable(string query, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 180;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }

    public static void ExecuteNonQuery(string query, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 180;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static T ExecuteScalar<T>(string query, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 180;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                return (T)cmd.ExecuteScalar();
            }
        }
    }

    private static void LogError(string query, SqlParameter[] parameters, Exception ex)
    {
        string parameterInfo = parameters != null
            ? string.Join(", ", parameters.Select(p => $"{p.ParameterName}={p.Value}"))
            : "No parameters";

        string logMessage = $"{DateTime.Now}: Query: {query}, Parameters: {parameterInfo}, Error: {ex.Message}\n";
        string logFilePath = HttpContext.Current.Server.MapPath("~/App_Data/errorLog.txt");

        try
        {
            System.IO.File.AppendAllText(logFilePath, logMessage);
        }
        catch
        {
            // Optional: handle logging failure silently
        }
    }

    public static void SetData(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 180;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogError(query, parameters, ex);
                throw new Exception($"Error executing query: {ex.Message} Query: {query}");
            }
        }
    }

    public static void LogLogin(string email)
    {
        string query = "INSERT INTO Audit (Email, Login_Time) VALUES (@Email, GETDATE())";
        SqlParameter[] parameters = {
            new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email }
        };

        ExecuteNonQuery(query, parameters);
    }

    public static void LogLogout(string email)
    {
        string query = @"
        UPDATE Audit
        SET Logout_Time = GETDATE()
        WHERE Email = @Email AND Logout_Time IS NULL
        AND AuditId = (
            SELECT TOP 1 AuditId
            FROM Audit
            WHERE Email = @Email AND Logout_Time IS NULL
            ORDER BY Login_Time DESC
        )";
        SqlParameter[] parameters = {
            new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email }
        };

        ExecuteNonQuery(query, parameters);
    }

    public static DataTable GetData(string query, SqlParameter[] parameters = null)
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 180;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        return dt;
    }
}



