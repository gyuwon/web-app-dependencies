using Foundation;
using QueryModel;

namespace api.todos.id;

public class Get_specs
{
    [Theory, AutoTodosData]
    public async Task Sut_returns_OK_status_code(
        TodosServer server,
        string text)
    {
        ReferenceCarrier reference = await server.AddTodoItem(text);
        using HttpClient client = server.CreateClient();
        string path = $"api/todos/{reference.Id}";

        HttpResponseMessage response = await client.GetAsync(path);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory, AutoTodosData]
    public async Task Sut_returns_NotFound_if_entity_not_exists(
        TodosServer server,
        Guid id)
    {
        using HttpClient client = server.CreateClient();
        string path = $"api/todos/{id}";

        HttpResponseMessage response = await client.GetAsync(path);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory, AutoTodosData]
    public async Task Sut_returns_correct_entity(
        TodosServer server,
        string text)
    {
        ReferenceCarrier reference = await server.AddTodoItem(text);
        using HttpClient client = server.CreateClient();
        string path = $"api/todos/{reference.Id}";

        HttpResponseMessage response = await client.GetAsync(path);

        TodoItemView? actual = await response.Content.ReadFromJsonAsync<TodoItemView>();
        actual.Should().NotBeNull();
        actual!.Id.Should().Be(reference.Id);
        actual.Text.Should().Be(text);
        actual.IsDone.Should().BeFalse();
    }
}
