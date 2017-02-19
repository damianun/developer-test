using System;
using OrangeBricks.Web.VMBuilder.Interfaces;

namespace OrangeBricks.Web.VMBuilder
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IViewModelBuilderResolver _viewModelBuilderResolver;

        public ViewModelFactory(IViewModelBuilderResolver viewModelBuilderResolver)
        {
            _viewModelBuilderResolver = viewModelBuilderResolver;
        }

        public TViewModel BuildViewModel<TViewModel, TInput>(TInput input) where TViewModel : class
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var builder = _viewModelBuilderResolver.ResolveViewModelBuilder<IViewModelBuilder<TViewModel, TInput>>();
            return builder.Build(input);
        }
    }
}