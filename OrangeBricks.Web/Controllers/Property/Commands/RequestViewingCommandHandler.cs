using System;
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
            throw new NotImplementedException();
        }
    }
}