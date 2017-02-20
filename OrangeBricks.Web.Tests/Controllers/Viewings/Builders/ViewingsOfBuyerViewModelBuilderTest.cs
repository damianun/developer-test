using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Viewings.Builders;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Tests.Utilities.DbSet;

namespace OrangeBricks.Web.Tests.Controllers.Viewings.Builders
{
    [TestFixture]
    public class ViewingsOfBuyerViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuilderShouldReturnOnlyViewingsRequestedByBuyer()
        {
            var builder = new ViewingsOfBuyerViewModelBuilder(_context);

            var viewings = new List<Viewing>
            {
                new Viewing {VisitorUserId = "1", Status = ViewingStatus.Pending, VisitAt = DateTime.Now, Property = new Models.Property { Id = 1 }},
                new Viewing {VisitorUserId = "2", Status = ViewingStatus.Pending, VisitAt = DateTime.Now, Property = new Models.Property { Id = 2 }},
                new Viewing {VisitorUserId = "3", Status = ViewingStatus.Pending, VisitAt = DateTime.Now, Property = new Models.Property { Id = 3 }},
                new Viewing {VisitorUserId = "1", Status = ViewingStatus.Pending, VisitAt = DateTime.Now, Property = new Models.Property { Id = 4 }},
                new Viewing {VisitorUserId = "1", Status = ViewingStatus.Pending, VisitAt = DateTime.Now, Property = new Models.Property { Id = 5 }}
            };

            var mockSet = Substitute.For<IDbSet<Viewing>>()
                .Initialize(viewings.AsQueryable());

            _context.Viewings.Returns(mockSet);

            var result = builder.Build("1");

            Assert.That(result.Viewings.Count, Is.EqualTo(3), "Incorrect number of viewings returned by builder");
        }

        [Test]
        public void BuilderShouldReturnNoViewingsIfThereAreNoViewingsRequestedByBuyer()
        {
            var builder = new ViewingsOfBuyerViewModelBuilder(_context);

            var viewings = new List<Viewing>
            {
                new Viewing {VisitorUserId = "4", Status = ViewingStatus.Pending, VisitAt = DateTime.Now, Property = new Models.Property { Id = 1 }},
                new Viewing {VisitorUserId = "2", Status = ViewingStatus.Pending, VisitAt = DateTime.Now, Property = new Models.Property { Id = 1 }},
                new Viewing {VisitorUserId = "3", Status = ViewingStatus.Pending, VisitAt = DateTime.Now, Property = new Models.Property { Id = 1 }}
            };

            var mockSet = Substitute.For<IDbSet<Viewing>>()
                .Initialize(viewings.AsQueryable());

            _context.Viewings.Returns(mockSet);

            var result = builder.Build("1");

            Assert.That(result.Viewings.Count, Is.EqualTo(0), "Builder should return no viewings");
        }
    }
}
