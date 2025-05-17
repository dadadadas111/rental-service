# 🏡 Student Accommodation Rental Web App

A full-stack web application for students to search and book rental properties from landlords. Built with **ASP.NET Core** for the backend and **Next.js** for the frontend, this project provides a clean, secure, and scalable solution for managing student housing.

---

## ✨ Features

### 🧑 For Students:
- Browse and filter listings without login
- Secure registration & login system
- Request bookings with specified move-in dates
- View personal booking history and statuses
- Leave reviews after completed stays

### 🧑‍💼 For Landlords:
- Register and list rental properties
- Set availability and pricing per listing
- Accept or reject student bookings
- View incoming requests in a personal dashboard

### 🔐 Authentication:
- Role-based access: `Student`, `Landlord`, `Admin`
- Protected endpoints and frontend routes

### 📬 Notifications:
- Email notifications on booking updates (via SendGrid)

### 💸 Payments (Optional):
- Stripe integration for collecting booking payments

---

## 🧱 Tech Stack

| Layer     | Technology                 |
|-----------|----------------------------|
| Backend   | ASP.NET Core Web API       |
| ORM       | Entity Framework Core      |
| Frontend  | Next.js + Tailwind CSS     |
| Auth      | JWT or ASP.NET Identity    |
| Database  | PostgreSQL / SQL Server    |
| Email     | SendGrid or SMTP           |
| Payments  | Stripe (Optional)          |
| Deployment| Railway, Azure, Vercel     |

---

## 📐 Core Entities

- `User` – Shared model for Students and Landlords
- `Listing` – Property posted by landlords
- `Booking` – Booking requests and history
- `Review` – Feedback from students after stay

---

## 📦 MVP Scope

This version prioritizes:
- Listings
- Bookings with date overlap checks
- Basic dashboards for both roles
- Authentication with role access control

Deferred:
- Messaging/chat system
- Calendar UI for booking
- Admin dashboard

---

## ✅ Project Status

> ⏳ In development – 3-week rapid build cycle  
> Focus: Clean architecture, functional MVP, fast deployment

---

## 📄 License

This project is built for academic purposes and is not licensed for production use.

---

## 🙋 Author

**dadadadas111**  
Student | Developer | Chess & Tetris Enthusiast  
