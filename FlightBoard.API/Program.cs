using MediatR; // Register MediatR for request/response handling
using FlightBoard.Infrastructure; // Use our database/repository setup
using FlightBoard.Application.Services; // Use our business logic services
using FlightBoard.Application.Handlers; // For MediatR registration
using FlightBoard.Domain.Services; // Import the IFlightStatusService interface
using FlightBoard.API.Hubs; // Import the SignalR hub

var builder = WebApplication.CreateBuilder(args); // Create the web server builder

// Register MediatR and all handlers in the Application layer
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllFlightsQuery>());

// Register database and repositories
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=flightboard.db");

// Register business logic service
builder.Services.AddScoped<IFlightStatusService, FlightStatusService>();
Console.WriteLine("DB Connection String: " + (builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=flightboard.db"));
// Add controllers
builder.Services.AddControllers();

// Add SignalR services
builder.Services.AddSignalR();

var app = builder.Build(); // Build the web application

app.MapControllers(); // Use controllers for HTTP requests

// Map the SignalR hub endpoint
app.MapHub<FlightBoardHub>("/hubs/flightboard");

app.Run(); // Start the web server
