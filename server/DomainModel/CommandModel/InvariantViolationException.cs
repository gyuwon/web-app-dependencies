using Foundation;

namespace CommandModel;

public class InvariantViolationException : InvalidOperationException
{
    public InvariantViolationException(string message)
        : base(message) => Error = new("InvariantViolation", message);

    public ServiceError Error { get; }
}
