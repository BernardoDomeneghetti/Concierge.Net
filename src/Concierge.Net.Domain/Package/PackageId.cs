using Concierge.Net.Domain.Base;

namespace Concierge.Net.Domain.Packages;

public class PackageId: Id<PackageId>
{
    private PackageId(Guid value) : base(value){}

    public static PackageId New()
    {
        return new PackageId(Guid.NewGuid());
    }

    public static PackageId From(Guid value)
    {
        return new PackageId(value);
    }
}
