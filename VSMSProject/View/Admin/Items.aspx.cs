using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSMSProject.View.Admin
{
    public partial class Items : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateQualityDropdown();
                ShowItems();
            }
        }

        private void PopulateQualityDropdown()
        {
            // Clear any existing items
            QualityCb.Items.Clear();

            // Add predefined quality options
            QualityCb.Items.Add(new ListItem("Best", "Best"));
            QualityCb.Items.Add(new ListItem("Medium", "Medium"));
            QualityCb.Items.Add(new ListItem("Low", "Low"));
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Required for exporting GridView to certain formats (if applicable)
        }

        private void ShowItems()
        {
            string Query = "SELECT * FROM ItemTb1";
            ItemGV.DataSource = DBHelper.GetData(Query);
            ItemGV.DataBind();
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(INameTb.Value) || string.IsNullOrEmpty(QualityCb.SelectedValue) ||
                    string.IsNullOrEmpty(IPriceTb.Value) || string.IsNullOrEmpty(ItemQtyTb.Value))
                {
                    ErrMsg.InnerText = "Missing Data";
                }
                else
                {
                    string IName = INameTb.Value;
                    string IQuality = QualityCb.SelectedValue; // Value selected from dropdown
                    string IPrice = IPriceTb.Value;
                    string IQty = ItemQtyTb.Value;
                    string EDate = ExpDate.Value;

                    string Query = "INSERT INTO ItemTb1 (ItemName, ItemQuality, ItemPrice, ItemQuantity, ItemExpDate) " +
                                   "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')";
                    Query = string.Format(Query, IName, IQuality, IPrice, IQty, EDate);
                    DBHelper.SetData(Query);
                    ShowItems();
                    ErrMsg.InnerText = "Item Added Successfully!";
                    //SuccessMessageLabel.Visible = true; // Show success message
                }
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
               // SuccessMessageLabel.Visible = false; // Hide success message on error
            }
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (INameTb.Value=="")
                {
                    ErrMsg.InnerText = "Missing Data!";
                }
                else
                {
                    string Query = "DELETE FROM ItemTb1 WHERE ItemId={0}";
                    Query = string.Format(Query, ItemGV.SelectedRow.Cells[1].Text);
                    DBHelper.SetData(Query);
                    ShowItems();
                    ErrMsg.InnerText = "Item Deleted Successfully!";
                    //SuccessMessageLabel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
                //SuccessMessageLabel.Visible = false;
            }
        }

        private int Key = 0;

        protected void ItemGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            INameTb.Value = ItemGV.SelectedRow.Cells[2].Text;
            QualityCb.SelectedValue = ItemGV.SelectedRow.Cells[3].Text;
            IPriceTb.Value = ItemGV.SelectedRow.Cells[4].Text;
            ItemQtyTb.Value = ItemGV.SelectedRow.Cells[5].Text;
            ExpDate.Value = ItemGV.SelectedRow.Cells[6].Text;

            if (INameTb.Value == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ItemGV.SelectedRow.Cells[1].Text);
            }
        }

        

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (INameTb.Value == "" || QualityCb.SelectedValue == "" || IPriceTb.Value == "" || ItemQtyTb.Value == "")

                {
                    ErrMsg.InnerText = "Missing Data";
                }
                else
                {
                    string IName = INameTb.Value;
                    string IQuality = QualityCb.SelectedValue;
                    string IPrice = IPriceTb.Value;
                    string IQty = ItemQtyTb.Value;
                    string EDate = ExpDate.Value;



                    string Query = "UPDATE ItemTb1 SET ItemName='{0}', ItemQuality='{1}', ItemPrice='{2}', ItemQuantity='{3}', ItemExpDate='{4}' WHERE ItemId={5}";

                    Query = string.Format(Query, IName, IQuality, IPrice, IQty, EDate, ItemGV.SelectedRow.Cells[1].Text);
                    DBHelper.SetData(Query);
                    ShowItems();
                    ErrMsg.InnerText = "Item Updated Successfully!";
                    //SuccessMessageLabel.Visible = true; // Show success message
                }
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
                //SuccessMessageLabel.Visible = false; // Hide success message on error
            }
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            INameTb.Value = "";
            QualityCb.SelectedIndex = -1; // Reset to no selection
            IPriceTb.Value = "";
            ItemQtyTb.Value = "";
            ExpDate.Value = "";

            // Optionally, clear any error message if visible
            ErrMsg.InnerText = "";

            // Re-bind the GridView to display the items from the database
            ShowItems();
        }
    }
}



