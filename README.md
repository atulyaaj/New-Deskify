<div align="center">

# 🧭 Deskify – Smart Desk & Meeting Room Booking System  
_Flexible and efficient workplace management for hybrid offices_

[![GitHub Stars](https://img.shields.io/github/stars/atulyaaj/deskify?style=social)](https://github.com/atulyaaj/deskify/stargazers)
[![Last Commit](https://img.shields.io/github/last-commit/atulyaaj/deskify)](https://github.com/atulyaaj/deskify/commits)
![Built with](https://img.shields.io/badge/Built%20with-ASP.NET%20Core%20%7C%20C%23%20%7C%20SQL%20%7C%20HTML%2FCSS%2FJS-blue)

</div>

---

## 📌 Overview

**Deskify** is a seat and meeting room booking management platform designed to simplify and optimize desk usage in hybrid work environments. With features like real-time seat maps, QR code passes, admin controls, and reporting dashboards, Deskify helps organizations manage their workspaces efficiently and improve employee experience.

---

## ✨ Features

- 🪑 Book & Cancel Desk/Meeting Room Reservations
- 🗺️ Interactive Seat & Meeting Room Maps
- 🔐 Secure Login/Register & Role-Based Access
- 📅 View Pass with QR Code for Entry
- 📢 Notification System (Booking, Cancellation, Pending)
- 📊 Admin Dashboards & Booking Reports
- 🔎 Search, Sort, and Filter Records
- 🧩 Profile View & Edit Options

---

## 🛠 Tech Stack

| Layer         | Technologies Used                                  |
|---------------|-----------------------------------------------------|
| Frontend      | HTML, CSS, JavaScript, Bootstrap                    |
| Backend       | C#, ASP.NET Core MVC                                |
| Database      | Microsoft SQL Server                                |
| Tools         | Visual Studio, SSMS, IIS                            |
| Other         | QR Code Libraries                                   |

---

## 🚀 How to Run

To get Deskify up and running locally, follow these steps:

### 🧰 1. Install Required Tools

- 💻 [Microsoft Visual Studio](https://visualstudio.microsoft.com/) (2019 or above)
- 🗄️ [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- 📊 [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

&nbsp;  

### ⚙️ 2. Configure Connection String

Open the `appsettings.json` file in the root directory and update the **SQL connection string** with your local database server name:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DeskifyDB;Trusted_Connection=True;"
}

```
&nbsp;  

### 🛠️ 3. Setup the Database

Open **Package Manager Console** in Visual Studio:

```
Tools > NuGet Package Manager > Package Manager Console
```

Then run the following command to apply migrations and initialize your database:

```powershell
Update-Database
```

This will create the necessary tables and schema in your SQL Server database using Entity Framework Core.

&nbsp;  

### ▶️ 4. Launch the Application

- In **Solution Explorer**, right-click your MVC project `DeskUI`
- Select **Set as Startup Project**
- Click the green **IIS Express** play button or use the shortcut `Ctrl + F5`

The application will launch at:

```
https://localhost:[PORT]/
```

You'll now see the Deskify homepage in your browser.

---

## 🖼️ Screenshots

<div align="center">

<img src="https://github.com/user-attachments/assets/737a5347-9822-4484-b4b7-88d62cf72966" width="500" alt="Login Page"><br>
🔐 *Login Page*

<img src="https://github.com/user-attachments/assets/26079650-fa49-4478-9f8e-dcca57c9e869" width="500" alt="User Dashboard"><br>
📋 *User Dashboard*

<img src="https://github.com/user-attachments/assets//b88fd9ac-7247-4ee1-a72d-4636efbd136b" width="500" alt="Room Map"><br>
🗺️ *Live Meeting Room Map View*

<img src="https://github.com/user-attachments/assets/ef5ac48c-6957-4b0e-959c-a0ba0dca9cdb" width="500" alt="Notification Page"><br>
🔔 *Notification Center – Booking & Cancellation Alerts*

<img src="https://github.com/user-attachments/assets/917916ae-4b47-4544-97e5-52831a89732d" width="500" alt="View Pass"><br>
📋 *View Pass - Seat Booking Details*

<img src="https://github.com/user-attachments/assets/bd766906-6960-4d62-9e74-3d7cb87f3aff" width="500" alt="QR Pass"><br>
🔳 *QR Pass for Seat Entry*

<img src="https://github.com/user-attachments/assets/12a8e76b-41bd-4af0-94d9-2c866c778946" width="500" alt="Admin Dashboard"><br>
🛠️ *Admin Panel - Manage Bookings & Floors*

<img src="https://github.com/user-attachments/assets/ae81adf8-764c-470a-949e-cb6e1c90b6d5" width="500" alt="Scan QR"><br>
📷 *Scan QR – Admin Seat Verification*

<img src="https://github.com/user-attachments/assets/0e8b846c-4d78-4072-bdac-9a981f574a2e" width="500" alt="Booking Lookup"><br>
*🔍 Booking Lookup*

</div>

---

## 🧪 Testing

Includes tests for:
- ✅ User registration/login/logout
- 📅 Seat & meeting room booking workflows
- ❌ Cancellation handling
- 🔐 QR pass generation and scan flow
- 📈 Admin analytics/reporting panel

---

<div align="center">

🚀 Developed by the **Deskify Team**  <br>
🎨 UI designed & developed by **Atulya Jaiswal**  

</div>
