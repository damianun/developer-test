﻿using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Cqrs.Interfaces;
using OrangeBricks.Web.VMBuilder.Interfaces;

namespace OrangeBricks.Web.Controllers.Property
{
    public class PropertyController : Controller
    {
        private readonly ICommandSender _commandSender;
        private readonly IViewModelFactory _viewModelFactory;

        public PropertyController( ICommandSender commandSender, IViewModelFactory viewModelFactory )
        {
            _commandSender = commandSender;
            _viewModelFactory = viewModelFactory;
        }

        [Authorize]
        public ActionResult Index(PropertiesQuery query)
        {
            return View(_viewModelFactory.BuildViewModel<PropertiesViewModel, PropertiesQuery>(query));
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

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePropertyCommand command)
        {
            if (ModelState.IsValid)
            {
                command.SellerUserId = User.Identity.GetUserId();

                _commandSender.Send(command);

                return RedirectToAction("MyProperties");
            }
            else
            {
                return Create();
            }
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult MyProperties()
        {
            return View(_viewModelFactory.BuildViewModel<MyPropertiesViewModel, string>(User.Identity.GetUserId()));
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        [ValidateAntiForgeryToken]
        public ActionResult ListForSale(ListPropertyCommand command)
        {
            _commandSender.Send(command);
                        
            return RedirectToAction("MyProperties");
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MakeOffer(int id)
        {
            return View(_viewModelFactory.BuildViewModel<MakeOfferViewModel,int>(id));
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        [ValidateAntiForgeryToken]
        public ActionResult MakeOffer(MakeOfferCommand command)
        {
            _commandSender.Send(command);

            return RedirectToAction("Index");
        }
    }
}