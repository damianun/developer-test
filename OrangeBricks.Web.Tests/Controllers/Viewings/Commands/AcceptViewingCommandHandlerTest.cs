using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Viewings.Commands;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Tests.Utilities.DbSet;

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
            var viewings = new List<Viewing>
            {
                new Viewing
                {
                    Id = 1,
                    Status = ViewingStatus.Pending,
                    Property = new Models.Property { Id = 1 }
                }
            };

            var mockSet = Substitute.For<IDbSet<Viewing>>()
                .Initialize(viewings.AsQueryable());

            _context.Viewings.Returns(mockSet);

            _handler.Handle(command);

            var viewing = viewings.Find(v => v.Id == 1);
            Assert.IsTrue(viewing.Status == ViewingStatus.Accepted, "Viewing has not been accepted");
        }

        [Test]
        public void HandlerShouldSetUpdatedAtAttributeOnViewing()
        {
            var command = new AcceptViewingCommand { ViewingId = 1 };

            var viewings = new List<Viewing>
            {
                new Viewing
                {
                    Id = 1,
                    Status = ViewingStatus.Pending,
                    UpdatedAt = DateTime.MaxValue,
                    Property = new Models.Property { Id = 1 }
                }
            };

            var mockSet = Substitute.For<IDbSet<Viewing>>()
                .Initialize(viewings.AsQueryable());

            _context.Viewings.Returns(mockSet);

            _handler.Handle(command);

            var viewing = viewings.Find(v => v.Id == 1);
            Assert.AreNotEqual(DateTime.MaxValue, viewing.UpdatedAt, "Updated date has not been set on Viewing");
        }
    }
}
