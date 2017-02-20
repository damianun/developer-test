using System;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewings.Commands
{
    public class AcceptViewingCommandHandler : ICommandHandler<AcceptViewingCommand>
    {
        private readonly IOrangeBricksContext _context;

        public AcceptViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(AcceptViewingCommand command)
        {
            var viewing = _context.Viewings
                .Where(v => v.Id == command.ViewingId)
                .Include(v => v.Property)
                .SingleOrDefault();

            viewing.UpdatedAt = DateTime.Now;
            viewing.Status = ViewingStatus.Accepted;

            _context.SaveChanges();
        }
    }
}