# 🏥 Nursing Home Management System

A full-stack Windows desktop application built in **C# (Windows Forms)** with a **SQL Server** database backend, developed as a second-year group project at North-West University (CMPG223).

---

## 📋 Project Overview

This system was built to manage the day-to-day operations of a nursing home facility. It supports three distinct user roles — **Admin**, **Nurse**, and **Resident** — each with appropriate access permissions enforced at both the UI and logic level.

---

## ✨ Features

### 🔐 Authentication & Role-Based Access Control
- Single login form handles all three user types (Admin, Nurse, Resident)
- Role is determined server-side in a single SQL query with precedence ordering
- Dashboard buttons are dynamically enabled/disabled based on the logged-in role
- Double-click prevention on login button to avoid duplicate submissions

### 👩‍⚕️ Nurse Management (CRUD)
- Add, update, delete, and search nurses
- Live search with debounce timer (300ms) across ID number, name, surname, and username
- South African ID number validation including **Luhn algorithm check**
- Username validation with regex (letters, digits, underscores, dots, hyphens)
- Password validation requiring minimum 6 characters with letters and digits
- Duplicate ID number and username checks before insert/update
- Inline field-level error messages using `ErrorProvider`
- Placeholder text on all input fields via Win32 `SendMessage` API

### 🛏️ Resident Management
- Full CRUD operations for resident records
- Room assignment and ID number tracking

### 💊 Medication Management
- Medication stock tracking
- Medication ordering system
- Assign medications to specific residents

### 🚨 Emergency Alerts
- Emergency alert system accessible to both Nurses and Residents

### 📊 Reports
- Reporting module accessible to Admin and Nurse roles

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Language | C# (.NET Framework) |
| UI Framework | Windows Forms (WinForms) |
| Database | Microsoft SQL Server Express |
| ORM / Data Access | ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter) |
| IDE | Microsoft Visual Studio |
| Version Control | Git / GitHub |

---

## 🗄️ Database Schema

The database (`NursingHomeDB`) includes the following tables:

- `ADMIN` — System administrators
- `NURSE` — Nursing staff with login credentials
- `RESIDENT` — Nursing home residents with room assignments
- `MED_STOCK` — Medication inventory
- `ORDER_MED` — Medication orders (batch tracking)
- `MED_TO_RES` — Medication-to-resident assignment bridge table
- `RES_TO_NURSE` — Resident-to-nurse assignment bridge table
- `EMEGENCY_ALERT` — Emergency alerts linked to residents and nurses

Referential integrity is enforced with foreign keys, cascade deletes, and unique constraints.

---

## ⚙️ Setup & Installation

### Prerequisites
- Windows OS
- Microsoft Visual Studio (2019 or later)
- SQL Server Express (or full SQL Server)

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/Hanno-du-Toit/NursingHome-Management-System.git
   ```

2. **Set up the database**
   - Open SQL Server Management Studio (SSMS)
   - Connect to your SQL Server instance
   - Open and run `NursingHomeDB_CreateTables.sql`
   - This will create the database, all tables, constraints, and sample data

3. **Update the connection string**
   - In `feature-Login-Form1.cs`, update the server name to match your SQL Server instance:
   ```csharp
   private readonly string connectionString =
       @"Server=YOUR_SERVER_NAME\SQLEXPRESS;Database=NursingHomeDB;Trusted_Connection=True;TrustServerCertificate=True;";
   ```

4. **Build and run**
   - Open the solution in Visual Studio
   - Build the solution (`Ctrl+Shift+B`)
   - Run (`F5`)

---

## 🔑 Sample Login Credentials

> ⚠️ These are sample credentials for demonstration purposes only. Passwords are stored in plain text as this is an academic project — production systems would use hashing.

| Role | Username | Password |
|---|---|---|
| Resident | `aleroux` | `pass987` |
| Resident | `pgovender` | `pass741` |
| Resident | `kmolefe` | `pass852` |

---

## 👥 Development Team — Group 11

| Name |
|---|
| Hanno du Toit |
| Armand Lourens |
| Schalk Pretorius |
| Jayden Mollet |
| Jordan Le Roux |
| Neil Pieters |

---

## 📚 Module

**CMPG223 — Systems Analysis & Design**  
North-West University, 2025
