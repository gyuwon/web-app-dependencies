namespace CommandModel;

internal static class TodoItemRepositoryExtensions
{
    public static async Task Update(
        this ITodoItemRepository repository,
        Guid id,
        Func<TodoItem, TodoItem> reviser)
    {
        if (await repository.TryUpdate(id, reviser) == false)
        {
            throw EntityNotFoundException.Create<TodoItem>(id);
        }
    }
}
