namespace CronometerTask.Domain.Common
{
    /// <summary>
    /// Aggregate root class for domain driven design.
    /// </summary>
    public abstract class AggregateRoot(Guid id) : Entity(id)
    {
    }
}
