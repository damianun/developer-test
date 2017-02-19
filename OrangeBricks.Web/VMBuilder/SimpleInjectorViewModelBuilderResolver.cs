using System;
using OrangeBricks.Web.VMBuilder.Interfaces;
using SimpleInjector;

namespace OrangeBricks.Web.VMBuilder
{
    public class SimpleInjectorViewModelBuilderResolver : IViewModelBuilderResolver
    {
        private readonly Container _container;

        public SimpleInjectorViewModelBuilderResolver(Container container)
        {
            _container = container;
        }

        public TViewModelBuilder ResolveViewModelBuilder<TViewModelBuilder>() where TViewModelBuilder : class, IViewModelBuilder
        {
            var builder = _container.GetInstance<TViewModelBuilder>();

            if (builder == null)
            {
                throw new InvalidOperationException($"No view model builder was found for type {typeof(TViewModelBuilder)}");
            }

            return builder;
        }
    }
}