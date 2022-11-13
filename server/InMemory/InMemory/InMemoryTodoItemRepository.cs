using CommandModel;
using QueryModel;

namespace InMemory;

public class InMemoryTodoItemRepository : ITodoItemRepository, ITodoItemReader
{
    private readonly List<TodoItem> _items = new();

    public Task AddItem(TodoItem item)
    {
        if (_items.Any(x => x.Id == item.Id))
        {
            throw new InvalidOperationException();
        }

        _items.Add(item);
        return Task.CompletedTask;
    }

    public Task<bool> TryUpdate(Guid id, Func<TodoItem, TodoItem> reviser)
    {
        if (_items.SingleOrDefault(x => x.Id == id) is TodoItem item)
        {
            int index = _items.IndexOf(item);
            TodoItem revision = GetRevision(item, reviser);
            _items[index] = revision;
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    private static TodoItem GetRevision(
        TodoItem item,
        Func<TodoItem, TodoItem> reviser)
    {
        TodoItem revision = reviser.Invoke(item);
        if (revision.Id != item.Id)
        {
            throw new InvalidOperationException();
        }

        return revision;
    }

    public Task<TodoItemView?> FindItem(Guid id)
    {
        IEnumerable<TodoItemView> query =
            from item in _items
            where item.Id == id
            select new TodoItemView(item.Id, item.Text, item.IsDone);

        return Task.FromResult(query.SingleOrDefault());
    }
}
