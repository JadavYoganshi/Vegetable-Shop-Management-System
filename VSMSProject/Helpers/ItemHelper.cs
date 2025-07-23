using System.Data;

namespace VSMSProject.Helpers
{
    public static class ItemHelper
    {
        public static DataTable GetAvailableItems()
        {
            string query = "SELECT ItemId, ItemName, ItemPrice, ItemQuantity FROM ItemTb1 WHERE ItemQuantity > 0";
            return DBHelper.GetData(query);
        }
    }
}
