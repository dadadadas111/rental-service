using Microsoft.EntityFrameworkCore;
using RentalAPI.Data;
using RentalAPI.Models;
using RentalAPI.Models.DTOs;

namespace RentalAPI.Services
{
    public class ListingService
    {
        private readonly AppDbContext _context;

        public ListingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ListingDTO>> GetAll()
        {
            return await _context.Listings
                .Include(l => l.Landlord)
                .Where(l => l.IsActive)
                .Select(l => new ListingDTO
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    Location = l.Location,
                    PricePerMonth = l.PricePerMonth,
                    NextAvailableDate = l.NextAvailableDate,
                    IsActive = l.IsActive,
                    LandlordEmail = l.Landlord.Email
                })
                .ToListAsync();
        }

        public async Task<List<ListingDTO>> GetFiltered(string? location, decimal? minPrice, decimal? maxPrice, DateTime? availableFrom)
        {
            var query = _context.Listings
                .Include(l => l.Landlord)
                .Where(l => l.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(l => l.Location.Contains(location));

            if (minPrice.HasValue)
                query = query.Where(l => l.PricePerMonth >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(l => l.PricePerMonth <= maxPrice.Value);

            if (availableFrom.HasValue)
                query = query.Where(l => l.NextAvailableDate == null || l.NextAvailableDate <= availableFrom.Value);

            return await query.Select(l => new ListingDTO
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                Location = l.Location,
                PricePerMonth = l.PricePerMonth,
                NextAvailableDate = l.NextAvailableDate,
                IsActive = l.IsActive,
                LandlordEmail = l.Landlord.Email
            }).ToListAsync();
        }


        public async Task<Listing?> GetById(Guid id)
        {
            return await _context.Listings
                .Include(l => l.Landlord)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Listing> Create(ListingCreateDTO dto, string landlordId)
        {
            var listing = new Listing
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                PricePerMonth = dto.PricePerMonth,
                NextAvailableDate = dto.NextAvailableDate,
                LandlordId = Guid.Parse(landlordId),
                IsActive = true,
                Bookings = new List<Booking>()
            };

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();
            return listing;
        }

        public async Task<(bool Success, string? Error, ListingDTO? Data)> UpdateAsync(Guid listingId, ListingUpdateDTO dto, Guid userId)
        {
            var listing = await _context.Listings.Include(l => l.Landlord).FirstOrDefaultAsync(l => l.Id == listingId);
            if (listing == null) return (false, "Listing not found", null);
            if (listing.LandlordId != userId) return (false, "Not your listing", null);

            if (dto.Title != null) listing.Title = dto.Title;
            if (dto.Description != null) listing.Description = dto.Description;
            if (dto.Location != null) listing.Location = dto.Location;
            if (dto.PricePerMonth.HasValue) listing.PricePerMonth = dto.PricePerMonth.Value;
            if (dto.NextAvailableDate.HasValue) listing.NextAvailableDate = dto.NextAvailableDate;
            if (dto.IsActive.HasValue) listing.IsActive = dto.IsActive.Value;

            await _context.SaveChangesAsync();

            var updated = new ListingDTO
            {
                Id = listing.Id,
                Title = listing.Title,
                Description = listing.Description,
                Location = listing.Location,
                PricePerMonth = listing.PricePerMonth,
                NextAvailableDate = listing.NextAvailableDate,
                IsActive = listing.IsActive,
                LandlordEmail = listing.Landlord.Email
            };

            return (true, null, updated);
        }



        public async Task<bool> Delete(Guid id, string landlordId)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null || listing.LandlordId.ToString() != landlordId)
                return false;

            listing.IsActive = false; // soft delete
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
