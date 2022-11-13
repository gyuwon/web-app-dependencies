using api;
using CommandModel;
using Commands;
using Foundation;

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
}
