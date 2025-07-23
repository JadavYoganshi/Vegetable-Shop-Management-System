<%@ Page Title="" Language="C#" MasterPageFile="~/View/Customer/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="CustomerHome.aspx.cs" Inherits="VSMSProject.View.Customer.CustomerHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: 'Poppins', sans-serif;
            background-color: #f3f3f3; /* Neutral background */
        }

        .background-container {
            position: relative;
            width: 100%;
            height: 100vh; /* Full-screen height */
            display: flex;
            flex-direction: column; /* Arrange sections vertically */
            justify-content: center; /* Center content vertically */
            align-items: center; /* Center content horizontally */
            overflow: hidden;
        }

        /* Background image styling with blur effect */
        .background-container::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-image: url('../../Asset/Images/background.png');
            background-size: cover;
            background-position: center;
            filter: blur(6px); /* Add blur effect */
            opacity: 0.6; /* Add opacity for a faded look */
            z-index: -1; /* Ensure background stays behind content */
        }

        /* Text container */
        .text-container {
            text-align: center;
            color: black;
            background-color: rgba(255, 255, 255, 0.9); /* Semi-transparent white background */
            padding: 40px 60px; /* Padding around the text */
            border-radius: 15px; /* Rounded corners */
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.3); /* Add a subtle shadow for depth */
        }

        .text-container h1 {
            font-size: 4.5rem; /* Larger headline font */
            font-weight: bold;
            margin: 0;
            color: #4CAF50; /* Vibrant green for main text */
            animation: fadeIn 2s ease-in-out forwards;
        }

        .text-container p {
            font-size: 1.8rem; /* Subheading font size */
            font-weight: 500;
            margin-top: 10px;
            color: #555; /* Slightly darker for contrast */
            animation: fadeIn 3s ease-in-out forwards;
        }

        /* Add button below text */
        .text-container .explore-btn {
            margin-top: 20px;
            display: inline-block;
            font-size: 1.5rem;
            font-weight: bold;
            color: white;
            text-decoration: none;
            background-color: #4CAF50;
            padding: 15px 30px;
            border-radius: 25px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
            transition: all 0.3s ease-in-out;
        }

        .text-container .explore-btn:hover {
            background-color: #388E3C; /* Darker green on hover */
            transform: translateY(-5px); /* Subtle lift effect */
        }

        /* Animations */
        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* Responsive adjustments */
        @media (max-width: 768px) {
            .text-container h1 {
                font-size: 3rem; /* Adjust headline font size for smaller screens */
            }

            .text-container p {
                font-size: 1.4rem; /* Adjust paragraph font size for smaller screens */
            }

            .text-container .explore-btn {
                font-size: 1.2rem; /* Adjust button font size */
            }
        }
    </style>
    <!-- Google Font -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;600;700&display=swap" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Mybody" runat="server">
    <div class="background-container">
        <!-- Text Content -->
        <div class="text-container">
            <h1>Welcome</h1>
            <p>To the Fresh Vegetable Shop!</p>
            
        </div>
    </div>
</asp:Content>

