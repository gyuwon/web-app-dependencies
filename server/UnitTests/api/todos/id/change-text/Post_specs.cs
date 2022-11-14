using Commands;
using Foundation;
using QueryModel;

namespace api.todos.id.change_text;

public class Post_specs
{
    [Theory, AutoTodosData]
    public async Task Sut_returns_OK_status_code(
        TodosServer server,
        string text,
        ChangeText command)
    {
        ReferenceCarrier reference = await server.AddTodoItem(text);
        HttpClient client = server.CreateClient();
        string path = $"api/todos/{reference.Id}/change-text";

        HttpResponseMessage response = await client.PostAsJsonAsync(path, command);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory, AutoTodosData]
    public async Task Sut_correctly_changes_text(
        TodosServer server,
        string text,
        ChangeText command)
    {
        ReferenceCarrier reference = await server.AddTodoItem(text);
        HttpClient client = server.CreateClient();
        string path = $"api/todos/{reference.Id}/change-text";

        await client.PostAsJsonAsync(path, command);

        TodoItemView item = await server.GetTodoItem(reference.Id);
        item.Text.Should().Be(command.Text);
    }

    [Theory, AutoTodosData]
    public async Task Sut_returns_NotFound_if_entity_not_found(
        TodosServer server,
        Guid id,
        ChangeText command)
    {
        HttpClient client = server.CreateClient();
        string path = $"api/todos/{id}/change-text";

        HttpResponseMessage response = await client.PostAsJsonAsync(path, command);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineAutoTodosData(null)]
    [InlineAutoTodosData("")]
    [InlineAutoTodosData(" ")]
    [InlineAutoTodosData("\r")]
    [InlineAutoTodosData("\n")]
    [InlineAutoTodosData("\t")]
    [InlineAutoTodosData("\r\n\t")]
    public async Task Sut_returns_BadRequest_if_text_is_empty(
        string newText,
        TodosServer server,
        string text)
    {
        ReferenceCarrier reference = await server.AddTodoItem(text);
        HttpClient client = server.CreateClient();
        string path = $"api/todos/{reference.Id}/change-text";
        ChangeText command = new ChangeText(newText);

        HttpResponseMessage response = await client.PostAsJsonAsync(path, command);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
