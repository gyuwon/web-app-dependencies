using Commands;

namespace CommandModel;

public sealed class MarkAsUndoneCommandExecutor
{
    private readonly ITodoItemRepository _repository;

    public MarkAsUndoneCommandExecutor(ITodoItemRepository repository)
        => _repository = repository;

    public Task Execute(Guid id, MarkAsUndone command)
        => _repository.Update(id, item => item.MarkAsUndone());
}
