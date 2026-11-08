using Concierge.Net.Package.Domain.Base;
using Concierge.Net.Package.Domain.Packages;
using Concierge.Net.Package.Domain.Residents;
using Concierge.Net.Package.Domain.Shared;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Concierge.Net.Package.Worker.Infraestructure.Persistence;

public static class MongoBsonConfiguration
{
    private static bool _configured;
    private static readonly Lock LockObj = new();

    public static void Configure()
    {
        lock (LockObj)
        {
            if (_configured)
                return;

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            BsonSerializer.RegisterSerializer(new PackageIdSerializer());
            BsonSerializer.RegisterSerializer(new ResidentIdSerializer());
            BsonSerializer.RegisterSerializer(new UnitIdSerializer());
            BsonSerializer.RegisterSerializer(new EmailSerializer());

            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity<PackageId>)))
            {
                BsonClassMap.RegisterClassMap<Entity<PackageId>>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(e => e.Id);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(AggregateRoot<PackageId>)))
            {
                BsonClassMap.RegisterClassMap<AggregateRoot<PackageId>>(cm =>
                {
                    cm.AutoMap();
                    cm.UnmapProperty(a => a.DomainEvents);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Domain.Packages.Package)))
            {
                BsonClassMap.RegisterClassMap<Domain.Packages.Package>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(DomainEvent)))
            {
                BsonClassMap.RegisterClassMap<DomainEvent>(cm =>
                {
                    cm.MapProperty(e => e.EventId);
                    cm.MapProperty(e => e.OccurredOnUtc);
                    cm.SetIsRootClass(true);
                });

                BsonClassMap.RegisterClassMap<PackageReceivedDomainEvent>(cm =>
                {
                    cm.MapProperty(e => e.PackageId);
                    cm.MapProperty(e => e.ResidentId);
                    cm.MapProperty(e => e.NotificationEmail);
                    cm.MapProperty(e => e.UnitId);
                    cm.MapCreator(e => new PackageReceivedDomainEvent(e.PackageId, e.ResidentId, e.NotificationEmail, e.UnitId));
                });

                BsonClassMap.RegisterClassMap<PackageCollectedDomainEvent>(cm =>
                {
                    cm.MapProperty(e => e.PackageId);
                    cm.MapProperty(e => e.CollectedByResidentId);
                    cm.MapProperty(e => e.UnitId);
                    cm.MapProperty(e => e.SignatureHash);
                    cm.MapProperty(e => e.CollectedAtUtc);
                    cm.MapProperty(e => e.NotificationEmail);
                    cm.MapCreator(e => new PackageCollectedDomainEvent(
                        e.PackageId,
                        e.CollectedByResidentId,
                        e.UnitId,
                        e.SignatureHash,
                        e.CollectedAtUtc,
                        e.NotificationEmail));
                });

                BsonClassMap.RegisterClassMap<PackageCancelledDomainEvent>(cm =>
                {
                    cm.MapProperty(e => e.PackageId);
                    cm.MapProperty(e => e.ResidentId);
                    cm.MapProperty(e => e.UnitId);
                    cm.MapProperty(e => e.Reason);
                    cm.MapProperty(e => e.CancelledAtUtc);
                    cm.MapProperty(e => e.NotificationEmail);
                    cm.MapCreator(e => new PackageCancelledDomainEvent(
                        e.PackageId,
                        e.ResidentId,
                        e.UnitId,
                        e.Reason,
                        e.CancelledAtUtc,
                        e.NotificationEmail));
                });
            }

            _configured = true;
        }
    }

    private sealed class PackageIdSerializer : SerializerBase<PackageId>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PackageId value)
            => BsonSerializer.Serialize(context.Writer, value.Value);

        public override PackageId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => PackageId.From(BsonSerializer.Deserialize<Guid>(context.Reader));
    }

    private sealed class ResidentIdSerializer : SerializerBase<ResidentId>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ResidentId value)
            => BsonSerializer.Serialize(context.Writer, value.Value);

        public override ResidentId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => ResidentId.From(BsonSerializer.Deserialize<Guid>(context.Reader));
    }

    private sealed class UnitIdSerializer : SerializerBase<UnitId>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, UnitId value)
            => BsonSerializer.Serialize(context.Writer, value.Value);

        public override UnitId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => UnitId.From(BsonSerializer.Deserialize<Guid>(context.Reader));
    }

    private sealed class EmailSerializer : SerializerBase<Email>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Email value)
            => context.Writer.WriteString(value.Value);

        public override Email Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Email.From(context.Reader.ReadString());
    }
}
