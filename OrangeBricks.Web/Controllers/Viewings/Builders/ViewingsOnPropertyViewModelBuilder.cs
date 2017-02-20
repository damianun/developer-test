using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.VMBuilder.Interfaces;

namespace OrangeBricks.Web.Controllers.Viewings.Builders
{
    public class ViewingsOnPropertyViewModelBuilder : IViewModelBuilder<ViewingsOnPropertyViewModel, int>
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsOnPropertyViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ViewingsOnPropertyViewModel Build(int id)
        {
            var property = _context.Properties
                .Where(p => p.Id == id)
                .Include(x => x.Viewings)
                .SingleOrDefault();

            var viewings = property.Viewings ?? new List<Viewing>();

            return new ViewingsOnPropertyViewModel
            {
                HasViewings = viewings.Any(),
                Viewings = viewings.Select(x => new ViewingsViewModel
                {
                    Id = x.Id,
                    VisitDateTime = x.VisitAt,
                    IsPending = x.Status == ViewingStatus.Pending,
                    Status = x.Status.ToString()
                }),
                PropertyId = property.Id,
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                NumberOfBedrooms = property.NumberOfBedrooms
            };
        }
    }
}