namespace Concierge.Net.Domain.Base;

public abstract class Id<T> : ValueObject where T : Id<T>
{
    protected Id(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public override string ToString()
    {
        return Value.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
