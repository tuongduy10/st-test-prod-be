namespace WebServer.Domain.Common;

public abstract class AuditableEntity<TId>
    : AggregateRoot<TId>
{
    public DateTime CreatedAt { get; protected set; }

    public DateTime UpdatedAt { get; protected set; }

    public bool IsDeleted { get; protected set; }
}
