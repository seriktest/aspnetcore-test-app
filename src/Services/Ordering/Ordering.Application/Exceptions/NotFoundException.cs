namespace Ordering.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message, object key)
        : base($"Entity \"{message}\" ({key}) was not found"){}
}