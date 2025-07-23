<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="VSMSProject.View.Registration" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
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

        /* Background image with low opacity using pseudo-element */
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
            opacity: 0.7; /* Set opacity of the image */
            z-index: -1; /* Send to back */
        }

        .form-container {
            background-color: rgba(255, 255, 255, 0.9); /* Slightly transparent white */
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            width: 400px;
            z-index: 1; /* Ensure it is above the background */
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
            <h1>Register</h1>
            <div class="mb-3">
                <label for="NameTb" class="form-label">Full Name</label>
                <input type="text" class="form-control" id="NameTb" runat="server" required />
            </div>
            <div class="mb-3">
                <label for="EmailTb" class="form-label">Email Address</label>
                <input type="email" class="form-control" id="EmailTb" runat="server" required />
            </div>
            <div class="mb-3">
                <label for="PasswordTb" class="form-label">Password</label>
                <input type="password" class="form-control" id="PasswordTb" runat="server" required />
            </div>
            <div class="mb-3">
                <label for="ConfirmPasswordTb" class="form-label">Confirm Password</label>
                <input type="password" class="form-control" id="ConfirmPasswordTb" runat="server" required />
            </div>
            <div class="mb-3">
                <label for="PhoneTb" class="form-label">Phone Number</label>
                <asp:TextBox ID="PhoneTb" runat="server" CssClass="form-control" MaxLength="10" OnKeyPress="return isNumberKey(event);" />
            </div>
            <div class="mb-3">
                <label for="AddressTb" class="form-label">Address</label>
                <textarea class="form-control" id="AddressTb" runat="server" required></textarea>
            </div>
            <div class="mb-3">
                <label id="ErrorMsg" runat="server" class="text-danger"></label>
            </div>
            <div class="mb-3 d-grid">
                <asp:Button Text="Register" Class="btn btn-success btn-block" runat="server" ID="RegisterBtn" OnClick="RegisterBtn_Click" />
            </div>
            <div class="text-center">
                <a href="Login.aspx">Already have an account? Login</a>
            </div>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
</script>

