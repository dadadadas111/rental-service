using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalAPI.Models.DTOs;
using RentalAPI.Services;
using System.Security.Claims;

namespace RentalAPI.Controllers
{
    [ApiController]
    [Route("api/listings")]
    public class ListingController : ControllerBase
    {
        private readonly ListingService _service;

        public ListingController(ListingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] string? location,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] DateTime? availableFrom)
        {
            var listings = await _service.GetFiltered(location, minPrice, maxPrice, availableFrom);
            return Ok(listings);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var listing = await _service.GetById(id);
            if (listing == null) return NotFound();
            return Ok(listing);
        }

        [Authorize(Roles = "Landlord")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ListingCreateDTO dto)
        {
            var landlordId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listing = await _service.Create(dto, landlordId!);
            return CreatedAtAction(nameof(GetById), new { id = listing.Id }, listing);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ListingUpdateDTO updatedDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var result = await _service.UpdateAsync(id, updatedDto, Guid.Parse(userId));
            if (!result.Success) return Forbid(result.Error);

            return Ok(result.Data);
        }



        [Authorize(Roles = "Landlord")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var landlordId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _service.Delete(id, landlordId!);
            return success ? NoContent() : Forbid();
        }
    }
}
