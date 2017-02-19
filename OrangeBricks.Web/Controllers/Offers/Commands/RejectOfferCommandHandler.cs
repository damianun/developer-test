using System;
using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class RejectOfferCommandHandler : ICommandHandler<RejectOfferCommand>
    {
        private readonly IOrangeBricksContext _context;

        public RejectOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(RejectOfferCommand command)
        {
            var offer = _context.Offers.Find(command.OfferId);

            offer.UpdatedAt = DateTime.Now;
            offer.Status = OfferStatus.Rejected;

            _context.SaveChanges();
        }
    }
}