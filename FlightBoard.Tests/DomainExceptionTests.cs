using Xunit;
using FlightBoard.Domain.Exceptions;

public class DomainExceptionTests
{
    [Fact]
    public void FlightValidationException_StoresMessage()
    {
        var ex = new FlightValidationException("Test error");
        Assert.Equal("Test error", ex.Message);
    }
} 