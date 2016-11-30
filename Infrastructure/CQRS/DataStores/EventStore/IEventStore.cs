using System;
using System.Collections.Generic;
using Infrastructure.CQRS.Events;

namespace Infrastructure.CQRS.EventStore
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
    }
}