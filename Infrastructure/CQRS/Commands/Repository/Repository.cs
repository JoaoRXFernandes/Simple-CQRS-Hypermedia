using System;
using Infrastructure.CQRS.Domain;
using Infrastructure.CQRS.EventStore;

namespace Infrastructure.CQRS.Commands.Repository
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }

    public class Repository<T> : IRepository<T> where T: AggregateRoot, new()
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var aggregate = new T();
            aggregate.LoadFromHistory(_storage.GetEventsForAggregate(id));

            return aggregate;
        }
    }

}
