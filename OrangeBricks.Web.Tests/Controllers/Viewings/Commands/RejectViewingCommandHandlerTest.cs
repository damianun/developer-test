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
    public class RejectViewingCommandHandlerTest
    {

        private RejectViewingCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Viewing> _viewings;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _viewings = Substitute.For<IDbSet<Viewing>>();
            _context.Viewings.Returns(_viewings);
            _handler = new RejectViewingCommandHandler(_context);
        }

        [Test]
        public void HandlerShouldRejectViewing()
        {
            var command = new RejectViewingCommand { ViewingId = 1 };

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
            Assert.IsTrue(viewing.Status == ViewingStatus.Rejected, "Viewing has not been rejected");
        }

        [Test]
        public void HandlerShouldSetUpdatedAtAttributeOnViewing()
        {
            var command = new RejectViewingCommand { ViewingId = 1, PropertyId = 1 };

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
            Assert.AreNotEqual(DateTime.MaxValue, viewing.UpdatedAt, "Uupdated date has not been set on Viewing");
        }
    }
}
