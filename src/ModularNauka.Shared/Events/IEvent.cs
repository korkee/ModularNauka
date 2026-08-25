namespace ModularNauka.Shared.Events;

public interface IEvent
{
    Guid Id { get; }
    DateTime OccurredAt { get; }
}
