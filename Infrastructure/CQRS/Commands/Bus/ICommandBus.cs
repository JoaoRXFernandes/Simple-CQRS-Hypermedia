namespace Infrastructure.CQRS.Commands.Bus
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : Command;
    }
}