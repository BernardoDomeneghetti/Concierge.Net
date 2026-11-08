using Concierge.Net.Resident.Domain.Base;
using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Domain.Shared;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Concierge.Net.Resident.Api.Infraestructure.Persistence;

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
            BsonSerializer.RegisterSerializer(new ResidentIdSerializer());
            BsonSerializer.RegisterSerializer(new UnitIdSerializer());
            BsonSerializer.RegisterSerializer(new CpfSerializer());
            BsonSerializer.RegisterSerializer(new PhoneNumberSerializer());
            BsonSerializer.RegisterSerializer(new EmailSerializer());

            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity<ResidentId>)))
            {
                BsonClassMap.RegisterClassMap<Entity<ResidentId>>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(e => e.Id);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(AggregateRoot<ResidentId>)))
            {
                BsonClassMap.RegisterClassMap<AggregateRoot<ResidentId>>(cm =>
                {
                    cm.AutoMap();
                    cm.UnmapProperty(a => a.DomainEvents);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Domain.Residents.Resident)))
            {
                BsonClassMap.RegisterClassMap<Domain.Residents.Resident>(cm =>
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

                BsonClassMap.RegisterClassMap<ResidentRegisteredDomainEvent>(cm =>
                {
                    cm.MapProperty(e => e.ResidentId);
                    cm.MapProperty(e => e.FullName);
                    cm.MapProperty(e => e.Cpf);
                    cm.MapProperty(e => e.Email);
                    cm.MapProperty(e => e.MobilePhone);
                    cm.MapProperty(e => e.UnitId);
                    cm.MapProperty(e => e.ResidentType);
                    cm.MapProperty(e => e.NotificationChannel);
                    cm.MapCreator(e => new ResidentRegisteredDomainEvent(
                        e.ResidentId,
                        e.FullName,
                        e.Cpf,
                        e.Email,
                        e.MobilePhone,
                        e.UnitId,
                        e.ResidentType,
                        e.NotificationChannel));
                });

                BsonClassMap.RegisterClassMap<ResidentUpdatedDomainEvent>(cm =>
                {
                    cm.MapProperty(e => e.ResidentId);
                    cm.MapProperty(e => e.FullName);
                    cm.MapProperty(e => e.Email);
                    cm.MapProperty(e => e.MobilePhone);
                    cm.MapProperty(e => e.UnitId);
                    cm.MapProperty(e => e.ResidentType);
                    cm.MapProperty(e => e.NotificationChannel);
                    cm.MapProperty(e => e.UpdatedAtUtc);
                    cm.MapCreator(e => new ResidentUpdatedDomainEvent(
                        e.ResidentId,
                        e.FullName,
                        e.Email,
                        e.MobilePhone,
                        e.UnitId,
                        e.ResidentType,
                        e.NotificationChannel,
                        e.UpdatedAtUtc));
                });

                BsonClassMap.RegisterClassMap<ResidentInactivatedDomainEvent>(cm =>
                {
                    cm.MapProperty(e => e.ResidentId);
                    cm.MapProperty(e => e.FullName);
                    cm.MapProperty(e => e.Email);
                    cm.MapProperty(e => e.MobilePhone);
                    cm.MapProperty(e => e.UnitId);
                    cm.MapProperty(e => e.Reason);
                    cm.MapProperty(e => e.InactivatedAtUtc);
                    cm.MapProperty(e => e.NotificationChannel);
                    cm.MapCreator(e => new ResidentInactivatedDomainEvent(
                        e.ResidentId,
                        e.FullName,
                        e.Email,
                        e.MobilePhone,
                        e.UnitId,
                        e.Reason,
                        e.InactivatedAtUtc,
                        e.NotificationChannel));
                });
            }

            _configured = true;
        }
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

    private sealed class CpfSerializer : SerializerBase<Cpf>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Cpf value)
            => context.Writer.WriteString(value.Value);

        public override Cpf Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Cpf.From(context.Reader.ReadString());
    }

    private sealed class PhoneNumberSerializer : SerializerBase<PhoneNumber>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PhoneNumber value)
            => context.Writer.WriteString(value.Value);

        public override PhoneNumber Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => PhoneNumber.From(context.Reader.ReadString());
    }

    private sealed class EmailSerializer : SerializerBase<Email>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Email value)
            => context.Writer.WriteString(value.Value);

        public override Email Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Email.From(context.Reader.ReadString());
    }
}
