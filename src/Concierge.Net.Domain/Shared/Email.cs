using System;
using System.ComponentModel.DataAnnotations;
using Concierge.Net.Domain.Base;

namespace Concierge.Net.Domain.Shared;

public sealed class Email(string value) : ValueObject
{
    [EmailAddress]
    public string Value { get; } = value 
        ?? throw new ArgumentNullException(nameof(value), "No value has been provided for e-mail value."); //TODO: Remove exception messages from hardCoded strings

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
