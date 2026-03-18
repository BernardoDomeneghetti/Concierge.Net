using System;

namespace Concierge.Net.Domain.Base;

public class Id<T> where T : Id<T>
{
    private Id(Guid value)
    {
        Value = value;
    }

    public static Id<T> New()
    {
        return new Id<T>(Guid.NewGuid());
    }

    public static Id<T> From(Guid value)
    {
        return new Id<T>(value);
    }

    public Guid Value { get; private set; }

    public override string ToString()
    {
        return Value.ToString();
    }

}
