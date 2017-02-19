namespace OrangeBricks.Web.VMBuilder.Interfaces
{
    public interface IViewModelBuilder
    {
    }

    public interface IViewModelBuilder<out TViewModel, in TInput> : IViewModelBuilder where TViewModel : class 
    {
        TViewModel Build(TInput input);
    }
}
