using System.Collections.Immutable;

namespace Foundation;

public sealed record ArrayCarrier<T>(ImmutableArray<T> Items);

public static class ArrayCarrier
{
    public static ArrayCarrier<T> Create<T>(IEnumerable<T> items)
        => new(ImmutableArray.CreateRange(items));
}
