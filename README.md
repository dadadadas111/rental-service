# 🏡 Accommodation Rental Web App

A full-stack web application for customers to search and book rental properties from landlords. Built with **ASP.NET Core MVC**, this project provides a simple and secure solution for managing rental bookings.

---

## ✨ Features

### For Customers:

* Browse and filter listings without login
* Secure registration & login system
* Request bookings with preferred move-in dates
* View personal booking history and statuses
* Leave reviews after completed stays

### For Landlords:

* Register and list rental properties
* Set availability and pricing
* Accept or reject booking requests
* Manage listings and bookings via dashboard

### Authentication:

* Role-based access: `Customer`, `Landlord`, `Admin`
* Protected routes and views

### Notifications:

* Email alerts on booking updates (via SendGrid)

---

## 🧱 Tech Stack

| Layer              | Technology              |
| ------------------ | ----------------------- |
| Backend & Frontend | ASP.NET Core MVC        |
| ORM                | Entity Framework Core   |
| Auth               | ASP.NET Identity        |
| Database           | SQL Server / PostgreSQL |
| Email              | SendGrid or SMTP        |
| Hosting            | Azure, Railway          |

---

## 📐 Core Entities

* `User` – Shared model for Customers and Landlords
* `Listing` – Rental property details
* `Booking` – Booking requests and history
* `Review` – Feedback from customers

---

## 📦 MVP Scope

* Listings browsing and filtering
* Booking creation with date validation
* Dashboards for Customers and Landlords
* Role-based authentication and authorization

---

## ✅ Project Status

> In development — 3-week rapid MVP build cycle

---

## 📄 License

For academic/demo use only.

---

## 🙋 Author

**dadadadas111**
Student | Developer | Chess & Tetris Enthusiast

---
