using System;
using Concierge.Net.Domain.Base;

namespace Concierge.Net.Domain.Residents;

public class UnitId: Id<UnitId>
{
    private UnitId(Guid value) : base(value){}

    public static UnitId New()
    {
        return new UnitId(Guid.NewGuid());
    }

    public static UnitId From(Guid value)
    {
        return new UnitId(value);
    }

}
