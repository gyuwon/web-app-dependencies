using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace api;

public sealed class TodosServer : TestServer
{
    public TodosServer(
        IServiceProvider services,
        IOptions<TestServerOptions> optionsAccessor)
        : base(services, optionsAccessor)
    {
    }

    public static TodosServer Create()
        => (TodosServer)new Factory().Server;

    private sealed class Factory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
            => base.CreateHost(builder.ConfigureServices(
                s => s.AddSingleton<IServer, TodosServer>()));
    }
}
