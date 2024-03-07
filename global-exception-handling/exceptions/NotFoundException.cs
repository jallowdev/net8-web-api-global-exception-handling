namespace global_exception_handling.exceptions;

public class NotFoundException:Exception
{
    public NotFoundException(string? message) : base(message)
    {
    }
}