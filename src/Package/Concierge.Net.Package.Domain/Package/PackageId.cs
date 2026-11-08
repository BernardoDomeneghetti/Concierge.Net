using Concierge.Net.Package.Domain.Base;

namespace Concierge.Net.Package.Domain.Packages;

public class PackageId : Id<PackageId>
{
    private PackageId(Guid value) : base(value) { }

    public static PackageId New()
    {
        return new PackageId(Guid.NewGuid());
    }

    public static PackageId From(Guid value)
    {
        return new PackageId(value);
    }
}
