namespace OrangeBricks.Web.VMBuilder.Interfaces
{
    public interface IViewModelFactory
    {
        TViewModel BuildViewModel<TViewModel, TInput>(TInput input) where TViewModel : class;
    }
}
