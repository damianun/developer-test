using System;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.VMBuilder.Interfaces;

namespace OrangeBricks.Web.Controllers.Viewings.Builders
{
    public class ViewingsOfBuyerViewModelBuilder : IViewModelBuilder<ViewingsOfBuyerViewModel, string>
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsOfBuyerViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ViewingsOfBuyerViewModel Build(string buyerUserId)
        {
            throw new NotImplementedException();
        }
    }
}