using System.ComponentModel.DataAnnotations;

namespace FlightBoard.Domain.Entities;

public class Flight
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(10)]
    public string FlightNumber { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string Destination { get; set; } = string.Empty;
    
    [Required]
    public DateTime DepartureTime { get; set; }
    
    [Required]
    [StringLength(10)]
    public string Gate { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties for future extensions
    public virtual ICollection<FlightStatus> StatusHistory { get; set; } = new List<FlightStatus>();
}

public class FlightStatus
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public FlightStatusType Status { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public virtual Flight Flight { get; set; } = null!;
}

public enum FlightStatusType
{
    Scheduled,
    Boarding,
    Departed,
    Landed
} 