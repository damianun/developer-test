using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Viewings.Commands;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;
using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.VMBuilder.Interfaces;

namespace OrangeBricks.Web.Controllers.Viewings
{
    public class ViewingsController : Controller
    {
        private readonly ICommandSender _commandSender;
        private readonly IViewModelFactory _viewModelFactory;

        public ViewingsController(ICommandSender sender, IViewModelFactory viewModelFactory)
        {
            _commandSender = sender;
            _viewModelFactory = viewModelFactory;
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult OnProperty(int id)
        {
            return View(_viewModelFactory.BuildViewModel<ViewingsOnPropertyViewModel, int>(id));
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        [ValidateAntiForgeryToken]
        public ActionResult Accept(AcceptViewingCommand command)
        {
            _commandSender.Send(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        [ValidateAntiForgeryToken]
        public ActionResult Reject(RejectViewingCommand command)
        {
            _commandSender.Send(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult OfBuyer()
        {
            return View(_viewModelFactory.BuildViewModel<ViewingsOfBuyerViewModel, string>(User.Identity.GetUserId()));
        }
    }
}