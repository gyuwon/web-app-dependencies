using Commands;

namespace CommandModel;

public sealed class ChangeTextCommandExecutor
{
    private readonly ITodoItemRepository _repository;

    public ChangeTextCommandExecutor(ITodoItemRepository repository)
        => _repository = repository;

    public Task Execute(Guid id, ChangeText command)
        => _repository.Update(id, item => item.ChangeText(command));
}
