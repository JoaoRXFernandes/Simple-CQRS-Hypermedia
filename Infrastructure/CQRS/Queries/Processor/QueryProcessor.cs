using System;
using Infrastructure.CQRS.Queries.Handlers;

namespace Infrastructure.CQRS.Queries.Processor
{
    public interface IQueryProcessor
    {
        TResult Process<TResult>(IQuery<TResult> query);
    }

    public class QueryProcessor : IQueryProcessor
    {
        private readonly Func<Type, object> _handlerResolver;

        public QueryProcessor(Func<Type, object> handlerResolver)
        {
            _handlerResolver = handlerResolver;
        }

        public TResult Process<TResult>(IQuery<TResult> query)
        {
            dynamic handler = _handlerResolver.Invoke(typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult)));
            return handler.Handle((dynamic)query);
        }
    }
}