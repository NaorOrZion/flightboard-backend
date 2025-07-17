namespace FlightBoard.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
}

public class FlightValidationException : DomainException
{
    public FlightValidationException(string message) : base(message)
    {
    }
}

public class FlightNotFoundException : DomainException
{
    public FlightNotFoundException(string message) : base(message)
    {
    }
} 