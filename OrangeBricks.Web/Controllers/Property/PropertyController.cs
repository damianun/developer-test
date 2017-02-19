using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property
{
    public class PropertyController : Controller
    {
        private readonly IOrangeBricksContext _context;
        private readonly ICommandSender _commandSender;

        public PropertyController( IOrangeBricksContext context, ICommandSender commandSender)
        {
            _context = context;
            _commandSender = commandSender;
        }

        [Authorize]
        public ActionResult Index(PropertiesQuery query)
        {
            var builder = new PropertiesViewModelBuilder(_context);
            var viewModel = builder.Build(query);

            return View(viewModel);
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Create()
        {
            var viewModel = new CreatePropertyViewModel
            {
                PossiblePropertyTypes = new[] {"House", "Flat", "Bungalow"}
                    .Select(x => new SelectListItem {Value = x, Text = x})
                    .AsEnumerable()
            };


            return View(viewModel);
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        [HttpPost]
        public ActionResult Create(CreatePropertyCommand command)
        {
            command.SellerUserId = User.Identity.GetUserId();

            _commandSender.Send(command);

            return RedirectToAction("MyProperties");
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult MyProperties()
        {
            var builder = new MyPropertiesViewModelBuilder(_context);
            var viewModel = builder.Build(User.Identity.GetUserId());

            return View(viewModel);
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult ListForSale(ListPropertyCommand command)
        {
            _commandSender.Send(command);
                        
            return RedirectToAction("MyProperties");
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MakeOffer(int id)
        {
            var builder = new MakeOfferViewModelBuilder(_context);
            var viewModel = builder.Build(id);
            return View(viewModel);
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MakeOffer(MakeOfferCommand command)
        {
            _commandSender.Send(command);

            return RedirectToAction("Index");
        }
    }
}