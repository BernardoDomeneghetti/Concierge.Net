using Concierge.Net.Resident.Domain.Base;

namespace Concierge.Net.Resident.Domain.Residents;

public sealed class PhoneNumber : ValueObject
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty.", nameof(value));

        var digits = new string(value.Where(char.IsDigit).ToArray());

        if (digits.Length is < 10 or > 15)
            throw new ArgumentException("Phone number must contain between 10 and 15 digits.", nameof(value));

        Value = digits;
    }

    public static PhoneNumber From(string value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
