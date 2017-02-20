using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class RequestViewingCommandHandlerTest
    {
        private RequestViewingCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);
            _handler = new RequestViewingCommandHandler(_context);
        }

        [Test]
        public void HandlerShouldCreateViewing()
        {
            var command = new RequestViewingCommand
            {
                PropertyId = 1,
                VisitDateTime = DateTime.ParseExact("12/11/2017 12:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                VisitorUserId = "ID"
            };

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Viewings.Count > 0, "No viewings were requested for the property");
        }

        [Test]
        public void HandlerShouldCreateViewingWithRequestedVisitDate()
        {
            var command = new RequestViewingCommand
            {
                PropertyId = 1,
                VisitDateTime = DateTime.ParseExact("12/11/2017 12:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                VisitorUserId = "ID"
            };

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Viewings.Any(x => x.VisitAt == command.VisitDateTime), "Viewing has different visitting date than requested");
        }

        [Test]
        public void HandlerShouldCreateViewingWithPendingStatus()
        {
            var command = new RequestViewingCommand
            {
                PropertyId = 1,
                VisitDateTime = DateTime.ParseExact("12/11/2017 12:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                VisitorUserId = "ID"
            };

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Viewings.Any(x => x.Status == ViewingStatus.Pending), "Viewing with pending status is missing");
        }

        [Test]
        public void HandlerShouldCreateAnOfferWithDatesSet()
        {
            var command = new RequestViewingCommand
            {
                PropertyId = 1,
                VisitDateTime = DateTime.ParseExact("12/11/2017 12:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                VisitorUserId = "ID"
            };

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Viewings.Any(x => x.UpdatedAt != default(DateTime)), "Viewing with UpdateAt date is missing");
            Assert.IsTrue(property.Viewings.Any(x => x.CreatedAt != default(DateTime)), "Viewing with CreateAt date is missing");
        }

        [Test]
        public void HandlerShouldCreateAnOfferWithBuyerReferenceSet()
        {
            var command = new RequestViewingCommand {
                PropertyId = 1,
                VisitDateTime = DateTime.ParseExact("12/11/2017 12:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                VisitorUserId = "ID"
            };

            var property = new Models.Property();

            _properties.Find(1).Returns(property);

            _handler.Handle(command);

            Assert.IsTrue(property.Viewings.Any(x => !string.IsNullOrEmpty(x.VisitorUserId)), "Buyer reference is missing on requested viewing");
        }

    }
}
