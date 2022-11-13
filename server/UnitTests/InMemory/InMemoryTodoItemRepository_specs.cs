using CommandModel;
using QueryModel;

namespace InMemory;

public class InMemoryTodoItemRepository_specs
{
    [Theory, AutoData]
    public async Task AddItem_correctly_adds_new_item(
        InMemoryTodoItemRepository sut,
        TodoItem newItem)
    {
        // Act
        await sut.AddItem(newItem);

        // Assert
        TodoItemView? actual = await sut.FindItem(newItem.Id);
        actual.Should().BeEquivalentTo(newItem);
    }

    [Theory, AutoData]
    public async Task AddItem_has_guard_against_id_duplication(
        InMemoryTodoItemRepository sut,
        Guid id,
        TodoItem source1,
        TodoItem source2)
    {
        await sut.AddItem(source1 with { Id = id });
        Func<Task> action = () => sut.AddItem(source1 with { Id = id });
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Theory, AutoData]
    public async Task TryUpdate_returns_true_if_item_exists(
        InMemoryTodoItemRepository sut,
        TodoItem item)
    {
        await sut.AddItem(item);
        bool actual = await sut.TryUpdate(item.Id, x => x);
        actual.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task TryUpdate_returns_false_if_item_not_exists(
        InMemoryTodoItemRepository sut,
        Guid id)
    {
        bool actual = await sut.TryUpdate(id, x => x);
        actual.Should().BeFalse();
    }

    [Theory, AutoData]
    public async Task TryUpdate_correctly_changes_properties(
        InMemoryTodoItemRepository sut,
        TodoItem item,
        TodoItem revisionSource)
    {
        await sut.AddItem(item);

        await sut.TryUpdate(item.Id, x => revisionSource with { Id = x.Id });

        TodoItemView? actual = await sut.FindItem(item.Id);
        actual.Should().BeEquivalentTo(revisionSource, c => c.Excluding(x => x.Id));
    }

    [Theory, AutoData]
    public async Task TryUpdate_fails_if_reviser_changes_id(
        InMemoryTodoItemRepository sut,
        TodoItem item,
        Guid newId)
    {
        await sut.AddItem(item);
        Func<Task> action = () => sut.TryUpdate(item.Id, x => x with { Id = newId });
        await action.Should().ThrowAsync<InvalidOperationException>();
    }
}
