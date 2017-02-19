namespace OrangeBricks.Web.Cqrs.Interfaces
{
    public interface ICommandHandler
    {        
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : class 
    {
        void Handle(TCommand command);
    }
}
