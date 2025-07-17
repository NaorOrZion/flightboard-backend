using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMockFlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "CreatedAt", "DepartureTime", "Destination", "FlightNumber", "Gate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 12, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 12, 12, 0, 0, 0, DateTimeKind.Utc), "Tel Aviv", "IS100", "A1", null },
                    { 2, new DateTime(2024, 7, 14, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 14, 12, 0, 0, 0, DateTimeKind.Utc), "London", "IS200", "B2", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
