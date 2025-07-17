using Microsoft.AspNetCore.Mvc; // 1. Use ASP.NET Core's web API features
using MediatR; // 2. Use MediatR for request/response handling
using FlightBoard.Application.Handlers; // 3. Use our business logic handlers
using FlightBoard.Application.DTOs; // 4. Use DTOs for data transfer
using FlightBoard.Domain.Entities; // 5. Use FlightStatusType enum
using Microsoft.AspNetCore.SignalR; // 6. Use SignalR for real-time updates
using FlightBoard.API.Hubs; // 7. Use our SignalR hub

namespace FlightBoard.API.Controllers; // 8. This code belongs to the API.Controllers namespace

[ApiController] // 9. Marks this class as a web API controller
[Route("api/[controller]")] // 10. Sets the route to /api/flights (controller name minus 'Controller')
public class FlightsController : ControllerBase // 11. Inherit from ControllerBase for API features
{
    private readonly IMediator _mediator; // 12. Field to hold the MediatR mediator
    private readonly IHubContext<FlightBoardHub> _hubContext; // 13. Field for SignalR hub context

    // 14. Constructor: ASP.NET Core will give us an IMediator and IHubContext automatically
    public FlightsController(IMediator mediator, IHubContext<FlightBoardHub> hubContext)
    {
        _mediator = mediator; // 15. Save the mediator for use in our methods
        _hubContext = hubContext; // 16. Save the hub context for broadcasting
    }

    [HttpGet] // 17. Handles GET requests to /api/flights
    public async Task<IActionResult> GetAllFlights()
    {
        var flights = await _mediator.Send(new GetAllFlightsQuery()); // 18. Get all flights
        return Ok(flights); // 19. Return 200 OK with the flights
    }

    [HttpPost] // 20. Handles POST requests to /api/flights
    public async Task<IActionResult> AddFlight([FromBody] CreateFlightDto dto)
    {
        try
        {
            var created = await _mediator.Send(new CreateFlightCommand(dto)); // 21. Add the flight
            await _hubContext.Clients.All.SendAsync("FlightAdded", created); // 22. Broadcast to all clients
            return CreatedAtAction(nameof(GetAllFlights), new { id = created.Id }, created); // 23. Return 201 Created
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message }); // 24. Return 400 Bad Request if validation fails
        }
    }

    [HttpDelete("{id}")] // 25. Handles DELETE requests to /api/flights/{id}
    public async Task<IActionResult> DeleteFlight(int id)
    {
        try
        {
            await _mediator.Send(new DeleteFlightCommand(id)); // 26. Delete the flight
            await _hubContext.Clients.All.SendAsync("FlightDeleted", id); // 27. Broadcast to all clients
            return NoContent(); // 28. Return 204 No Content on success
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { error = ex.Message }); // 29. Return 404 if flight not found
        }
    }

    [HttpGet("search")] // 30. Handles GET requests to /api/flights/search
    public async Task<IActionResult> SearchFlights([FromQuery] FlightStatusType? status, [FromQuery] string? destination)
    {
        var flights = await _mediator.Send(new SearchFlightsQuery(status, destination)); // 31. Search flights
        return Ok(flights); // 32. Return 200 OK with the results
    }
} 