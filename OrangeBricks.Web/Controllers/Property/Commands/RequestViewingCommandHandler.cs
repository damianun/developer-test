using System;
using System.Collections.Generic;
using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class RequestViewingCommandHandler : ICommandHandler<RequestViewingCommand>
    {
        private readonly IOrangeBricksContext _context;

        public RequestViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(RequestViewingCommand command)
        {
            var property = _context.Properties.Find(command.PropertyId);

            var viewing = new Viewing()
            {                
                VisitAt = command.VisitDateTime,
                Status = ViewingStatus.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                VisitorUserId = command.VisitorUserId
            };

            if (property.Viewings == null)
            {
                property.Viewings = new List<Viewing>();
            }

            property.Viewings.Add(viewing);

            _context.SaveChanges();
        }
    }
}