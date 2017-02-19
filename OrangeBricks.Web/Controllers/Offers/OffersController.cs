using System.Web.Mvc;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Offers.Builders;
using OrangeBricks.Web.Controllers.Offers.Commands;
using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers
{
    [OrangeBricksAuthorize(Roles = "Seller")]
    public class OffersController : Controller
    {
        private readonly IOrangeBricksContext _context;
        private readonly ICommandSender _commandSender;

        public OffersController( IOrangeBricksContext context, ICommandSender sender )
        {
            _context = context;
            _commandSender = sender;
        }

        public ActionResult OnProperty(int id)
        {
            var builder = new OffersOnPropertyViewModelBuilder(_context);
            var viewModel = builder.Build(id);

            return View(viewModel);
        }

        [HttpPost]        
        public ActionResult Accept(AcceptOfferCommand command)
        {
            _commandSender.Send(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }

        [HttpPost]
        public ActionResult Reject(RejectOfferCommand command)
        {
            _commandSender.Send(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }
    }
}