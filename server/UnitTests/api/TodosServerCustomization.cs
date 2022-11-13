namespace api;

public sealed class TodosServerCustomization : ICustomization
{
    public void Customize(IFixture fixture)
        => fixture.Register(TodosServer.Create);
}
