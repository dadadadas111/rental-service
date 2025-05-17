namespace RentalAPI.Models.DTOs
{
    public class ListingCreateDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public decimal PricePerMonth { get; set; }
        public DateTime? NextAvailableDate { get; set; }
    }

    public class ListingUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public decimal? PricePerMonth { get; set; }
        public DateTime? NextAvailableDate { get; set; }
        public bool? IsActive { get; set; }
    }


    public class ListingDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public decimal PricePerMonth { get; set; }
        public DateTime? NextAvailableDate { get; set; }
        public bool IsActive { get; set; }
        public string LandlordEmail { get; set; } = null!;
    }
}
