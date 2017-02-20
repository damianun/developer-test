using System;
using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Viewings.Commands;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Tests.Controllers.Viewings.Commands
{
    [TestFixture]
    public class AcceptViewingCommandHandlerTest
    {

        private AcceptViewingCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Viewing> _viewings;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _viewings = Substitute.For<IDbSet<Viewing>>();
            _context.Viewings.Returns(_viewings);
            _handler = new AcceptViewingCommandHandler(_context);
        }

        [Test]
        public void HandlerShouldAcceptViewing()
        {
            var command = new AcceptViewingCommand { ViewingId = 1 };

            var viewing = new Viewing() { Status = ViewingStatus.Pending };

            _viewings.Find(1).Returns(viewing);

            _handler.Handle(command);

            Assert.IsTrue(viewing.Status == ViewingStatus.Accepted, "Viewing has not been accepted");
        }

        [Test]
        public void HandlerShouldSetUpdatedAtAttributeOnViewing()
        {
            var command = new AcceptViewingCommand { ViewingId = 1 };

            var viewing = new Viewing { Status = ViewingStatus.Pending, UpdatedAt = DateTime.MaxValue };

            _viewings.Find(1).Returns(viewing);

            _handler.Handle(command);

            Assert.AreNotEqual(DateTime.MaxValue, viewing.UpdatedAt, "Updated date has not been set on Viewing");
        }
    }
}
