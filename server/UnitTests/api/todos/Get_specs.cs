using Foundation;
using QueryModel;

namespace api.todos;

public class Get_specs
{
    [Theory, AutoTodosData]
    public async Task Sut_returns_OK_status_code(TodosServer server)
    {
        using HttpClient client = server.CreateClient();
        HttpResponseMessage response = await client.GetAsync("api/todos");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory, AutoTodosData]
    public async Task Sut_returns_all_items(
        TodosServer server,
        string[] texts)
    {
        await Task.WhenAll(texts.Select(server.AddTodoItem));
        using HttpClient client = server.CreateClient();

        HttpResponseMessage response = await client.GetAsync("api/todos");

        ArrayCarrier<TodoItemView>? actual = await
            response.Content.ReadFromJsonAsync<ArrayCarrier<TodoItemView>>();
        actual.Should().NotBeNull();
        actual!.Items.Select(x => x.Text).Should().Contain(texts);
    }
}
