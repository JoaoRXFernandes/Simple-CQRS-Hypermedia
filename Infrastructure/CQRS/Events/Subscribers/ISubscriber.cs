namespace Infrastructure.CQRS.Events.Subscribers
{
    public interface ISubscriber<T> where T : Event
    {
        void Handle(T @event);
    }
}