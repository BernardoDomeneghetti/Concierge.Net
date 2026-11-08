using Concierge.Net.Resident.Domain.Base;

namespace Concierge.Net.Resident.Domain.Residents;

public sealed class Cpf : ValueObject
{
    public string Value { get; }

    private Cpf(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CPF cannot be empty.", nameof(value));

        var digits = new string(value.Where(char.IsDigit).ToArray());

        if (digits.Length != 11)
            throw new ArgumentException("CPF must contain exactly 11 digits.", nameof(value));

        Value = digits;
    }

    public static Cpf From(string value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
