using Commands;
using InMemory;
using QueryModel;

namespace CommandModel;

public class ChangeTextCommandExecutor_specs
{
    [Theory, AutoData]
    public async Task Sut_correctly_update_text(
        [Frozen(by: Matching.ImplementedInterfaces)]
            InMemoryTodoItemRepository repository,
        TodoItem item,
        ChangeTextCommandExecutor sut,
        ChangeText command)
    {
        await repository.AddItem(item);

        await sut.Execute(item.Id, command);

        TodoItemView actual = (await repository.FindItem(item.Id))!;
        actual.Text.Should().Be(command.Text);
    }

    [Theory, AutoData]
    public async Task Sut_fails_if_item_not_exists(
        [Frozen(by: Matching.ImplementedInterfaces)]
            InMemoryTodoItemRepository repository,
        ChangeTextCommandExecutor sut,
        Guid id,
        ChangeText command)
    {
        // Act
        Func<Task> action = () => sut.Execute(id, command);

        // Assert
        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Theory]
    [InlineAutoData(null)]
    [InlineAutoData("")]
    [InlineAutoData(" ")]
    [InlineAutoData("\r")]
    [InlineAutoData("\n")]
    [InlineAutoData("\t")]
    [InlineAutoData("\r\n\t")]
    public async Task Sut_has_guard_against_invalid_text(
        string text,
        [Frozen(by: Matching.ImplementedInterfaces)]
            InMemoryTodoItemRepository repository,
        TodoItem item,
        ChangeTextCommandExecutor sut)
    {
        await repository.AddItem(item);
        ChangeText command = new(text);

        Func<Task> action = () => sut.Execute(item.Id, command);

        await action.Should().ThrowAsync<InvariantViolationException>();
    }

    [Theory, AutoTodosData]
    public async Task Sut_fails_if_todo_item_is_done(
        [Frozen(by: Matching.ImplementedInterfaces)]
            InMemoryTodoItemRepository repository,
        Guid id,
        string text,
        ChangeTextCommandExecutor sut,
        ChangeText command)
    {
        await repository.AddTodoItem(id, text);
        await repository.MarkAsDone(id);

        Func<Task> action = () => sut.Execute(id, command);

        await action.Should().ThrowAsync<InvariantViolationException>();
    }
}
