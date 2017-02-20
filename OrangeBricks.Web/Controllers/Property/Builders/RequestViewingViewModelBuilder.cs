using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.VMBuilder.Interfaces;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class RequestViewingViewModelBuilder : IViewModelBuilder<RequestViewingViewModel, int>
    {
        private readonly IOrangeBricksContext _context;

        public RequestViewingViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public RequestViewingViewModel Build(int propertyId)
        {
            throw new System.NotImplementedException();
        }
    }
}