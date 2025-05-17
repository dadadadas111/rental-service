using Microsoft.EntityFrameworkCore;
using RentalAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Listing> Listings => Set<Listing>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Unique constraint
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // User -> Listing (1:M)
        modelBuilder.Entity<Listing>()
            .HasOne(l => l.Landlord)
            .WithMany(u => u.Listings!)
            .HasForeignKey(l => l.LandlordId)
            .OnDelete(DeleteBehavior.Restrict);

        // User -> Booking (1:M)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Student)
            .WithMany(u => u.Bookings!)
            .HasForeignKey(b => b.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Listing -> Booking (1:M)
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Listing)
            .WithMany(l => l.Bookings)
            .HasForeignKey(b => b.ListingId)
            .OnDelete(DeleteBehavior.Restrict);

        // Booking -> Review (1:1)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Booking)
            .WithOne()
            .HasForeignKey<Review>(r => r.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
