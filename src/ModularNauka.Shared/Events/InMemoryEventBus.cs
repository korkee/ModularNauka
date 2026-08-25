using Microsoft.Extensions.DependencyInjection;

namespace ModularNauka.Shared.Events;

public sealed class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Type>> _handlerTypes = new();
    private readonly IServiceScopeFactory _scopeFactory;

    public InMemoryEventBus(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        if (!_handlerTypes.TryGetValue(eventType, out var list))
        {
            list = new List<Type>();
            _handlerTypes[eventType] = list;
        }
        list.Add(handler.GetType());
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IEvent
    {
        if (!_handlerTypes.TryGetValue(typeof(TEvent), out var handlerTypes))
            return;

        using var scope = _scopeFactory.CreateScope();
        foreach (var handlerType in handlerTypes)
        {
            var handler = (IEventHandler<TEvent>)scope.ServiceProvider.GetRequiredService(handlerType);
            await handler.HandleAsync(@event, ct);
        }
    }
}
