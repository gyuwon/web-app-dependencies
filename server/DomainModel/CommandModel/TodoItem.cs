namespace CommandModel;

public sealed record TodoItem(Guid Id, string Text, bool IsDone)
{
    internal static TodoItem Create(Guid id, string text)
    {
        AssertThatTextIsNotEmpty(text);

        return new(id, text, IsDone: false);
    }

    private static void AssertThatTextIsNotEmpty(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            string message = "The text of todo item cannot be blank.";
            throw new InvariantViolationException(message);
        }
    }
}
