using System;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using Assert = NUnit.Framework.Assert;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class MakeOfferCommandHandlerTest
    {
        private MakeOfferCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);
            _handler = new MakeOfferCommandHandler(_context);
        }

        [Test]
        public void HandlerShouldCreateAnOffer()
        {
            var command = new MakeOfferCommand { Offer = 9000, PropertyId = 1 };

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Offers.Count >0, "No offers were made for the property");
        }

        [Test]
        public void HandlerShouldCreateAnOfferWithCorrectAmount()
        {
            var command = new MakeOfferCommand { Offer = 9000, PropertyId = 1 };

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Offers.FirstOrDefault(x => x.Amount == command.Offer) != null, "An offer with required amount is missing");
        }

        [Test]
        public void HandlerShouldCreateAnOfferWithPendingStatus()
        {
            var command = new MakeOfferCommand { Offer = 9000, PropertyId = 1 };

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Offers.FirstOrDefault(x => x.Status == OfferStatus.Pending) != null, "An offer with pending status is missing");
        }

        [Test]
        public void HandlerShouldCreateAnOfferWithDatesSet()
        {
            var command = new MakeOfferCommand { Offer = 9000, PropertyId = 1 };

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Offers.FirstOrDefault(x => x.UpdatedAt != default(DateTime)) != null, "An offer with UpdateAt date is missing");
            Assert.IsTrue(property.Offers.FirstOrDefault(x => x.CreatedAt != default(DateTime)) != null, "An offer with CreateAt date is missing");
        }

        [Test]
        public void HandlerShouldCreateAnOfferWithBuyerReferenceSet()
        {
            var command = new MakeOfferCommand { Offer = 9000, PropertyId = 1, BuyerUserId = "ID"};

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Offers.FirstOrDefault(x => !string.IsNullOrEmpty(x.BuyerUserId)) != null, "Buyer reference is missing on offer");
        }
    }
}
