using Microsoft.EntityFrameworkCore;
using FlightBoard.Domain.Entities;

namespace FlightBoard.Infrastructure.Data;

public class FlightBoardDbContext : DbContext
{
    public FlightBoardDbContext(DbContextOptions<FlightBoardDbContext> options) : base(options)
    {
    }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<FlightStatus> FlightStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Flight entity
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FlightNumber).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Destination).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Gate).IsRequired().HasMaxLength(10);
            entity.Property(e => e.DepartureTime).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            
            // Make FlightNumber unique
            entity.HasIndex(e => e.FlightNumber).IsUnique();
            
            // Configure relationship with FlightStatus
            entity.HasMany(e => e.StatusHistory)
                  .WithOne(e => e.Flight)
                  .HasForeignKey(e => e.FlightId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure FlightStatus entity
        modelBuilder.Entity<FlightStatus>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.Timestamp).IsRequired();
        });

        // Seed mock data
        modelBuilder.Entity<Flight>().HasData(
            new Flight
            {
                Id = 1,
                FlightNumber = "IS100",
                Destination = "Tel Aviv",
                DepartureTime = new DateTime(2024, 6, 12, 12, 0, 0, DateTimeKind.Utc),
                Gate = "A1",
                CreatedAt = new DateTime(2024, 6, 12, 10, 0, 0, DateTimeKind.Utc)
            },
            new Flight
            {
                Id = 2,
                FlightNumber = "IS200",
                Destination = "London",
                DepartureTime = new DateTime(2024, 7, 14, 12, 0, 0, DateTimeKind.Utc),
                Gate = "B2",
                CreatedAt = new DateTime(2024, 7, 14, 10, 0, 0, DateTimeKind.Utc)
            }
        );
    }
} 