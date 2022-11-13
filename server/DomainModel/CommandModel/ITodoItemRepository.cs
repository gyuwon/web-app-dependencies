namespace CommandModel;

public interface ITodoItemRepository
{
    Task AddItem(TodoItem item);

    Task<bool> TryUpdate(Guid id, Func<TodoItem, TodoItem> reviser);
}
