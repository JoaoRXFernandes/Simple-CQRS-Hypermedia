namespace Infrastructure.CQRS.Commands.Handlers
{
    public interface ICommandHandler<T> where T : Command
    {
        void Handle(T command);
    }
}