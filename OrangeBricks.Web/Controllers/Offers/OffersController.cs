using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Offers.Commands;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.VMBuilder.Interfaces;

namespace OrangeBricks.Web.Controllers.Offers
{
    public class OffersController : Controller
    {
        private readonly ICommandSender _commandSender;
        private readonly IViewModelFactory _viewModelFactory;

        public OffersController( ICommandSender sender, IViewModelFactory viewModelFactory )
        {
            _commandSender = sender;
            _viewModelFactory = viewModelFactory;
        }

        public ActionResult OnProperty(int id)
        {
            return View(_viewModelFactory.BuildViewModel<OffersOnPropertyViewModel, int>(id));
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        [ValidateAntiForgeryToken]
        public ActionResult Accept(AcceptOfferCommand command)
        {
            _commandSender.Send(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        [ValidateAntiForgeryToken]
        public ActionResult Reject(RejectOfferCommand command)
        {
            _commandSender.Send(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult OfBuyer()
        {
            return View(_viewModelFactory.BuildViewModel<OffersOfBuyerViewModel, string>(User.Identity.GetUserId()));
        }
    }
}