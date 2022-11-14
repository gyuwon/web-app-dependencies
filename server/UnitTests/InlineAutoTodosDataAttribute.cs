public class InlineAutoTodosDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoTodosDataAttribute(params object[]? values)
        : base(new AutoTodosDataAttribute(), values)
    {
    }
}
