using api;

public class AutoTodosDataAttribute : AutoDataAttribute
{
    public AutoTodosDataAttribute()
        : base(() =>
            new Fixture().Customize(
                new CompositeCustomization(
                    new TodosServerCustomization())))
    {
    }
}
