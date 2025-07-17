using Xunit;
using Microsoft.EntityFrameworkCore;
using FlightBoard.Infrastructure.Data;
using FlightBoard.Infrastructure.Repositories;
using FlightBoard.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Linq;

public class FlightRepositoryInMemoryTests
{
    private FlightBoardDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<FlightBoardDbContext>()
            .UseSqlite("Filename=:memory:")
            .Options;
        var context = new FlightBoardDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task AddAndGetFlight_Works()
    {
        using var context = CreateContext();
        var repo = new FlightRepository(context);
        var flight = new Flight
        {
            FlightNumber = "IS500",
            Destination = "Berlin",
            DepartureTime = DateTime.UtcNow.AddHours(2),
            Gate = "C1",
            CreatedAt = DateTime.UtcNow
        };
        await repo.AddAsync(flight);
        var all = await repo.GetAllAsync();
        Assert.Contains(all, f => f.FlightNumber == "IS500");
    }

    [Fact]
    public async Task DeleteFlight_RemovesFlight()
    {
        using var context = CreateContext();
        var repo = new FlightRepository(context);
        var flight = new Flight
        {
            FlightNumber = "IS600",
            Destination = "Rome",
            DepartureTime = DateTime.UtcNow.AddHours(3),
            Gate = "D2",
            CreatedAt = DateTime.UtcNow
        };
        await repo.AddAsync(flight);
        await repo.DeleteAsync(flight.Id);
        var all = await repo.GetAllAsync();
        Assert.DoesNotContain(all, f => f.FlightNumber == "IS600");
    }
} 