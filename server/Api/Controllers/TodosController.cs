using CommandModel;
using Commands;
using Foundation;
using Microsoft.AspNetCore.Mvc;
using QueryModel;

namespace Api.Controllers;

[Route("/api/todos")]
public class TodosController : Controller
{
    [HttpPost("add-todo-item")]
    public async Task<ReferenceCarrier> AddTodoItem(
        [FromServices] ITodoItemRepository repository,
        [FromBody] AddTodoItem command)
    {
        AddTodoItemCommandExecutor executor = new(repository);
        Guid id = Guid.NewGuid();
        await executor.Execute(id, command);
        return new ReferenceCarrier(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindItem(
        [FromServices] ITodoItemReader reader,
        Guid id)
    {
        return await reader.FindItem(id) switch
        {
            TodoItemView item => Ok(item),
            null => NotFound(),
        };
    }

    [HttpPost("{id}/change-text")]
    public Task ChangeText(
        [FromServices] ITodoItemRepository repository,
        Guid id,
        [FromBody] ChangeText command)
    {
        ChangeTextCommandExecutor executor = new(repository);
        return executor.Execute(id, command);
    }
}
