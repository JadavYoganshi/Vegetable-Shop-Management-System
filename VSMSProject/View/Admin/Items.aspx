<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="VSMSProject.View.Admin.Items" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Asset/Lib/Bootstrap/css/Items.css" />
    <style>
        /* Custom styles for spacing between logo and title */
        .page-header {
            display: flex;
            align-items: center;
        }

        .page-logo {
            width: 50px; /* Adjust logo size if needed */
            height: 50px;
            margin-right: 0px; /* Adjust this value to control the space between the logo and text */
        }

        .page-header h1 {
            margin: 0;
            font-size: 2.5rem; /* Adjust the font size of the title */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Mybody" runat="server">
    <div class="container-fluid">
        <!-- Page Header -->
        <div class="row align-items-center page-header">
    <div class="col-auto">
        <!-- Reduced gap by modifying margin-right -->
        <img src="../../Asset/Images/manage_item.png" class="page-logo" alt="Item Logo" />
    </div>
    <div class="col">
        <h1 class="text-primary">Manage Items</h1>
    </div>
</div>

        <!-- Item Form and Grid -->
        <div class="row mt-4">
            <!-- Form Section -->
            <div class="col-md-4">
                <h3 class="text-primary">Item Details</h3>
                <form id="ItemForm">
                    <div class="form-group">
                        <label for="INameTb">Item Name</label>
                        <input type="text" class="form-control" id="INameTb" runat="server" placeholder="Enter Item Name" />
                    </div></br>
                    <div class="form-group">
                        <label for="QualityCb">Item Quality</label>
                        <asp:DropDownList ID="QualityCb" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div></br>
                    <div class="form-group">
                        <label for="IPriceTb">Item Price (Per KG)</label>
                        <input type="number" class="form-control" id="IPriceTb" runat="server" placeholder="Enter Price per KG" />
                    </div></br>
                    <div class="form-group">
                        <label for="ItemQtyTb">Item Quantity</label>
                        <input type="number" class="form-control" id="ItemQtyTb" runat="server" placeholder="Enter Quantity" />
                    </div></br>
                    <div class="form-group">
                        <label for="ExpDate">Expiration Date</label>
                        <input type="date" class="form-control" id="ExpDate" runat="server" />
                    </div></br>
                    <div class="form-group text-danger">
                        <label id="ErrMsg" runat="server"></label>
                    </div>
                    
                    <div class="button-group">
                        <asp:Button ID="EditBtn" runat="server" CssClass="btn btn-primary" Text="Edit" OnClick="EditBtn_Click" />
                        <asp:Button ID="SaveBtn" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="SaveBtn_Click" />
                        <asp:Button ID="DeleteBtn" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="DeleteBtn_Click" />
                        <asp:Button ID="Refresh" runat="server" CssClass="btn btn-primary" Text="refresh" OnClick="Refresh_Click" />
                    </div>
                </form>
            </div>

            <!-- Table Section -->
            <div class="col-md-8">
                <h3 class="text-primary">Item List</h3>
                <asp:GridView ID="ItemGV" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnSelectedIndexChanged="ItemGV_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Select" />
                        <asp:BoundField DataField="ItemId" HeaderText="Item ID" />
                        <asp:BoundField DataField="ItemName" HeaderText="Name" />
                        <asp:BoundField DataField="ItemQuality" HeaderText="Quality" />
                        <asp:BoundField DataField="ItemPrice" HeaderText="Price" />
                        <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="ItemExpDate" HeaderText="Expiration Date" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <script src="../../Asset/Lib/Bootstrap/js/Items.js"></script>
</asp:Content>

