namespace RentalAPI.Models
{
    public class Listing
    {
        public Guid Id { get; set; }
        public Guid LandlordId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Location { get; set; }
        public decimal PricePerMonth { get; set; }
        public DateTime? NextAvailableDate { get; set; }
        public bool IsActive { get; set; } = true;

        public User? Landlord { get; set; }
        public required ICollection<Booking> Bookings { get; set; }
    }
}