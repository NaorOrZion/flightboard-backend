using MediatR;
using FlightBoard.Domain.Repositories;
using FlightBoard.Domain.Entities;
using FlightBoard.Application.DTOs;

namespace FlightBoard.Application.Handlers;

public record CreateFlightCommand(CreateFlightDto Flight) : IRequest<FlightDto>;

public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, FlightDto>
{
    private readonly IFlightRepository _flightRepository;

    public CreateFlightCommandHandler(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<FlightDto> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var flightDto = request.Flight;

        // Validation
        if (string.IsNullOrWhiteSpace(flightDto.FlightNumber))
            throw new ArgumentException("Flight number is required.");

        if (string.IsNullOrWhiteSpace(flightDto.Destination))
            throw new ArgumentException("Destination is required.");

        if (string.IsNullOrWhiteSpace(flightDto.Gate))
            throw new ArgumentException("Gate is required.");

        if (flightDto.DepartureTime <= DateTime.Now)
            throw new ArgumentException("Departure time must be in the future.");

        // Check if flight number already exists
        if (await _flightRepository.FlightNumberExistsAsync(flightDto.FlightNumber))
            throw new ArgumentException("Flight number already exists.");

        var flight = new Flight
        {
            FlightNumber = flightDto.FlightNumber,
            Destination = flightDto.Destination,
            DepartureTime = flightDto.DepartureTime,
            Gate = flightDto.Gate,
            CreatedAt = DateTime.Now
        };

        var createdFlight = await _flightRepository.AddAsync(flight);

        return new FlightDto
        {
            Id = createdFlight.Id,
            FlightNumber = createdFlight.FlightNumber,
            Destination = createdFlight.Destination,
            DepartureTime = createdFlight.DepartureTime,
            Gate = createdFlight.Gate,
            Status = FlightStatusType.Scheduled, // New flights are always scheduled
            StatusDisplayName = "Scheduled",
            CreatedAt = createdFlight.CreatedAt,
            UpdatedAt = createdFlight.UpdatedAt
        };
    }
} 