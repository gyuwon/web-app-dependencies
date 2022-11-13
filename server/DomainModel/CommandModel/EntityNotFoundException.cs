using Foundation;

namespace CommandModel;

public class EntityNotFoundException : InvalidOperationException
{
    public EntityNotFoundException(string source, string subject)
        : base($"Could not find entity. Source: {source}, Subject: {subject}")
        => Error = new("EntityNotFound", Message);

    public ServiceError Error { get; }

    public static EntityNotFoundException Create<T>(Guid id)
        => new(source: typeof(T).Name, subject: $"{id}");
}
