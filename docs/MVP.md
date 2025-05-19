### 📌 Minimum Viable Product (MVP)

**Goal:** Launch a functional platform that lets Customers find rooms, request & pay for bookings, and lets landlords manage their listings—all within 2 months.

---

#### 1. Core Roles & Auth

|Role|Capabilities in MVP|
|---|---|
|**Guest**|Browse & filter listings (read-only)|
|**Customer**|Sign up / sign in (email + Google) → request bookings → pay → leave review after stay|
|**Landlord**|Sign up / sign in → create / edit / delete own listings (only if no accepted/pending bookings) → view & accept/reject booking requests|
|**Admin**|Minimal panel: list / deactivate users & listings|

**Auth stack**

- **ASP.NET Identity** with JWT + Google OAuth (built-in social-login middleware).
    
- Next.js uses **NextAuth.js** (Credentials + Google) to obtain JWT and store it securely.
    

---

#### 2. Key User Flows

1. **Browse & Filter** (public)
    
    - Filters: price range, location keyword, room type.
        
2. **Sign Up / Log In**
    
    - Role selected at sign-up (Customer / landlord).
        
    - Email verification (optional but recommended).
        
3. **Booking Lifecycle**
    
    1. Customer picks dates → sends booking request.
        
    2. Landlord sees request in dashboard → Accept / Reject.
        
    3. If **Accepted** → Customer is redirected to Stripe Checkout.
        
    4. Payment success marks booking **Confirmed** (stored Stripe session ID).
        
4. **Review**
    
    - Customer can leave rating + comment **after booking end date**.
        
5. **Email Notifications** (SendGrid):
    
    - Request received, request accepted/rejected, payment success.
        

---

#### 3. Data Model (simplified)

see [DB.md](DB.md) for full schema.

---

#### 4. Tech Stack Decisions

|Layer|Choice|Notes|
|---|---|---|
|**Backend API**|ASP.NET 8 Web API|Clean Architecture folder structure|
|**ORM**|EF Core + Code-First Migrations||
|**Payments**|Stripe (Test mode for MVP)||
|**Frontend**|Next.js 14 (App Router) + Tailwind CSS||
|**State / Data Fetch**|React Query or fetch wrapper||
|**CI/CD**|GitHub Actions → Railway (API) / Vercel (Next.js)||

---

#### 5. What’s **Out-of-Scope** for MVP

- Real-time chat (`SignalR`)
    
- Calendar availability UI
    
- Advanced admin analytics
    
- Push / in-app notifications (email only for now)

---