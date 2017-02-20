using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Tests.Utilities.DbSet;

namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
{
    [TestFixture]
    public class RequestViewingViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuildShouldReturnDefaultViewing()
        {
            var builder = new RequestViewingViewModelBuilder(_context);

            var properties = new List<Models.Property>{
                new Models.Property{ Id = 1, StreetName = "Smith Street", Description = "", IsListedForSale = true },
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var viewModel = builder.Build(1);

            Assert.IsNotNull(viewModel,"Builder returned no default view model");
        }
    }
}
