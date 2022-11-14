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
        [FromServices] AddTodoItemCommandExecutor executor,
        [FromBody] AddTodoItem command)
    {
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
        [FromServices] ChangeTextCommandExecutor executor,
        Guid id,
        [FromBody] ChangeText command)
    {
        return executor.Execute(id, command);
    }

    [HttpPost("{id}/mark-as-done")]
    public async Task<IActionResult> MarkAsDone(
        [FromServices] ITodoItemReader reader,
        [FromServices] MarkAsDoneCommandExecutor executor,
        Guid id,
        [FromBody] MarkAsDone command)
    {
        TodoItemView? item = await reader.FindItem(id);
        if (item?.IsDone == true)
        {
            string code = "InvalidCommand";
            string message = "The item has already been done.";
            return BadRequest(new ServiceError(code, message));
        }

        await executor.Execute(id, command);
        return Ok();
    }
}
