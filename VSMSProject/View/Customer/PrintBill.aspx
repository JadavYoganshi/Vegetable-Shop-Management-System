<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintBill.aspx.cs" Inherits="VSMSProject.View.Customer.PrintBill" %>
<!DOCTYPE html>
<html>
<head>
    <title>Print Bill</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .header {
            text-align: center;
            margin-bottom: 20px;
        }
        .header h1 {
            margin: 0;
            color: #007bff;
        }
        .bill-info {
            margin-bottom: 20px;
        }
        .bill-info label {
            font-weight: bold;
        }
        .bill-table {
            width: 100%;
            border-collapse: collapse;
        }
        .bill-table th, .bill-table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: center;
        }
        .bill-table th {
            background-color: #f8f9fa;
        }
        .grand-total {
            font-weight: bold;
            text-align: right;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <div class="header">
        <h1>Vegetable Shop Management System</h1>
        <h4>Bill Receipt</h4>
    </div>
    <div class="bill-info">
        <p><label>Bill ID:</label> <span><%= Session["BillId"] %></span></p>
        <p><label>Date & Time:</label> <span><%= DateTime.Now.ToString("f") %></span></p>
    </div>
    <table class="bill-table">
        <thead>
            <tr>
                <th>#</th>
                <th>Item</th>
                <th>Price</th>
                <th>Quantity</th>
                <th>Quality</th>
                <th>Total</th>
            </tr>
        </thead>
        <tbody>
            <% 
                if (Session["BillDetails"] is System.Data.DataTable billDetails)
                {
                    int index = 1;
                    foreach (System.Data.DataRow row in billDetails.Rows)
                    {
            %>
            <tr>
                <td><%= index++ %></td>
                <td><%= row["Item"] %></td>
                <td><%= row["Price"] %></td>
                <td><%= row["Quantity"] %></td>
                <td><%= row["Quality"] %></td>
                <td><%= row["Total"] %></td>
            </tr>
            <% 
                    }
                }
            %>
        </tbody>
    </table>
    <div class="grand-total">
        <p>Grand Total: <span><%= Session["GrandTotal"] %></span></p>
    </div>
    <script>
        window.onload = function () {
            // Auto-print the page
            window.print();
        };
    </script>
</body>
</html>
