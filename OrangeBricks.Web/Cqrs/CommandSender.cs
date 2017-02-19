using System;
using OrangeBricks.Web.Cqrs.Interfaces;

namespace OrangeBricks.Web.Cqrs
{
    public class CommandSender : ICommandSender
    {
        private readonly ICommandHandlerResolver _commandHandlerResolver;

        public CommandSender(ICommandHandlerResolver commandHandlerResolver)
        {
            _commandHandlerResolver = commandHandlerResolver;
        }

        public void Send<TCommand>(TCommand command) where TCommand : class
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = _commandHandlerResolver.ResolveCommandHandler<ICommandHandler<TCommand>>();
            handler.Handle(command);
        }
    }
}