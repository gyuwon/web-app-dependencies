using Commands;

namespace CommandModel;

internal static class TestSpecificLanguage
{
    public static Task AddTodoItem(
        this ITodoItemRepository repository,
        Guid id,
        string text)
    {
        AddTodoItemCommandExecutor executor = new(repository);
        AddTodoItem command = new AddTodoItem(text);
        return executor.Execute(id, command);
    }

    public static Task MarkAsDone(
        this ITodoItemRepository repository,
        Guid id)
    {
        MarkAsDoneCommandExecutor executor = new(repository);
        return executor.Execute(id, new MarkAsDone());
    }
}