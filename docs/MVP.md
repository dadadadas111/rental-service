### 📌 Minimum Viable Product (MVP)

**Goal:** Launch a functional web platform where customers can browse listings, book rooms, and landlords can manage properties—all in **1 ASP.NET MVC app**, within 2 months.

---

#### 1. Core Roles & Auth

| Role         | Capabilities in MVP                                                                 |
| ------------ | ----------------------------------------------------------------------------------- |
| **Guest**    | Browse listings, search/filter (read-only access)                                   |
| **Customer** | Register/Login (Email or Google) → Request bookings → Pay → Leave review post-stay  |
| **Landlord** | Register/Login → Create/Edit/Delete own listings → Manage incoming booking requests |
| **Admin**    | Admin panel to manage (list, deactivate) users & listings                           |

**Auth Stack**

* **ASP.NET Identity** for built-in login/registration.
* **Google OAuth** via ASP.NET external login providers.
* **Role-based authorization** with `[Authorize(Roles = "X")]` attributes.

---

#### 2. Key User Flows

1. **Browse & Filter Listings** (Public)

   * Search by location/keyword.
   * Filter by price range, availability, room type.

2. **User Registration/Login**

   * Register with email or Google account.
   * Select role on registration (Customer / Landlord).
   * Email confirmation optional.

3. **Booking Lifecycle**

   1. Customer selects listing + dates → submits booking request.
   2. Landlord views requests → Accept / Reject.
   3. If **Accepted**, customer proceeds to **Stripe Checkout**.
   4. On payment success, booking becomes **Confirmed**.

4. **Review Flow**

   * After booking end date, customer can submit a rating + comment.
   * Display average ratings on listings.

5. **Notifications**

   * Use **SendGrid** to notify:

     * Booking request received
     * Request accepted/rejected
     * Payment confirmed

---

#### 3. Data Model (Simplified)

See [`DB.md`](DB.md) for full schema with entity relations.

Core Entities:

* `User` (role: Customer / Landlord / Admin)
* `Listing`
* `Booking`
* `Review`

---

#### 4. Tech Stack Decisions

| Layer             | Choice                     | Notes                                  |
| ----------------- | -------------------------- | -------------------------------------- |
| **Web Framework** | ASP.NET Core 8 MVC (Razor) | Unified project (no separate frontend) |
| **ORM**           | EF Core + Code-First       | With migrations & seed data            |
| **Payments**      | Stripe (Test Mode)         | Triggered after booking accepted       |
| **Email**         | SendGrid                   | SMTP or API-based                      |
| **Hosting**       | Azure / Render (free tier) | Will decide before deployment          |
| **CI/CD**         | GitHub Actions             | Optional in MVP                        |

---

#### 5. What’s **Out-of-Scope** for MVP

* Real-time features (e.g., SignalR chat)
* Drag-and-drop calendar UI for availability
* Admin analytics dashboards
* Push or in-app notifications (email only)

