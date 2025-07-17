using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FlightBoard.Domain.Repositories;
using FlightBoard.Infrastructure.Data;
using FlightBoard.Infrastructure.Repositories;

namespace FlightBoard.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Register DbContext
        services.AddDbContext<FlightBoardDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register Repositories
        services.AddScoped<IFlightRepository, FlightRepository>();

        return services;
    }
} 