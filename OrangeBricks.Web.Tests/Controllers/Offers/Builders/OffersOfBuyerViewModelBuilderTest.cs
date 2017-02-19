using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Offers.Builders;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Tests.Utilities.DbSet;

namespace OrangeBricks.Web.Tests.Controllers.Offers.Builders
{
    [TestFixture]
    public class OffersOfBuyerViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuilderShouldReturnOnlyOffersThatBelongToBuyer()
        {
            var builder = new OffersOfBuyerViewModelBuilder(_context);

            var offers = new List<Offer>
            {
                new Offer {Amount = 9000, BuyerUserId = "1", Status = OfferStatus.Pending},
                new Offer {Amount = 9001, BuyerUserId = "2", Status = OfferStatus.Pending},
                new Offer {Amount = 9003, BuyerUserId = "3", Status = OfferStatus.Pending},
                new Offer {Amount = 9005, BuyerUserId = "1", Status = OfferStatus.Rejected},
                new Offer {Amount = 9004, BuyerUserId = "1", Status = OfferStatus.Pending}
            };

            var mockSet = Substitute.For<IDbSet<Offer>>()
                .Initialize(offers.AsQueryable());

            _context.Offers.Returns(mockSet);

            var result = builder.Build("1");

            Assert.That(result.Offers.Count, Is.EqualTo(3), "Incorrect number of offers returned by builder");
        }

        [Test]
        public void BuilderShouldReturnNoOffersIfThereAreNoOffersMadeByVuyer()
        {
            var builder = new OffersOfBuyerViewModelBuilder(_context);

            var offers = new List<Offer>
            {
                new Offer {Amount = 9000, BuyerUserId = "4", Status = OfferStatus.Pending},
                new Offer {Amount = 9001, BuyerUserId = "2", Status = OfferStatus.Pending},
                new Offer {Amount = 9003, BuyerUserId = "3", Status = OfferStatus.Pending}
            };

            var mockSet = Substitute.For<IDbSet<Offer>>()
                .Initialize(offers.AsQueryable());

            _context.Offers.Returns(mockSet);

            var result = builder.Build("1");

            Assert.That(result.Offers.Count, Is.EqualTo(0), "Builder should return no offers");
        }
    }
}
