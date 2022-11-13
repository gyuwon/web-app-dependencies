namespace QueryModel;

public interface ITodoItemReader
{
    Task<TodoItemView?> FindItem(Guid id);
}
