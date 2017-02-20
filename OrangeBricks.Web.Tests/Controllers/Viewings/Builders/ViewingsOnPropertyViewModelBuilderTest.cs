using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Viewings.Builders;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Tests.Utilities.DbSet;

namespace OrangeBricks.Web.Tests.Controllers.Viewings.Builders
{
    [TestFixture]
    public class ViewingsOnPropertyViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuildShouldReturnNoViewingsOnProperty()
        {

            var builder = new ViewingsOnPropertyViewModelBuilder(_context);

            var properties = new List<Models.Property> {
                new Models.Property{ Id = 1, StreetName = "", Description = "Great location", IsListedForSale = true }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.IsEmpty(result.Viewings, "There should be no viewings on property");

        }

        [Test]
        public void BuildShouldReturnViewingsOnProperty()
        {

            var builder = new ViewingsOnPropertyViewModelBuilder(_context);

            var properties = new List<Models.Property> {
                new Models.Property{ Id = 1, StreetName = "", Description = "Great location", IsListedForSale = true,
                    Viewings = new List<Viewing>
                    {
                        new Viewing { Id = 1, VisitAt = DateTime.Now },
                        new Viewing { Id = 2, VisitAt = DateTime.Now }
                    }
                }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.AreEqual(2, result.Viewings.Count(), "Unexpected number of viewings on property returned");
        }

        [Test]
        public void BuildShouldReturnViewingOnPropertyWithCorrectVisitDate()
        {

            var builder = new ViewingsOnPropertyViewModelBuilder(_context);
            var date = DateTime.ParseExact("12/11/2017 12:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            var properties = new List<Models.Property> {
                new Models.Property{ Id = 1, StreetName = "", Description = "Great location", IsListedForSale = true,
                    Viewings = new List<Viewing>
                    {
                        new Viewing { Id = 1, VisitAt = date }
                    }
                }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.IsTrue(result.Viewings.Any(x => x.VisitDateTime == date), "Viewing with required visit date is missing");
        }

        [Test]
        public void BuildShouldReturnViewingOnPropertyWithPendingStatus()
        {

            var builder = new ViewingsOnPropertyViewModelBuilder(_context);

            var properties = new List<Models.Property> {
                new Models.Property{ Id = 1, StreetName = "", Description = "Great location", IsListedForSale = true,
                    Viewings = new List<Viewing>
                    {
                        new Viewing { Id = 1, VisitAt = DateTime.Now, Status = ViewingStatus.Pending }
                    }
                }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.IsTrue(result.Viewings.Any(x => x.IsPending), "Viewing with Pending status is missing");
        }

        [Test]
        public void BuildShouldReturnViewingOnPropertyWithCorrectStreetName()
        {

            var builder = new ViewingsOnPropertyViewModelBuilder(_context);

            var properties = new List<Models.Property> {
                new Models.Property{ Id = 1, StreetName = "Green Lane", Description = "Great location", IsListedForSale = true,
                    Viewings = new List<Viewing>
                    {
                        new Viewing() { Id = 1, VisitAt = DateTime.Now, Status = ViewingStatus.Pending }
                    }
                }
            };

            var propertiesMockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(propertiesMockSet);

            var result = builder.Build(1);

            Assert.AreEqual(result.StreetName, "Green Lane", "Viewing with required Street Name is missing");
        }
    }
}
