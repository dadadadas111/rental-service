namespace RentalAPI.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public int Rating { get; set; } // 1 to 5
        public required string Comment { get; set; }

        public required Booking Booking { get; set; }
    }
}