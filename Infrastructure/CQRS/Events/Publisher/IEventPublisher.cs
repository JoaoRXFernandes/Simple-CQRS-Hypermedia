namespace Infrastructure.CQRS.Events.Publisher
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }
}