using Concierge.Net.Resident.Domain.Base;

namespace Concierge.Net.Resident.Domain.Residents;

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
