

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VSMSProject.View.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="icon" href="../../Asset/Images/favicon1.png" type="image/x-icon" />
    <link rel="stylesheet" href="../Asset/Lib/Bootstrap/css/bootstrap.min.css" />
    <style>
    body {
        margin: 0;
        padding: 0;
        height: 100vh;
        display: flex;
        align-items: center;
        justify-content: center;
        position: relative;
        overflow: hidden;
    }

    body::before {
        content: "";
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-image: url('../Asset/Images/login&register.jpg');
        background-size: cover;
        background-position: center;
        opacity: 0.7;
        z-index: -1;
    }

    .form-container {
        background-color: rgba(255, 255, 255, 0.9);
        padding: 20px;
        border-radius: 10px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        width: 400px;
        z-index: 1;
    }

    .form-container h1 {
        text-align: center;
        font-family: 'Poppins', sans-serif;
        font-weight: bold;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h1>Login</h1>
            <div class="mb-3">
                <label for="EmailId" class="form-label">Email address</label>
                <input type="email" class="form-control" id="EmailId" runat="server" required="required">
            </div>
            <div class="mb-3">
                <label for="UserPasswordTb" class="form-label">Password</label>
                <input type="password" class="form-control" id="UserPasswordTb" runat="server" required="required">
            </div>
            <div class="mb-3">
                <div class="d-flex">
                    <div class="form-check me-3">
                        <input type="radio" class="form-check-input" id="CustomerRadio" name="Role" runat="server">
                        <label class="form-check-label" for="CustomerRadio">Customer</label>
                    </div>
                    <div class="form-check">
                        <input type="radio" class="form-check-input" id="AdminRadio" checked="true" name="Role" runat="server">
                        <label class="form-check-label" for="AdminRadio">Admin</label>
                    </div>
                </div>
            </div>
            <div class="mb-3 d-grid">
                <label id="InfoMsg" runat="server" class="text-danger"></label>
                <asp:Button Text="Login" CssClass="btn btn-primary btn-block" runat="server" ID="SaveBtn" OnClick="SaveBtn_Click" />
            </div>
            <div class="text-center">
                <a href="Registration.aspx">Don't have an account? Register</a>
            </div>
        </div>
    </form>
</body>
</html>
