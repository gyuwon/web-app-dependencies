using Api.Filters;
using CommandModel;
using InMemory;
using QueryModel;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

InMemoryTodoItemRepository repository = new();
services.AddSingleton<ITodoItemRepository>(repository);
services.AddSingleton<ITodoItemReader>(repository);
services.AddSingleton<AddTodoItemCommandExecutor>();
services.AddSingleton<ChangeTextCommandExecutor>();
services.AddSingleton<MarkAsDoneCommandExecutor>();
services.AddSingleton<MarkAsUndoneCommandExecutor>();

services.AddCors();
services.AddControllers(c =>
{
    c.Filters.Add<EntityNotFoundExceptionFilter>();
    c.Filters.Add<InvariantViolationExceptionFilter>();
});
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();
app.Run();

public partial class Program { }
