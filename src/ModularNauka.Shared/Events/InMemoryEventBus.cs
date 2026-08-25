namespace ModularNauka.Shared.Events;

// Prosty event bus działający w pamięci procesu — idealny dla monolitu.
// Handlery rejestruje się raz przy starcie aplikacji (w Program.cs).
public sealed class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<object>> _handlers = new();

    public void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
    {
        var type = typeof(TEvent);
        if (!_handlers.TryGetValue(type, out var list))
        {
            list = new List<object>();
            _handlers[type] = list;
        }
        list.Add(handler);
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IEvent
    {
        if (!_handlers.TryGetValue(typeof(TEvent), out var handlers))
            return;

        foreach (var handler in handlers)
        {
            await ((IEventHandler<TEvent>)handler).HandleAsync(@event, ct);
        }
    }
}
