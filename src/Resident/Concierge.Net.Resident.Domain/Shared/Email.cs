using System.ComponentModel.DataAnnotations;
using Concierge.Net.Resident.Domain.Base;

namespace Concierge.Net.Resident.Domain.Shared;

public sealed class Email(string value) : ValueObject
{
    [EmailAddress]
    public string Value { get; } = !string.IsNullOrWhiteSpace(value)
        ? value.Trim()
        : throw new ArgumentException("No value has been provided for e-mail value.", nameof(value));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Email From(string value) => new(value);

    public override string ToString() => Value;
}
