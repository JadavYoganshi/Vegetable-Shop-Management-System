using System;
using System.Data;
using System.Data.SqlClient;

namespace VSMSProject.Helpers
{
    public static class BillDataHelper
    {
        /// <summary>
        /// Adds a new bill to BillTb1 and returns the generated BillId.
        /// </summary>
        public static int AddBill(int customerId, decimal amount)
        {
            string query = @"
                INSERT INTO BillTb1 (BillDate, Customer, Amount)
                OUTPUT INSERTED.BillId
                VALUES (GETDATE(), @Customer, @Amount)";

            SqlParameter[] parameters = {
                new SqlParameter("@Customer", customerId),
                new SqlParameter("@Amount", amount)
            };

            // Explicitly specify the return type as 'int'
            return DBHelper.ExecuteScalar<int>(query, parameters); // Specify the type parameter explicitly
        }

        /// <summary>
        /// Adds a new entry to the BillDetails table.
        /// </summary>
        public static void AddBillDetail(int billId, int itemId, int quantity, decimal total)
        {
            string query = @"
                INSERT INTO BillDetails (BillId, ItemId, Quantity, Total)
                VALUES (@BillId, @ItemId, @Quantity, @Total)";

            SqlParameter[] parameters = {
                new SqlParameter("@BillId", billId),
                new SqlParameter("@ItemId", itemId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@Total", total)
            };

            DBHelper.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Retrieves all bills for a specific customer.
        /// </summary>
        public static DataTable GetBillsByCustomer(int customerId)
        {
            string query = "SELECT * FROM BillTb1 WHERE Customer = @Customer";
            SqlParameter[] parameters = {
                new SqlParameter("@Customer", customerId)
            };

            return DBHelper.GetData(query, parameters);
        }

        /// <summary>
        /// Retrieves all details for a specific bill.
        /// </summary>
        public static DataTable GetBillDetails(int billId)
        {
            string query = @"
                SELECT bd.DetailId, bd.ItemId, bd.Quantity, bd.Total, i.ItemName, i.ItemPrice
                FROM BillDetails bd
                JOIN ItemTb1 i ON bd.ItemId = i.ItemId
                WHERE bd.BillId = @BillId";

            SqlParameter[] parameters = {
                new SqlParameter("@BillId", billId)
            };

            return DBHelper.GetData(query, parameters);
        }

        /// <summary>
        /// Updates the total amount of a bill.
        /// </summary>
        public static void UpdateBillAmount(int billId, decimal newAmount)
        {
            string query = "UPDATE BillTb1 SET Amount = @Amount WHERE BillId = @BillId";
            SqlParameter[] parameters = {
                new SqlParameter("@BillId", billId),
                new SqlParameter("@Amount", newAmount)
            };

            DBHelper.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Deletes a bill and its associated details.
        /// </summary>
        public static void DeleteBill(int billId)
        {
            string deleteDetailsQuery = "DELETE FROM BillDetails WHERE BillId = @BillId";
            string deleteBillQuery = "DELETE FROM BillTb1 WHERE BillId = @BillId";

            SqlParameter[] parameters = {
                new SqlParameter("@BillId", billId)
            };

            DBHelper.ExecuteNonQuery(deleteDetailsQuery, parameters);
            DBHelper.ExecuteNonQuery(deleteBillQuery, parameters);
        }
    }
}
