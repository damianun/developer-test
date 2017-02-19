namespace OrangeBricks.Web.Cqrs.Interfaces
{
    public interface ICommandHandlerResolver
    {
        TCommandHandler ResolveCommandHandler<TCommandHandler>() where TCommandHandler : class, ICommandHandler;
    }
}
