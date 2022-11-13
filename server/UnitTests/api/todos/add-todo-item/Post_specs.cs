using Commands;
using Foundation;

namespace api.todos.add_todo_item;

public class Post_specs
{
    private const string Path = "/api/todos/add-todo-item";

    [Theory, AutoTodosData]
    public async Task Sut_returns_OK_status_code(
        TodosServer server,
        AddTodoItem command)
    {
        using HttpClient client = server.CreateClient();
        HttpResponseMessage response = await client.PostAsJsonAsync(Path, command);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory, AutoTodosData]
    public async Task Sut_returns_reference(
        TodosServer server,
        AddTodoItem command)
    {
        using HttpClient client = server.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync(Path, command);

        ReferenceCarrier? actual = await response.Content.ReadFromJsonAsync<ReferenceCarrier>();
        actual.Should().NotBeNull();
        actual!.Id.Should().NotBeEmpty();
    }
}
