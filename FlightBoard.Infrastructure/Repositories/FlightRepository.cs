using Microsoft.EntityFrameworkCore;
using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Repositories;
using FlightBoard.Infrastructure.Data;

namespace FlightBoard.Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly FlightBoardDbContext _context;

    public FlightRepository(FlightBoardDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Flight>> GetAllAsync()
    {
        var conn = _context.Database.GetDbConnection();
        Console.WriteLine("Actual DB Path: " + conn.DataSource);
        
        return await _context.Flights
            .Include(f => f.StatusHistory)
            .OrderBy(f => f.DepartureTime)
            .ToListAsync();
    }

    public async Task<Flight?> GetByIdAsync(int id)
    {
        return await _context.Flights
            .Include(f => f.StatusHistory)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Flight?> GetByFlightNumberAsync(string flightNumber)
    {
        return await _context.Flights
            .Include(f => f.StatusHistory)
            .FirstOrDefaultAsync(f => f.FlightNumber == flightNumber);
    }

    public async Task<IEnumerable<Flight>> GetByStatusAndDestinationAsync(FlightStatusType? status, string? destination)
    {
        var query = _context.Flights.Include(f => f.StatusHistory).AsQueryable();

        if (!string.IsNullOrWhiteSpace(destination))
        {
            query = query.Where(f => f.Destination.Contains(destination));
        }

        var flights = await query.OrderBy(f => f.DepartureTime).ToListAsync();

        // Filter by status if provided (status is calculated, not stored)
        if (status.HasValue)
        {
            var currentTime = DateTime.UtcNow;
            flights = flights.Where(f =>
            {
                var timeDifference = f.DepartureTime - currentTime;
                var totalMinutes = timeDifference.TotalMinutes;

                var calculatedStatus = totalMinutes switch
                {
                    > 30 => FlightStatusType.Scheduled,
                    <= 30 and >= 0 => FlightStatusType.Boarding,
                    < 0 and >= -60 => FlightStatusType.Departed,
                    < -60 => FlightStatusType.Landed,
                    _ => FlightStatusType.Scheduled
                };

                return calculatedStatus == status.Value;
            }).ToList();
        }

        return flights;
    }

    public async Task<Flight> AddAsync(Flight flight)
    {
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
        return flight;
    }

    public async Task<Flight> UpdateAsync(Flight flight)
    {
        flight.UpdatedAt = DateTime.UtcNow;
        _context.Flights.Update(flight);
        await _context.SaveChangesAsync();
        return flight;
    }

    public async Task DeleteAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight != null)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> FlightNumberExistsAsync(string flightNumber, int excludeId = 0)
    {
        return await _context.Flights
            .AnyAsync(f => f.FlightNumber == flightNumber && f.Id != excludeId);
    }
} 