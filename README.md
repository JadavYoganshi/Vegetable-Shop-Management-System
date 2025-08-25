# Vegetable Shop Management System

## 📌 Project Overview
The **Vegetable Shop Management System** is a web-based application designed to simplify and digitalize the management of a vegetable store.  
It allows the **Admin** to manage Items and show Admin and Dashboard.

This project was developed using **ASP.NET Framework (C#)** with **SQL Server** as the database.

---

## 🚀 Features
### Admin
- 🔑 Login & Logout system
- 🏠 Dashboard with quick overview
- 📦 Manage Items (Add, Update, Delete, View)
- 👥 Show Customers

### Customer
- 📝 Registration & Login
- 🛒 View available vegetables/items
- 📥 Place orders
- 🧾 View and Print Bills

---

## 🛠️ Technologies Used
- **Frontend:** HTML, CSS, Bootstrap
- **Backend:** ASP.NET (C#)
- **Database:** SQL Server
- **IDE/Tools:** Visual Studio 2022, SQL Server Management Studio

---

## 📦 Required Packages
Make sure you have installed the following NuGet packages in your project:

- `Microsoft.EntityFrameworkCore`  
- `Microsoft.EntityFrameworkCore.SqlServer`  
- `Microsoft.EntityFrameworkCore.Tools`
- `iTextSharp`  (for print bill in PDF Format)

👉 To install via NuGet Package Manager Console:
```bash
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package iTextSharp
```

---

## ⚙️ Setup Instructions
### 1️⃣ Clone or Extract Project
```bash
git clone <repository-url>
```
or extract the provided ZIP file.

### 2️⃣ Open in Visual Studio
- Open `VSMSProject.sln` in **Visual Studio 2022**.

### 3️⃣ Setup Database
1. Open **SQL Server Management Studio (SSMS)**.
2. Create a new database, e.g., `VegetableShopDB`:
   ```sql
   CREATE DATABASE VegetableShopDB;
   ```
3. Update **Web.config** connection string:
   ```xml
   <connectionStrings>
     <add name="DefaultConnection" 
          connectionString="Data Source=.;Initial Catalog=VegetableShopDB;Integrated Security=True" 
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

### 4️⃣ Apply Database Migration
If the project uses **Entity Framework Code-First**:
```bash
Enable-Migrations
Add-Migration InitialCreate
Update-Database
```

If the project uses **Database-First**:
- Import your database `.mdf` file or connect directly through SSMS.

### 5️⃣ Run the Application
- Press **F5** or click **Start** in Visual Studio.
- The project will open in your default browser.

---

## 📂 Project Structure
```
Vegetable-Shop-Management-System/
│── VSMSProject.sln               # Solution file
│── VSMSProject/                  # Main ASP.NET Web Forms Project
│   ├── Account/                  # Login & Registration pages
│   ├── Admin/                    # Admin dashboard, item, and customer pages
│   ├── Customer/                 # Customer-related pages
│   ├── App_Code/                 # Business logic classes (if used)
│   ├── App_Data/                 # Database/Error logs
│   ├── Asset/                    # CSS, JS, Images
│   ├── Images/                   # Static images
│   ├── Global.asax               # Application-level configuration
│   ├── Web.config                # Configuration file
│
│── Images/                       # Screenshots
│── .gitignore
│── .gitattributes

```

---

## 📸 Screenshots
Some key pages of the project:

- 🔑 Login & Register  
- 🏠 Dashboard  
- 📦 Item Management  
- 👥 Customer Management  
- 🧾 Bill Printing  

(See `Images/` folder for screenshots)

---

## 👩‍💻 Developer
- **Name:** Yoganshi Jadav [GitHub](https://github.com/JadavYoganshi) 
- **Role:** Developer  
- **Tech Stack:** ASP.NET Framework, SQL Server, C#, HTML, CSS, Bootstrap  

---

## 📜 License
This project is for educational purposes only.  
You are free to modify and use it for learning.
