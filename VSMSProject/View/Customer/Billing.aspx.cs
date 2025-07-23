/*using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using VSMSProject.BusinessLogic;

namespace VSMSProject.View.Customer
{
    public partial class Billing : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GetLoggedInCustomerId() <= 0)
                {
                    Response.Redirect("~/View/Login.aspx");
                }
                BindItemDropdown();
                InitializeBillTable();
                DateTimeLabel.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        private int GetLoggedInCustomerId()
        {
            if (Session["CustomerId"] != null)
            {
                return Convert.ToInt32(Session["CustomerId"]);
            }
            return -1; // Customer not logged in
        }

        private void InitializeBillTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("ID", typeof(int)),
                new DataColumn("ItemId", typeof(int)),
                new DataColumn("Item", typeof(string)),
                new DataColumn("Price", typeof(decimal)),
                new DataColumn("Quantity", typeof(int)),
                new DataColumn("Quality", typeof(string)),
                new DataColumn("Total", typeof(decimal))
            });

            ViewState["Bill"] = dt;
            BindGrid();
        }

        private void BindGrid()
        {
            DataTable dt = ViewState["Bill"] as DataTable;
            if (dt != null)
            {
                BillGV.DataSource = dt;
                BillGV.DataBind();
            }
        }

        private void BindItemDropdown()
        {
            string query = "SELECT ItemId, ItemName FROM ItemTb1 WHERE ItemQuantity > 0";
            DataTable items = ExecuteDataTable(query);
            if (items.Rows.Count > 0)
            {
                DataRow defaultRow = items.NewRow();
                defaultRow["ItemId"] = 0;
                defaultRow["ItemName"] = "--- Select an Item ---";
                items.Rows.InsertAt(defaultRow, 0);

                ItemDropdown.DataSource = items;
                ItemDropdown.DataTextField = "ItemName";
                ItemDropdown.DataValueField = "ItemId";
                ItemDropdown.DataBind();
            }
            else
            {
                ItemDropdown.Items.Insert(0, new ListItem("---Select an Item---", "0"));
            }
        }

        protected void AddToBillBtn_Click(object sender, EventArgs e)
        {
            int itemId = Convert.ToInt32(ItemDropdown.SelectedValue);
            if (itemId == 0)
            {
                ErrMsg.Text = "Please select an item!";
                return;
            }

            if (!int.TryParse(QtyTb.Value, out int quantity) || quantity <= 0)
            {
                ErrMsg.Text = "Please enter a valid quantity!";
                return;
            }

            DataTable bill = ViewState["Bill"] as DataTable;
            if (bill == null) return;

            DataTable itemDetails = ExecuteDataTable($"SELECT ItemName, ItemPrice, ItemQuantity, ItemQuality FROM ItemTb1 WHERE ItemId={itemId}");
            if (itemDetails.Rows.Count > 0)
            {
                int availableQuantity = Convert.ToInt32(itemDetails.Rows[0]["ItemQuantity"]);
                if (quantity > availableQuantity)
                {
                    ErrMsg.Text = "Insufficient stock!";
                    return;
                }

                string itemName = itemDetails.Rows[0]["ItemName"].ToString();
                decimal price = Convert.ToDecimal(itemDetails.Rows[0]["ItemPrice"]);
                string quality = itemDetails.Rows[0]["ItemQuality"].ToString();
                decimal total = price * quantity;

                // Check if item already exists in the bill
                DataRow existingRow = bill.AsEnumerable().FirstOrDefault(row => row.Field<int>("ItemId") == itemId);
                if (existingRow != null)
                {
                    // Update the existing row
                    int existingQuantity = existingRow.Field<int>("Quantity");
                    existingRow["Quantity"] = existingQuantity + quantity;
                    existingRow["Total"] = price * (existingQuantity + quantity);
                }
                else
                {
                    // Add new row
                    DataRow newRow = bill.NewRow();
                    newRow["ItemId"] = itemId;
                    newRow["Item"] = itemName;
                    newRow["Price"] = price;
                    newRow["Quantity"] = quantity;
                    newRow["Quality"] = quality;
                    newRow["Total"] = total;
                    bill.Rows.Add(newRow);
                }

                ViewState["Bill"] = bill;
                BindGrid();
                UpdateGrandTotal();
            }
        }

        private void UpdateGrandTotal()
        {
            DataTable bill = ViewState["Bill"] as DataTable;
            decimal grandTotal = bill.AsEnumerable().Sum(row => row.Field<decimal>("Total"));
            GrandTotalLabel.Text = grandTotal.ToString("C");
        }

        protected void ItemDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemId = Convert.ToInt32(ItemDropdown.SelectedValue);
            if (itemId > 0)
            {
                DataTable itemDetails = ExecuteDataTable($"SELECT ItemPrice, ItemQuantity, ItemQuality FROM ItemTb1 WHERE ItemId={itemId}");
                if (itemDetails.Rows.Count > 0)
                {
                    ItPriceTb.Value = itemDetails.Rows[0]["ItemPrice"].ToString();
                    ItQtyTb.Value = itemDetails.Rows[0]["ItemQuantity"].ToString();
                    ItQualityTb.Value = itemDetails.Rows[0]["ItemQuality"].ToString();
                }
            }
        }

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            ItemDropdown.SelectedIndex = 0;
            ItPriceTb.Value = string.Empty;
            ItQtyTb.Value = string.Empty;
            ItQualityTb.Value = string.Empty;
            QtyTb.Value = string.Empty;
            ErrMsg.Text = string.Empty;
        }

        private DataTable ExecuteDataTable(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        protected void PrintBillBtn_Click(object sender, EventArgs e)
        {
            DataTable bill = ViewState["Bill"] as DataTable;
            if (bill == null || bill.Rows.Count == 0)
            {
                ErrMsg.Text = "Please add items to the bill before printing.";
                return;
            }

            // Insert Bill Summary into BillTb1
            int customerId = GetLoggedInCustomerId();
            decimal totalAmount = bill.AsEnumerable().Sum(row => row.Field<decimal>("Total"));
            DateTime billDate = DateTime.Now;

            try
            {
                // Insert bill summary
                int billId = BillingLogic.InsertBill(customerId, totalAmount);

                // Insert each Bill Detail into BillDetails table
                foreach (DataRow row in bill.Rows)
                {
                    int itemId = Convert.ToInt32(row["ItemId"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    decimal total = Convert.ToDecimal(row["Total"]);

                    BillingLogic.InsertBillDetail(billId, itemId, quantity, total);
                }

                // If everything is successful, show success message
                ErrMsg.Text = "Bill stored successfully.....";

                // Trigger the print dialog using JavaScript
                string script = "window.print();";
                ClientScript.RegisterStartupScript(this.GetType(), "PrintOperation", script, true);
            }
            catch (Exception ex)
            {
                ErrMsg.Text = "Failed to store the bill: " + ex.Message;
            }
        }

    }
}*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using VSMSProject.BusinessLogic;

namespace VSMSProject.View.Customer
{
    public partial class Billing : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GetLoggedInCustomerId() <= 0)
                {
                    Response.Redirect("~/View/Login.aspx");
                }
                BindItemDropdown();
                InitializeBillTable();
                //DateTimeLabel.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        private int GetLoggedInCustomerId()
        {
            if (Session["CustomerId"] != null)
            {
                return Convert.ToInt32(Session["CustomerId"]);
            }
            return -1; // Customer not logged in
        }

        private void InitializeBillTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("ID", typeof(int)),
                new DataColumn("ItemId", typeof(int)),
                new DataColumn("Item", typeof(string)),
                new DataColumn("Price", typeof(decimal)),
                new DataColumn("Quantity", typeof(int)),
                new DataColumn("Quality", typeof(string)),
                new DataColumn("Total", typeof(decimal))
            });

            ViewState["Bill"] = dt;
            BindGrid();
        }

        private void BindGrid()
        {
            DataTable dt = ViewState["Bill"] as DataTable;
            if (dt != null)
            {
                BillGV.DataSource = dt;
                BillGV.DataBind();
            }
        }

        private void BindItemDropdown()
        {
            string query = "SELECT ItemId, ItemName FROM ItemTb1 WHERE ItemQuantity > 0";
            DataTable items = ExecuteDataTable(query);
            if (items.Rows.Count > 0)
            {
                DataRow defaultRow = items.NewRow();
                defaultRow["ItemId"] = 0;
                defaultRow["ItemName"] = "--- Select an Item ---";
                items.Rows.InsertAt(defaultRow, 0);

                ItemDropdown.DataSource = items;
                ItemDropdown.DataTextField = "ItemName";
                ItemDropdown.DataValueField = "ItemId";
                ItemDropdown.DataBind();
            }
            else
            {
                ItemDropdown.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---Select an Item---", "0"));
            }
        }
        protected void AddToBillBtn_Click(object sender, EventArgs e)
        {
            int itemId = Convert.ToInt32(ItemDropdown.SelectedValue);
            if (itemId == 0)
            {
                ErrMsg.Text = "Please select an item!";
                return;
            }

            if (!int.TryParse(QtyTb.Value, out int quantity) || quantity <= 0)
            {
                ErrMsg.Text = "Please enter a valid quantity!";
                return;
            }

            DataTable bill = ViewState["Bill"] as DataTable;
            if (bill == null) return;

            DataTable itemDetails = ExecuteDataTable($"SELECT ItemName, ItemPrice, ItemQuantity, ItemQuality FROM ItemTb1 WHERE ItemId={itemId}");
            if (itemDetails.Rows.Count > 0)
            {
                int availableQuantity = Convert.ToInt32(itemDetails.Rows[0]["ItemQuantity"]);
                if (quantity > availableQuantity)
                {
                    ErrMsg.Text = "Insufficient stock!";
                    return;
                }

                string itemName = itemDetails.Rows[0]["ItemName"].ToString();
                decimal price = Convert.ToDecimal(itemDetails.Rows[0]["ItemPrice"]);
                string quality = itemDetails.Rows[0]["ItemQuality"].ToString();
                decimal total = price * quantity;

                // Check if item already exists in the bill
                DataRow existingRow = bill.AsEnumerable().FirstOrDefault(row => row.Field<int>("ItemId") == itemId);
                if (existingRow != null)
                {
                    // Update the existing row
                    int existingQuantity = existingRow.Field<int>("Quantity");
                    existingRow["Quantity"] = existingQuantity + quantity;
                    existingRow["Total"] = price * (existingQuantity + quantity);
                }
                else
                {
                    // Add new row
                    DataRow newRow = bill.NewRow();
                    newRow["ItemId"] = itemId;
                    newRow["Item"] = itemName;
                    newRow["Price"] = price;
                    newRow["Quantity"] = quantity;
                    newRow["Quality"] = quality;
                    newRow["Total"] = total;
                    bill.Rows.Add(newRow);
                }

                ViewState["Bill"] = bill;
                BindGrid();
                UpdateGrandTotal();

                // Update the stock in the database
                UpdateItemStock(itemId, quantity);

                // Refresh the dropdown to show updated stock
                BindItemDropdown();
            }
        }


        private void UpdateItemStock(int itemId, int purchasedQuantity)
        {
            string query = "UPDATE ItemTb1 SET ItemQuantity = ItemQuantity - @PurchasedQuantity WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PurchasedQuantity", purchasedQuantity);
                    cmd.Parameters.AddWithValue("@ItemId", itemId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }


        private void UpdateGrandTotal()
        {
            DataTable bill = ViewState["Bill"] as DataTable;
            decimal grandTotal = bill.AsEnumerable().Sum(row => row.Field<decimal>("Total"));
            GrandTotalLabel.Text = grandTotal.ToString("C");
        }

        protected void ItemDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemId = Convert.ToInt32(ItemDropdown.SelectedValue);
            if (itemId > 0)
            {
                DataTable itemDetails = ExecuteDataTable($"SELECT ItemPrice, ItemQuantity, ItemQuality FROM ItemTb1 WHERE ItemId={itemId}");
                if (itemDetails.Rows.Count > 0)
                {
                    ItPriceTb.Value = itemDetails.Rows[0]["ItemPrice"].ToString();
                    ItQtyTb.Value = itemDetails.Rows[0]["ItemQuantity"].ToString();
                    ItQualityTb.Value = itemDetails.Rows[0]["ItemQuality"].ToString();
                }
            }
        }

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            ItemDropdown.SelectedIndex = 0;
            ItPriceTb.Value = string.Empty;
            ItQtyTb.Value = string.Empty;
            ItQualityTb.Value = string.Empty;
            QtyTb.Value = string.Empty;
            ErrMsg.Text = string.Empty;
        }

        protected void PrintBillBtn_Click(object sender, EventArgs e)
        {
            DataTable bill = ViewState["Bill"] as DataTable;
            if (bill == null || bill.Rows.Count == 0)
            {
                ErrMsg.Text = "Please add items to the bill before printing.";
                return;
            }

            int customerId = GetLoggedInCustomerId();
            decimal totalAmount = bill.AsEnumerable().Sum(row => row.Field<decimal>("Total"));
            DateTime billDate = DateTime.Now;

            try
            {
                // Save the bill in the database
                int billId = BillingLogic.InsertBill(customerId, totalAmount);
                foreach (DataRow row in bill.Rows)
                {
                    int itemId = Convert.ToInt32(row["ItemId"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    decimal total = Convert.ToDecimal(row["Total"]);

                    BillingLogic.InsertBillDetail(billId, itemId, quantity, total);
                }

                // Generate PDF
                string fileName = GenerateBillPDF(billId, customerId, bill, totalAmount, billDate);

                // Trigger download for the generated PDF
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.TransmitFile(Server.MapPath($"~/GeneratedBills/{fileName}"));
                Response.End();
            }
            catch (Exception ex)
            {
                ErrMsg.Text = "Failed to save and generate the bill: " + ex.Message;
            }
        }

        private string GenerateBillPDF(int billId, int customerId, DataTable bill, decimal totalAmount, DateTime billDate)
        {
            string fileName = $"Bill_{billId}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Server.MapPath($"~/GeneratedBills/{fileName}");

            // Ensure the directory exists
            string directoryPath = Server.MapPath("~/GeneratedBills/");
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                document.Open();

                // Add title
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                var thankYouFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);

                Paragraph title = new Paragraph("Vegetable Shop Management System", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);
                document.Add(new Paragraph("\n"));

                // Add customer and bill details in a table
                PdfPTable detailsTable = new PdfPTable(2)
                {
                    WidthPercentage = 50,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                detailsTable.SetWidths(new float[] { 1.5f, 2 }); // Adjust column width proportions

                detailsTable.AddCell(new PdfPCell(new Phrase("Bill ID:", boldFont)) { Border = Rectangle.NO_BORDER });
                detailsTable.AddCell(new PdfPCell(new Phrase(billId.ToString(), normalFont)) { Border = Rectangle.NO_BORDER });

                detailsTable.AddCell(new PdfPCell(new Phrase("Customer ID:", boldFont)) { Border = Rectangle.NO_BORDER });
                detailsTable.AddCell(new PdfPCell(new Phrase(customerId.ToString(), normalFont)) { Border = Rectangle.NO_BORDER });

                detailsTable.AddCell(new PdfPCell(new Phrase("Date:", boldFont)) { Border = Rectangle.NO_BORDER });
                detailsTable.AddCell(new PdfPCell(new Phrase(billDate.ToString("dd-MM-yyyy hh:mm tt"), normalFont)) { Border = Rectangle.NO_BORDER });

                document.Add(detailsTable);
                document.Add(new Paragraph("\n"));

                // Add the bill items table
                PdfPTable itemsTable = new PdfPTable(5)
                {
                    WidthPercentage = 100,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                itemsTable.SetWidths(new float[] { 2, 1, 1, 1, 1 });

                // Add header row with styling
                PdfPCell headerCell = new PdfPCell
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };

                headerCell.Phrase = new Phrase("Item Name", boldFont);
                itemsTable.AddCell(headerCell);

                headerCell.Phrase = new Phrase("Price", boldFont);
                itemsTable.AddCell(headerCell);

                headerCell.Phrase = new Phrase("Quantity", boldFont);
                itemsTable.AddCell(headerCell);

                headerCell.Phrase = new Phrase("Quality", boldFont);
                itemsTable.AddCell(headerCell);

                headerCell.Phrase = new Phrase("Total", boldFont);
                itemsTable.AddCell(headerCell);

                // Add bill items
                foreach (DataRow row in bill.Rows)
                {
                    itemsTable.AddCell(new PdfPCell(new Phrase(row["Item"].ToString(), normalFont)));
                    itemsTable.AddCell(new PdfPCell(new Phrase(row["Price"].ToString(), normalFont)));
                    itemsTable.AddCell(new PdfPCell(new Phrase(row["Quantity"].ToString(), normalFont)));
                    itemsTable.AddCell(new PdfPCell(new Phrase(row["Quality"].ToString(), normalFont)));
                    itemsTable.AddCell(new PdfPCell(new Phrase(row["Total"].ToString(), normalFont)));
                }
                document.Add(itemsTable);

                document.Add(new Paragraph("\n"));
                Paragraph totalParagraph = new Paragraph($"Grand Total: ₹{totalAmount}", boldFont)
                {
                    Alignment = Element.ALIGN_RIGHT
                };
                document.Add(totalParagraph);

                document.Add(new Paragraph("\n"));
                Paragraph thankYouMessage = new Paragraph("Thank you for purchasing vegetables! We hope to see you again.", thankYouFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 20
                };
                document.Add(thankYouMessage);

                document.Close();
            }

            return fileName;
        }


        private void AddTableCell(PdfPTable table, string text, bool isHeader = false)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, FontFactory.GetFont(FontFactory.HELVETICA, isHeader ? 12 : 10, isHeader ? Font.BOLD : Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 4,
                BackgroundColor = isHeader ? BaseColor.LIGHT_GRAY : BaseColor.WHITE,
                BorderWidth = 1
            };
            table.AddCell(cell);
        }


        private DataTable ExecuteDataTable(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}


