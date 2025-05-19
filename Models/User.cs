namespace RentalAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; } // "Student", "Landlord", "Admin"
        public required string PhoneNumber { get; set; }
        public string? FacebookUrl { get; set; }

        public ICollection<Listing>? Listings { get; set; }  // If landlord
        public ICollection<Booking>? Bookings { get; set; }  // If student
    }
}