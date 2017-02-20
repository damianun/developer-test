using System.Linq;
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

            var buyersViewings = _context.Viewings
                .Where(p => p.VisitorUserId == buyerUserId)
                .OrderBy(p => p.Property.Id)
                .ThenBy(p => p.VisitAt)
                .Select(x => new ViewingOnPropertOfBuyerViewModel()
                {
                    VisitDateTime = x.VisitAt,
                    Status = x.Status.ToString(),
                    StreetName = x.Property.StreetName,
                    PropertyType = x.Property.PropertyType,
                    PropertyId = x.Property.Id,
                    Accepted = x.Status == ViewingStatus.Accepted
                });

            return new ViewingsOfBuyerViewModel
            {
                Viewings = buyersViewings.ToList(),
                HasViewings = buyersViewings.Any()
            };
        }
    }
}