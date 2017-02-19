using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class CreatePropertyCommandHandler : ICommandHandler<CreatePropertyCommand>
    {
        private readonly IOrangeBricksContext _context;

        public CreatePropertyCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(CreatePropertyCommand command)
        {
            var property = new Models.Property
            {
                PropertyType = command.PropertyType,
                StreetName = command.StreetName,
                Description = command.Description,
                NumberOfBedrooms = command.NumberOfBedrooms,
                SellerUserId = command.SellerUserId
            };


            _context.Properties.Add(property);

            _context.SaveChanges();
        }
    }
}