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
    public class OffersOnPropertyViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuildShouldReturnNoOffersOnProperty()
        {

            var builder = new OffersOnPropertyViewModelBuilder(_context);

            var properties = new List<Models.Property> { 
                new Models.Property{ Id = 1, StreetName = "", Description = "Great location", IsListedForSale = true }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.IsEmpty(result.Offers, "There should be no offers on property");
            
        }

        [Test]
        public void BuildShouldReturnOffersOnProperty()
        {

            var builder = new OffersOnPropertyViewModelBuilder(_context);

            var properties = new List<Models.Property> {
                new Models.Property{ Id = 1, StreetName = "", Description = "Great location", IsListedForSale = true,
                    Offers = new List<Offer>
                    {
                        new Offer { Id = 1, Amount = 15000 },
                        new Offer { Id = 2, Amount = 800 }
                    }
                }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.AreEqual(2, result.Offers.Count(), "Unexpected number of offers on property returned");
        }

        [Test]
        public void BuildShouldReturnOfferOnPropertyWithCorrectAmount()
        {

            var builder = new OffersOnPropertyViewModelBuilder(_context);

            var properties = new List<Models.Property> {
                new Models.Property{ Id = 1, StreetName = "", Description = "Great location", IsListedForSale = true,
                    Offers = new List<Offer>
                    {
                        new Offer { Id = 1, Amount = 15000 }
                    }
                }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.IsTrue(result.Offers.SingleOrDefault(x => x.Amount == 15000) != null, "An offer with required amount is missing");
        }

        [Test]
        public void BuildShouldReturnOfferOnPropertyWithPendingStatus()
        {

            var builder = new OffersOnPropertyViewModelBuilder(_context);

            var properties = new List<Models.Property> {
                new Models.Property{ Id = 1, StreetName = "", Description = "Great location", IsListedForSale = true,
                    Offers = new List<Offer>
                    {
                        new Offer { Id = 1, Amount = 15000, Status = OfferStatus.Pending }
                    }
                }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.IsTrue(result.Offers.SingleOrDefault(x => x.IsPending) != null, "An offer with Pending status is missing");
        }

        [Test]
        public void BuildShouldReturnOfferOnPropertyWithCorrectStreetName()
        {

            var builder = new OffersOnPropertyViewModelBuilder(_context);

            var properties = new List<Models.Property> {
                new Models.Property{ Id = 1, StreetName = "Green Lane", Description = "Great location", IsListedForSale = true,
                    Offers = new List<Offer>
                    {
                        new Offer { Id = 1, Amount = 15000, Status = OfferStatus.Pending }
                    }
                }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.AreEqual(result.StreetName, "Green Lane", "An offer with required Street Name is missing");
        }
    }
}
