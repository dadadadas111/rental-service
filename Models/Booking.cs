namespace rental_service.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid ListingId { get; set; }
        public Guid StudentId { get; set; }

        public DateTime MoveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected, Paid
        public string? StripeSessionId { get; set; }

        public required Listing Listing { get; set; }
        public required User Student { get; set; }
    }
}