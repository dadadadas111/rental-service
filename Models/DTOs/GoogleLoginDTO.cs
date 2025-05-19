namespace RentalAPI.Models.DTOs
{
    public class GoogleLoginDTO
    {
        public required string IdToken { get; set; }
        public string? Role { get; set; } // Optional
    }

}