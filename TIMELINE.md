
# 🗓️ 3-Week Revised Timeline
---

## ✅ Week 1: Core Setup & Backend MVP

### 🎯 Goals:

- Finalize scope + data models
    
- Scaffold backend & basic APIs
    
- Setup frontend (Next.js)
    

### 🔧 Tasks:

#### Backend (ASP.NET):

- Scaffold project (`dotnet new webapi`)
    
- Add EF Core + initial models:
    
    - `User` (with roles), `Listing`, `Booking`
        
- Implement:
    
    - Auth (JWT or ASP.NET Identity)
        
    - Listing CRUD
        
    - Booking create + availability check
        
- Seed dummy data (for listings & users)
    
- Add Swagger for testing
    

#### Frontend (Next.js):

- Scaffold project
    
- Setup auth forms (register/login)
    
- Pages:
    
    - Home (listing browser)
        
    - Listing detail
        
    - Booking form
        

### ⏵ Output:

[[Week 1 Output]]

---

## ✅ Week 2: Core User Flows & Dashboards

### 🎯 Goals:

- Students: Browse → Book
    
- Landlords: CRUD listings
    
- Dashboards for both roles
    

### 🔧 Tasks:

#### Backend:

- Finish:
    
    - Booking logic (prevent overlaps)
        
    - Accept/reject booking endpoints
        
    - Email sending (SendGrid or SMTP)
        

#### Frontend:

- Student dashboard:
    
    - View bookings
        
- Landlord dashboard:
    
    - View, edit, delete listings
        
    - Accept/reject bookings
        
- Add filters to homepage
    

---

## ✅ Week 3: Polish, Test & Deploy

### 🎯 Goals:

- Clean UI
    
- Test flows
    
- Deploy backend & frontend
    
- Prepare presentation/demo
    

### 🔧 Tasks:

#### Backend:

- Write basic tests (optional)
    
- Add booking/review constraints
    
- Deploy to Railway/Azure
    

#### Frontend:

- Improve UI with Tailwind
    
- Add toast notifications (e.g., success/error)
    
- Deploy to Vercel/Netlify
    

#### Docs:

- `README` with setup + feature list
    
- Screenshots / demo video (if required)
    

---

## 🔁 Continuous Tasks (All Weeks)

- Use Git commits with messages
    
- Prioritize working app over extra features
    
- Stick to MVP scope: listings, booking, auth
    

---