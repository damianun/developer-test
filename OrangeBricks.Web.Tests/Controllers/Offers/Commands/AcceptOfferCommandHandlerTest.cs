using System;
using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Offers.Commands;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Tests.Controllers.Offers.Commands
{
    [TestFixture]
    public class AcceptOfferCommandHandlerTest
    {
        private AcceptOfferCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Offer> _offers;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _offers = Substitute.For<IDbSet<Offer>>();
            _context.Offers.Returns(_offers);
            _handler = new AcceptOfferCommandHandler(_context);
        }

        [Test]
        public void HandlerShouldAcceptOffer()
        {
            var command = new AcceptOfferCommand { OfferId = 1 };
            
            var offer = new Offer {Status = OfferStatus.Pending};

            _offers.Find(1).Returns(offer);

            _handler.Handle(command);

            Assert.IsTrue(offer.Status == OfferStatus.Accepted, "An offer has not been accepted");
        }

        [Test]
        public void HandlerShouldSetUpdatedAtAttribute()
        {
            var command = new AcceptOfferCommand { OfferId = 1 };

            var offer = new Offer { Status = OfferStatus.Pending, UpdatedAt = DateTime.MaxValue };

            _offers.Find(1).Returns(offer);

            _handler.Handle(command);

            Assert.AreNotEqual(DateTime.MaxValue, offer.UpdatedAt, "An offer updated date has not been set");
        }
    }
}
