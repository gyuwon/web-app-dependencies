using api;
using CommandModel;
using Commands;
using Foundation;
using QueryModel;

internal static class TestSpecificLanguage
{
    public static Task AddTodoItem(
        this ITodoItemRepository repository,
        Guid id,
        string text)
    {
        AddTodoItemCommandExecutor executor = new(repository);
        AddTodoItem command = new AddTodoItem(text);
        return executor.Execute(id, command);
    }

    public static Task MarkAsDone(
        this ITodoItemRepository repository,
        Guid id)
    {
        MarkAsDoneCommandExecutor executor = new(repository);
        return executor.Execute(id, new MarkAsDone());
    }

    public static async Task<ReferenceCarrier> AddTodoItem(
        this TodosServer server,
        string text)
    {
        using HttpClient client = server.CreateClient();
        string path = "/api/todos/add-todo-item";
        AddTodoItem body = new(text);
        HttpResponseMessage response = await client.PostAsJsonAsync(path, body);
        return (await response.Content.ReadFromJsonAsync<ReferenceCarrier>())!;
    }

    public static async Task<TodoItemView?> FindTodoItem(
        this TodosServer server,
        Guid id)
    {
        using HttpClient client = server.CreateClient();
        string path = $"/api/todos/{id}";
        HttpResponseMessage response = await client.GetAsync(path);
        return await response.Content.ReadFromJsonAsync<TodoItemView>();
    }

    public static async Task<TodoItemView> GetTodoItem(
        this TodosServer server,
        Guid id)
    {
        return (await server.FindTodoItem(id))!;
    }

    public static async Task<HttpResponseMessage> MarkAsDone(
        this TodosServer server,
        Guid id)
    {
        using HttpClient client = server.CreateClient();
        string path = $"/api/todos/{id}/mark-as-done";
        MarkAsDone command = new();
        return await client.PostAsJsonAsync(path, command);
    }
}
