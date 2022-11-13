using Commands;
using InMemory;
using QueryModel;

namespace CommandModel;

public class AddTodoItemCommandExecutor_specs
{
    [Theory, AutoData]
    public async Task Sut_correctly_adds_new_item(
        [Frozen(by: Matching.ImplementedInterfaces)]
            InMemoryTodoItemRepository repository,
        AddTodoItemCommandExecutor sut,
        Guid id,
        AddTodoItem command)
    {
        // Act
        await sut.Execute(id, command);

        // Assert
        TodoItemView? item = await repository.FindItem(id);
        item.Should().NotBeNull();
        item.Should().BeEquivalentTo(command, c => c.ExcludingMissingMembers());
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
        AddTodoItemCommandExecutor sut,
        Guid id)
    {
        AddTodoItem command = new(text);
        Func<Task> action = () => sut.Execute(id, command);
        await action.Should().ThrowAsync<InvariantViolationException>();
    }
}
