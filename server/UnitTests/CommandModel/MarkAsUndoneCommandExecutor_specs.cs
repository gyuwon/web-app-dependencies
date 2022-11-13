using Commands;
using InMemory;
using QueryModel;

namespace CommandModel;

public class MarkAsUndoneCommandExecutor_specs
{
    [Theory, AutoData]
    public async Task Sut_correctly_updates_item(
        [Frozen(by: Matching.ImplementedInterfaces)]
            InMemoryTodoItemRepository repository,
        Guid id,
        string text,
        MarkAsUndoneCommandExecutor sut,
        MarkAsUndone command)
    {
        await repository.AddTodoItem(id, text);
        await repository.MarkAsDone(id);

        await sut.Execute(id, command);

        TodoItemView actual = (await repository.FindItem(id))!;
        actual.IsDone.Should().BeFalse();
    }
}
