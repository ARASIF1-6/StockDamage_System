# 📦 Stock Damage System

A simple and intuitive **Stock Damage Management System** built using C#, .NET, HTML, Bootstrap, JavaScript and JQuery.  
This application helps track inventory damage, manage stock loss.

---

## 🧠 Project Overview

**Stock Damage System** is a web application that allows users to:

✔ Register and track damaged stock items  
✔ Maintain inventory records  
✔ Analyze stock damage history  
✔ Reduce manual loss tracking errors

---

## 🛠️ Technology Stack

| Technology | Purpose |
|------------|---------|
| 💻 C#
| 🪟 .NET Framework
| 📦 MS SQL Server (.bacpac) | Database storage |
| 🧰 Visual Studio | IDE for development |
| 📄 HTML / JS / Bootstrap

---

## 🗂️ Repository Structure

StockDamage_System/
├── .vs/ # Visual Studio config
├── StockDamage/ # Main application project
├── packages/ # NuGet packages & dependencies
├── StockDamage.sln # Visual Studio Solution File
└── StockDamageDB.bacpac # Database backup package


---

## 🚀 Installation & Setup

> ⚠️ Make sure you have **Visual Studio** and **SQL Server** installed on your system.

1. **Clone this repository**

```bash
git clone https://github.com/ARASIF1-6/StockDamage_System.git
cd StockDamage_System
```

2. **Open the solution**

Double-click StockDamage.sln to open it in Visual Studio.

3. **Restore packages**

Inside Visual Studio, go to:
Tools → NuGet Package Manager → Restore Packages

4. **Setup the database**

Import StockDamageDB.bacpac into SQL Server using:

SQL Server Management Studio (SSMS):
Right click → Import Data-Tier Application → Select bacpac

5. **Build & Run**

Build the solution using:
Build → Rebuild Solution

Run the app
Debug → Start Debugging (F5)

