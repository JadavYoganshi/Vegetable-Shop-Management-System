<%@ Page Title="" Language="C#" MasterPageFile="~/View/Customer/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="VSMSProject.View.Customer.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .custom-card {
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            border-radius: 8px;
            margin: 10px 0;
        }

        .custom-header {
            background-color: #007bff;
            color: white;
            font-size: 1.4em;
            text-align: center;
            padding: 10px;
            border-radius: 8px 8px 0 0;
        }

        .btn-custom {
            background-color: #28a745;
            color: white;
            border: none;
        }

        .btn-custom:hover {
            background-color: #218838;
        }

        .grand-total {
            background-color: #ffc107;
            font-weight: bold;
            padding: 10px;
            border-radius: 8px;
        }

        @media print {
            .no-print {
                display: none;
            }

            body {
                font-family: Arial, sans-serif;
                background-color: white;
            }

            .container {
                max-width: 100%;
            }

            .custom-header {
                background-color: #007bff;
                color: white;
                font-size: 1.4em;
                text-align: center;
                padding: 12px;
            }

            .grand-total {
                font-size: 1.4em;
                text-align: right;
                margin-top: 20px;
            }

            .bill-table th,
            .bill-table td {
                padding: 8px;
                text-align: center;
                border: 1px solid #ddd;
            }

            .bill-table th {
                background-color: #f8f9fa;
                font-weight: bold;
            }

            .text-end,
            .text-center {
                text-align: center !important;
            }

            h1, h4 {
                text-align: center;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Mybody" runat="server">
    <div class="container mt-4">
        <h1 class="text-center mb-4 text-primary">Customer Billing</h1>
        <div class="row">
            <!-- Form Section -->
            <div class="col-md-5 no-print">
                <div class="card custom-card">
                    <div class="custom-header">Item Details</div>
                    <div class="card-body">
                        <div class="mb-3">
                            <label for="ItemDropdown" class="form-label">Select Item</label>
                            <asp:DropDownList ID="ItemDropdown" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ItemDropdown_SelectedIndexChanged">
                                <asp:ListItem Text="--Select an Item--" Value="0" />
                            </asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <label for="ItPriceTb" class="form-label">Item Price</label>
                            <input type="text" class="form-control" id="ItPriceTb" runat="server" ReadOnly="true" />
                        </div>
                        <div class="mb-3">
                            <label for="ItQtyTb" class="form-label">Available Quantity</label>
                            <input type="text" class="form-control" id="ItQtyTb" runat="server" ReadOnly="true" />
                        </div>
                        <div class="mb-3">
                            <label for="ItQualityTb" class="form-label">Item Quality</label>
                            <input type="text" class="form-control" id="ItQualityTb" runat="server" ReadOnly="true" />
                        </div>
                        <div class="mb-3">
                            <label for="QtyTb" class="form-label">Enter Quantity</label>
                            <input type="text" class="form-control" id="QtyTb" runat="server" required="required" />
                        </div>
                        <asp:Label ID="ErrMsg" runat="server" CssClass="text-danger"></asp:Label>
                        <div class="d-flex justify-content-between">
                            <asp:Button Text="Add to Bill" CssClass="btn btn-custom" runat="server" ID="AddToBillBtn" OnClick="AddToBillBtn_Click" />
                            <asp:Button Text="Reset" CssClass="btn btn-secondary" runat="server" ID="ResetBtn" OnClick="ResetBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Bill Section -->
            <div class="col-md-7">
                <div class="card custom-card">
                    <div class="custom-header">Customer Bill</div>
                    <div class="card-body">
                        <%--<p class="text-end">
                            Date/Time: <asp:Label ID="DateTimeLabel" runat="server"></asp:Label>
                        </p>
                        <p class="text-end">
                            Bill ID: <asp:Label ID="BillIdLabel" runat="server"></asp:Label>
                        </p>--%>
                        <asp:GridView ID="BillGV" runat="server" CssClass="table table-striped bill-table" AutoGenerateColumns="False" ShowFooter="true">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="#" />
                                <asp:BoundField DataField="Item" HeaderText="Item" />
                                <asp:BoundField DataField="Price" HeaderText="Price" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="Quality" HeaderText="Quality" />
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="text-end">
                            <h4 class="grand-total">Grand Total: <asp:Label ID="GrandTotalLabel" runat="server" /></h4>
                        </div>
                        <div class="text-center mt-3">
                            <asp:Button Text="Print Bill" CssClass="btn btn-primary btn-lg no-print" ID="PrintBillBtn" OnClick="PrintBillBtn_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
      <a href="../Login.aspx">
    </div>
</asp:Content>

