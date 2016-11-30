using System;
using System.Collections.Generic;
using System.Threading;
using Infrastructure.CQRS.Events;
using Infrastructure.CQRS.Events.Publisher;
using Infrastructure.CQRS.Events.Subscribers;

namespace Infrastructure.InMemory.Events.Publisher
{
    public class InMemoryEventPublisher : IEventPublisher
    {
        private readonly Func<Type, IEnumerable<object>> _subscribersResolver;

        public InMemoryEventPublisher(Func<Type, IEnumerable<object>> subscribersResolver)
        {
            _subscribersResolver = subscribersResolver;
        }

        public void Publish<T>(T @event) where T : Event
        {
            dynamic subscribers = _subscribersResolver.Invoke(typeof(ISubscriber<>).MakeGenericType(@event.GetType()));
            foreach (var handler in subscribers)
            {
                ThreadPool.QueueUserWorkItem(x => handler.Handle((dynamic)@event));
            }
        }
    }
}
