using Commands;
using Foundation;
using QueryModel;

namespace api.todos.id.mark_as_undone;

public class Post_specs
{
    [Theory, AutoTodosData]
    public async Task Sut_returns_OK_status_code(
        TodosServer server,
        string text,
        MarkAsUndone command)
    {
        ReferenceCarrier reference = await server.AddTodoItem(text);
        await server.MarkAsDone(reference.Id);
        HttpClient client = server.CreateClient();
        string path = $"api/todos/{reference.Id}/mark-as-undone";

        HttpResponseMessage response = await client.PostAsJsonAsync(path, command);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory, AutoTodosData]
    public async Task Sut_correctly_changes_state(
        TodosServer server,
        string text,
        MarkAsUndone command)
    {
        ReferenceCarrier reference = await server.AddTodoItem(text);
        await server.MarkAsDone(reference.Id);
        HttpClient client = server.CreateClient();
        string path = $"api/todos/{reference.Id}/mark-as-undone";

        await client.PostAsJsonAsync(path, command);

        TodoItemView item = await server.GetTodoItem(reference.Id);
        item.IsDone.Should().BeFalse();
    }

    [Theory, AutoTodosData]
    public async Task Sut_returns_BadRequest_if_item_is_not_completed(
        TodosServer server,
        string text,
        MarkAsUndone command)
    {
        ReferenceCarrier reference = await server.AddTodoItem(text);
        HttpClient client = server.CreateClient();
        string path = $"api/todos/{reference.Id}/mark-as-undone";

        HttpResponseMessage response = await client.PostAsJsonAsync(path, command);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
