using System.Text;
using Foundation;

namespace CommandModel;

public class EntityNotFoundException : InvalidOperationException
{
    public EntityNotFoundException(string source, string subject)
        : base(ComposeMessage(source, subject))
        => Error = new("EntityNotFound", Message);

    public ServiceError Error { get; }

    private static string ComposeMessage(string source, string subject)
    {
        StringBuilder builder = new();

        builder.AppendLine("Could not find entity");
        builder.AppendLine(FormatSource(source));
        builder.AppendLine(FormatSubject(subject));

        return builder.ToString();
    }

    private static string FormatSource(string? source)
        => $"Source: {source}";

    private static string FormatSubject(string? subject)
        => $"Subject: {subject}";

    public static EntityNotFoundException Create<T>(Guid id)
        => new(source: typeof(T).Name, subject: $"{id}");
}
