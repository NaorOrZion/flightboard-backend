using MediatR;
using FlightBoard.Domain.Repositories;
using FlightBoard.Domain.Services;
using FlightBoard.Application.DTOs;

namespace FlightBoard.Application.Handlers;

public record GetAllFlightsQuery : IRequest<IEnumerable<FlightDto>>;

public class GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsQuery, IEnumerable<FlightDto>>
{
    private readonly IFlightRepository _flightRepository;
    private readonly IFlightStatusService _flightStatusService;

    public GetAllFlightsQueryHandler(IFlightRepository flightRepository, IFlightStatusService flightStatusService)
    {
        _flightRepository = flightRepository;
        _flightStatusService = flightStatusService;
    }

    public async Task<IEnumerable<FlightDto>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
    {
        var flights = await _flightRepository.GetAllAsync();
        var currentTime = DateTime.Now;

        return flights.Select(flight => new FlightDto
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            Destination = flight.Destination,
            DepartureTime = flight.DepartureTime,
            Gate = flight.Gate,
            Status = _flightStatusService.CalculateFlightStatus(flight.DepartureTime, currentTime),
            StatusDisplayName = _flightStatusService.GetStatusDisplayName(
                _flightStatusService.CalculateFlightStatus(flight.DepartureTime, currentTime)),
            CreatedAt = flight.CreatedAt,
            UpdatedAt = flight.UpdatedAt
        });
    }
} 