using System;
using OrangeBricks.Web.Cqrs.Interfaces;
using SimpleInjector;

namespace OrangeBricks.Web.Cqrs
{
    public class SimpleInjectorCommandHandlerResolver : ICommandHandlerResolver
    {
        private readonly Container _container;

        public SimpleInjectorCommandHandlerResolver(Container container)
        {
            _container = container;
        }
        public TCommandHandler ResolveCommandHandler<TCommandHandler>() where TCommandHandler : class, ICommandHandler
        {
            var handler = _container.GetInstance<TCommandHandler>();

            if (handler == null)
            {
                throw new InvalidOperationException($"No command handler was found for type {typeof(TCommandHandler)}");
            }

            return handler;
        }
    }
}