namespace OrangeBricks.Web.Cqrs.Interfaces
{
    public interface ICommandSender
    {
        void Send<TCommand>(TCommand command) where TCommand : class;
    }
}
