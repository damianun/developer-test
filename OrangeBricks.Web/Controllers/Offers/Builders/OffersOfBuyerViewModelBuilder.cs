using System;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.VMBuilder.Interfaces;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class OffersOfBuyerViewModelBuilder : IViewModelBuilder<OffersOfBuyerViewModel, string>
    {
        private readonly IOrangeBricksContext _context;

        public OffersOfBuyerViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public OffersOfBuyerViewModel Build(string userid)
        {
            throw new NotImplementedException();
        }
    }
}