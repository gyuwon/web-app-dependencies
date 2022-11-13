using Commands;

namespace CommandModel;

public sealed class AddTodoItemCommandExecutor
{
    private readonly ITodoItemRepository _repository;

    public AddTodoItemCommandExecutor(ITodoItemRepository repository)
        => _repository = repository;

    public Task Execute(Guid id, AddTodoItem command)
        => _repository.AddItem(TodoItem.Create(id, command.Text));
}
