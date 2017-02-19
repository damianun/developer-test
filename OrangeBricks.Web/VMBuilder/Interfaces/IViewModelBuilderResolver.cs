namespace OrangeBricks.Web.VMBuilder.Interfaces
{
    public interface IViewModelBuilderResolver
    {
        TViewModelBuilder ResolveViewModelBuilder<TViewModelBuilder>() where TViewModelBuilder : class, IViewModelBuilder;
    }
}
