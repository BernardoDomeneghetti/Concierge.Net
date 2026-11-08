using Concierge.Net.Package.Domain.Base;

namespace Concierge.Net.Package.Domain.Residents;

public class ResidentId : Id<ResidentId>
{
    private ResidentId(Guid value) : base(value) { }

    public static ResidentId New()
    {
        return new ResidentId(Guid.NewGuid());
    }

    public static ResidentId From(Guid value)
    {
        return new ResidentId(value);
    }
}
