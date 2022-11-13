using CommandModel;
using InMemory;
using QueryModel;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

InMemoryTodoItemRepository repository = new();
services.AddSingleton<ITodoItemRepository>(repository);
services.AddSingleton<ITodoItemReader>(repository);

services.AddControllers();
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
app.MapControllers();
app.Run();

public partial class Program { }
