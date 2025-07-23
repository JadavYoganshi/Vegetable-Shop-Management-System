<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="VSMSProject.View.Admin.Customers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Asset/Lib/Bootstrap/css/Customers.css" />
    <style>
    /* Custom styles for spacing between logo and title */
    .page-header {
        display: flex;
        align-items: center;
    }

    .page-logo {
        width: 35px; /* Adjust logo size if needed */
        height: 35px;
        margin-right: 0px; /* Adjust this value to control the space between the logo and text */
    }

    .page-header h1 {
        margin: 0;
        font-size: 4rem; /* Adjust the font size of the title */
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Mybody" runat="server">
    <div class="container-fluid">
        <!-- Page Header -->
 <div class="row align-items-center page-header">
     <div class="col-auto text-left">
         <!-- Larger logo -->
         <img src="../../Asset/Images/customer.png" class="page-logo img-fluid" alt="Item Logo" style="height: 70px; width: auto;" />
     </div>
     <div class="col text-left">
         <!-- Manage Items text aligned to the left -->
         <h1 class="text-primary" style="margin: 0; font-size: 2rem;">Manage Items</h1>
     </div>
 </div>

        <!-- Table Section -->
        <div class="row mt-4">
            <div class="col-md-12">
                <h3 class="text-primary">Customer List</h3>
                <!-- GridView to display customer list -->
                <asp:GridView ID="CustomerGV" runat="server" CssClass="table table-bordered table-hover"
                    AutoGenerateColumns="False" DataKeyNames="CustId" OnRowDeleting="CustomerGV_RowDeleting"
                    OnRowDataBound="CustomerGV_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete"
                                    CssClass="btn btn-danger" 
                                    OnClientClick="return confirm('Are you sure you want to delete this customer?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CustId" HeaderText="Customer ID" SortExpression="CustId" />
                        <asp:BoundField DataField="CustName" HeaderText="Name" SortExpression="CustName" />
                        <asp:BoundField DataField="CustEmail" HeaderText="Email" SortExpression="CustEmail" />
                        <asp:BoundField DataField="CustPassword" HeaderText="Password" SortExpression="CustPassword" />
                        <asp:BoundField DataField="CustPhone" HeaderText="Phone" SortExpression="CustPhone" />
                        <asp:BoundField DataField="CustAddress" HeaderText="Address" SortExpression="CustAddress" />
                    </Columns>
                </asp:GridView>

                <!-- Error Message Section -->
                <div class="form-group text-danger">
                    <label id="ErrMsg" runat="server"></label>
                </div>
            </div>
        </div>
    </div>

    <!-- Include external JavaScript -->
    <script src="../../Asset/Lib/Bootstrap/js/Customers.js"></script>
</asp:Content>

