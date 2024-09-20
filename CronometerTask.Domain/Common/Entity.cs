namespace CronometerTask.Domain.Common
{
    /// <summary>
    /// Entity class for domain driven design. In case we would need to persist our objects in the future.
    /// </summary>
    public abstract class Entity(Guid id) : IEquatable<Entity>
    {
        public Guid Id { get; private init; } = id;

        public static bool operator ==(Entity? a, Entity? b)
        {
            return a is not null && b is not null && a.Equals(b);
        }

        public static bool operator !=(Entity? a, Entity? b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj.GetType() != GetType()) return false;

            if (obj is not Entity entity) return false;

            return entity.Id == Id;
        }

        public bool Equals(Entity? other)
        {
            if (other is null) return false;

            if (other.GetType() != GetType()) return false;

            return other.Equals(this);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
