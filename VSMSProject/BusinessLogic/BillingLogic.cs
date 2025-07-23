using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace VSMSProject.BusinessLogic
{
    public static class BillingLogic
    {
        // Updates the item quantity in the ItemTb1 table
        public static void UpdateItemQuantity(int itemId, int newQuantity)
        {
            try
            {
                string query = "EXEC UpdateItemQuantity @ItemId, @NewQuantity";
                SqlParameter[] parameters = {
                    new SqlParameter("@ItemId", itemId),
                    new SqlParameter("@NewQuantity", newQuantity)
                };
                DBHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateItemQuantity: {ex.Message}");
                throw;
            }
        }

        // Fetches items from the ItemTb1 table that have a quantity greater than 0
        public static DataTable GetUpdatedItemList()
        {
            try
            {
                string query = "SELECT ItemId, ItemName, ItemQuantity FROM ItemTb1 WHERE ItemQuantity > 0";
                return DBHelper.GetData(query);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetUpdatedItemList: {ex.Message}");
                throw;
            }
        }

        // Fetches details of a specific item by ID
        public static DataTable GetItemDetails(int itemId)
        {
            try
            {
                string query = "SELECT ItemPrice, ItemQuality, ItemQuantity FROM ItemTb1 WHERE ItemId = @ItemId";
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ItemId", itemId) };
                return DBHelper.GetData(query, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetItemDetails: {ex.Message}");
                throw;
            }
        }

        /* public static DataTable GetItemById(int itemId)
         {
             string query = "SELECT * FROM ItemTb1 WHERE ItemId = @ItemId";
             SqlParameter[] parameters = new SqlParameter[]
             {
                 new SqlParameter("@ItemId", itemId)
             };

          } */


        // Inserts a new bill into BillTb1 and returns the new BillId
        public static int InsertBill(int customerId, decimal totalAmount)
        {
            try
            {
                string query = @"
                INSERT INTO BillTb1 (BillDate, Customer, Amount) 
                OUTPUT INSERTED.BillId 
                VALUES (GETDATE(), @Customer, @Amount)";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Customer", customerId),
                    new SqlParameter("@Amount", totalAmount)
                };

                return DBHelper.ExecuteScalar<int>(query, parameters); // Returns the new BillId
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in InsertBill: {ex.Message}");
                throw;
            }
        }

        // Checks if an item exists in the database
        public static bool ItemExists(int itemId)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM ItemTb1 WHERE ItemId = @ItemId";
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ItemId", itemId) };

                int result = DBHelper.ExecuteScalar<int>(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ItemExists: {ex.Message}");
                throw;
            }
        }

        // Inserts a new entry into the BillDetails table and updates the item quantity
        public static void InsertBillDetail(int billId, int itemId, int quantity, decimal total)
        {
            try
            {
                // Insert into BillDetails table
                string query = @"
                    INSERT INTO BillDetails (BillId, ItemId, Quantity, Total) 
                    VALUES (@BillId, @ItemId, @Quantity, @Total)";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@BillId", billId),
                    new SqlParameter("@ItemId", itemId),
                    new SqlParameter("@Quantity", quantity),
                    new SqlParameter("@Total", total)
                };

                DBHelper.ExecuteNonQuery(query, parameters);

                // Update the item quantity in the inventory
                DataTable itemDetails = DBHelper.ExecuteDataTable("SELECT ItemQuantity FROM ItemTb1 WHERE ItemId = @ItemId", new SqlParameter("@ItemId", itemId));
                int availableQuantity = Convert.ToInt32(itemDetails.Rows[0]["ItemQuantity"]);
                DBHelper.ExecuteNonQuery("UPDATE ItemTb1 SET ItemQuantity = @NewQuantity WHERE ItemId = @ItemId",
                    new SqlParameter("@NewQuantity", availableQuantity - quantity),
                    new SqlParameter("@ItemId", itemId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in InsertBillDetail: {ex.Message}");
                throw;
            }
        }
    }
}
