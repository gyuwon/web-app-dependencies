using Commands;

namespace CommandModel;

public sealed class MarkAsDoneCommandExecutor
{
    private readonly ITodoItemRepository _repository;

    public MarkAsDoneCommandExecutor(ITodoItemRepository repository)
        => _repository = repository;

    public Task Execute(Guid id, MarkAsDone command)
        => _repository.Update(id, item => item.MarkAsDone());
}
