using System.Linq;
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

        public OffersOfBuyerViewModel Build(string buyerUserId)
        {

            var buyersOffers = _context.Offers
                .Where(p => p.BuyerUserId == buyerUserId)
                .OrderBy(p => p.Property.Id)
                .ThenBy(p => p.CreatedAt)
                .Select(x => new OfferOnPropertOfBuyerViewModel()
                {
                    Amount = x.Amount,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status.ToString(),
                    StreetName = x.Property.StreetName,
                    PropertyType = x.Property.PropertyType,
                    PropertyId = x.Property.Id,
                    Accepted = x.Status == OfferStatus.Accepted
                });

            return new OffersOfBuyerViewModel
            {
                Offers = buyersOffers.ToList(),
                HasOffers = buyersOffers.Any()
            };
        }
    }
}