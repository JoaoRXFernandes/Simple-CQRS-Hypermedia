using System;
using Infrastructure.CQRS.Commands;
using Infrastructure.CQRS.Commands.Bus;
using Infrastructure.CQRS.Commands.Handlers;

namespace Infrastructure.InMemory.Commands.Bus
{
    public class InMemoryCommandBus : ICommandBus
    {
        private readonly Func<Type, object> _handlerResolver;

        public InMemoryCommandBus(Func<Type, object> handlerResolver)
        {
            _handlerResolver = handlerResolver;
        }

        public void Send<T>(T command) where T : Command
        {
            dynamic handler = _handlerResolver.Invoke(typeof(ICommandHandler<>).MakeGenericType(command.GetType()));
            handler.Handle((dynamic)command);
        }
    }
}
