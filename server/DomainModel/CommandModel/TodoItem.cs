namespace CommandModel;

public sealed record TodoItem(Guid Id, string Text, bool IsDone);
