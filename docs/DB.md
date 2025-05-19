

# 🧾 Database Schema Documentation

### 📘 Tech Stack

- **Backend**: ASP.NET Core + Entity Framework Core
    
- **Approach**: Code-First with Migrations
    

---

## 🏗️ Entities Overview

| Entity    | Purpose                                |     |
| --------- | -------------------------------------- | --- |
| `User`    | Represents both Customers and Landlords |     |
| `Listing` | Property posted by a Landlord          |     |
| `Booking` | Booking requests from Customers         |     |
| `Review`  | Feedback from Customers after stay      |     |

---

## 🧑‍💻 `User`

Represents all users in the system, including Customers and landlords.  
User role determines capabilities.

```csharp
public class User {
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } // "Customer", "Landlord", "Admin"
    public string PhoneNumber { get; set; }
    public string? FacebookUrl { get; set; }

    public ICollection<Listing>? Listings { get; set; }  // If landlord
    public ICollection<Booking>? Bookings { get; set; }  // If Customer
}
```

### Notes:

- ✅ Role-based logic is handled in backend.
    
- 🛡️ Ensure unique email constraint.
    

---

## 🏠 `Listing`

Represents a room/apartment available for rent.

```csharp
public class Listing {
    public Guid Id { get; set; }
    public Guid LandlordId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public decimal PricePerMonth { get; set; }
    public DateTime? NextAvailableDate { get; set; }
    public bool IsActive { get; set; } = true;

    public User Landlord { get; set; }
    public ICollection<Booking> Bookings { get; set; }
}
```

### Notes:

- `NextAvailableDate` is **manually set** by landlord. System doesn’t auto-calculate this.
    
- `IsActive` lets landlord temporarily deactivate listing.
    

---

## 📅 `Booking`

Represents a booking request from a Customer to a listing.

```csharp
public class Booking {
    public Guid Id { get; set; }
    public Guid ListingId { get; set; }
    public Guid CustomerId { get; set; }

    public DateTime MoveInDate { get; set; }
    public DateTime? MoveOutDate { get; set; }

    public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected, Paid
    public string? StripeSessionId { get; set; }

    public Listing Listing { get; set; }
    public User Customer { get; set; }
}
```

### Booking Logic:

- Before accepting, check for **overlapping dates** with `Status = Accepted or Paid`.
    
- A booking becomes **confirmed** only after payment.
    
- A landlord **can’t delete a listing** with any “Accepted” or “Paid” booking.
    

---

## ⭐ `Review`

Written by Customers **only after their booking ends**.

```csharp
public class Review {
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public int Rating { get; set; } // 1 to 5
    public string Comment { get; set; }

    public Booking Booking { get; set; }
}
```

### Notes:

- Enforce that `Booking.CustomerId == User.Id` and that current date is after `MoveOutDate`.
    

---

## 🔄 Relationships Summary

|Relation|Type|
|---|---|
|User ↔ Listings|1-to-many|
|User ↔ Bookings|1-to-many|
|Listing ↔ Bookings|1-to-many|
|Booking ↔ Review|1-to-1|

---

## ⚠️ Special Considerations

### 📍 Overlapping Bookings

When Customer requests a booking:

```sql
SELECT *
FROM Bookings
WHERE ListingId = @ListingId
  AND Status IN ('Accepted', 'Paid')
  AND MoveInDate <= @RequestedMoveOutDate
  AND (MoveOutDate IS NULL OR MoveOutDate >= @RequestedMoveInDate)
```

Reject if any match found.

---

### 🗓️ Handling Availability

- If `NextAvailableDate` is set:
    
    - Only allow booking **after** that date
        
- If **not set**, fallback to latest end date from active bookings
    

---

### 🛑 Prevent Landlord Deletion of Listing

```csharp
if (listing.Bookings.Any(b => b.Status == "Accepted" || b.Status == "Paid")) {
    throw new InvalidOperationException("Can't delete a listing with active bookings.");
}
```

---

### 📩 Notifications

- Use SendGrid or similar to send email on:
    
    - Booking accepted / rejected
        
    - Payment successful
        

---

## 🔐 Auth & Roles

- Use ASP.NET Identity or JWT Auth
    
- Add a `Role` claim for route protection
    
    - `[Authorize(Roles = "Landlord")]` etc.
        

---
