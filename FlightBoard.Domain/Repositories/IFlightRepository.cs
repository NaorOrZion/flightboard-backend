using FlightBoard.Domain.Entities;

namespace FlightBoard.Domain.Repositories;

// Every class that implements this interface must have these methods
public interface IFlightRepository
{
    Task<IEnumerable<Flight>> GetAllAsync();
    Task<Flight?> GetByIdAsync(int id);
    Task<Flight?> GetByFlightNumberAsync(string flightNumber);
    Task<IEnumerable<Flight>> GetByStatusAndDestinationAsync(FlightStatusType? status, string? destination);
    Task<Flight> AddAsync(Flight flight);
    Task<Flight> UpdateAsync(Flight flight);
    Task DeleteAsync(int id);
    Task<bool> FlightNumberExistsAsync(string flightNumber, int excludeId = 0);
} 