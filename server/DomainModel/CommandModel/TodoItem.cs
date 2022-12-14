using Commands;

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

    internal TodoItem ChangeText(ChangeText command)
    {
        AssertThatTextIsNotEmpty(command.Text);
        AssertThatInProgress();
        return this with { Text = command.Text };
    }

    private void AssertThatInProgress()
    {
        if (IsDone)
        {
            string message = "Cannot change the text of completed task.";
            throw new InvariantViolationException(message);
        }
    }

    internal TodoItem MarkAsDone() => this with { IsDone = true };

    internal TodoItem MarkAsUndone() => this with { IsDone = false };
}
