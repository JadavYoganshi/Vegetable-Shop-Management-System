<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="VSMSProject.View.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Linking Bootstrap and Icon Libraries -->
    <link href="../../Asset/Lib/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <style>
        /* Custom Styles for Dashboard */
        .dashboard-logo {
            height: 50px;
            width: auto;
        }

        .dashboard-card {
            border: none;
            border-radius: 10px;
            color: #fff;
            text-align: center;
            transition: transform 0.3s, box-shadow 0.3s;
        }

        .dashboard-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
        }

        .dashboard-icon {
            font-size: 3rem;
            margin-bottom: 10px;
        }

        .custom-dropdown {
            font-size: 1.2rem;
            padding: 10px;
        }

        /* Increase the font size of the dashboard title */
        .dashboard-title {
            font-size: 2rem;  /* Adjust the size as needed */
            color: #007bff;  /* Optional: Set a color for the title */
        }

        /* Responsiveness */
        @media (max-width: 768px) {
            .dashboard-card {
                margin-bottom: 20px;
            }

            .dashboard-title {
                font-size: 2rem; /* Adjust title size on smaller screens */
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Mybody" runat="server">
    <div class="container-fluid">
        <!-- Header Section -->
        <div class="row bg-light shadow-sm mb-4">
            <div class="col-md-12 d-flex align-items-center py-3">
                <img src="../../Asset/Images/dashboard.png" alt="Dashboard Logo" class="dashboard-logo rounded me-3">
                <!-- Apply the 'dashboard-title' class to make the title larger -->
                <h1 class="dashboard-title mb-0">Vegetable Shop Dashboard</h1>
            </div>
        </div>

        <!-- Dashboard Summary Section -->
        <div class="row text-center">
            <!-- Items Card -->
            <div class="col-md-4">
                <div class="card dashboard-card bg-danger">
                    <div class="card-body">
                        <i class="fas fa-carrot dashboard-icon"></i>
                        <h3>Items</h3>
                        <h1 runat="server" id="INumTb">0</h1>
                    </div>
                </div>
            </div>
            <!-- Finance Card -->
            <div class="col-md-4">
                <div class="card dashboard-card bg-primary">
                    <div class="card-body">
                        <i class="fas fa-wallet dashboard-icon"></i>
                        <h3>Finance</h3>
                        <h1 runat="server" id="FinanceTb">Rs 0</h1>
                    </div>
                </div>
            </div>
            <!-- Customers Card -->
            <div class="col-md-4">
                <div class="card dashboard-card bg-success">
                    <div class="card-body">
                        <i class="fas fa-users dashboard-icon"></i>
                        <h3>Customers</h3>
                        <h1 runat="server" id="CustNumTb">0</h1>
                    </div>
                </div>
            </div>
        </div>

        <!-- Dropdown and Total Sales Section -->
        <div class="row mt-5">
            <div class="col-md-6 mx-auto">
                <div class="input-group mb-3">
                    <label class="input-group-text" for="CustomerCb">Customer</label>
                    <asp:DropDownList ID="CustomerCb" runat="server" class="form-select custom-dropdown" OnSelectedIndexChanged="CustomerCb_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <!-- Total Sales Card -->
        <div class="row text-center">
            <div class="col-md-6 mx-auto">
                <div class="card dashboard-card bg-secondary">
                    <div class="card-body">
                        <i class="fas fa-chart-line dashboard-icon"></i>
                        <h3>Total Sales</h3>
                        <h1 runat="server" id="TotalTb">Rs 0</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
