using System.Collections.Immutable;

namespace QueryModel;

public interface ITodoItemReader
{
    Task<TodoItemView?> FindItem(Guid id);

    Task<ImmutableArray<TodoItemView>> GetAllItems();
}
