using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Mica.Controllers;
using Mica.Models;
using Mica.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Moq;


namespace Mica.Tests.Controllers
{
    [TestFixture]
    public class BanksControllerTests
    {
        [Test]
        public void Create_Bank_AddsBankToContext()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Bank>>();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Banks).Returns(mockSet.Object);

            // Act
            var controller = new BanksController(mockContext.Object);
            controller.Create(new Bank { Id = 3, Name = "Westpac", CountryId = 1 });

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Bank>()), Times.Once);

        }
        [Test]
        public void Update_Bank_UpdatesBankDetails()
        {
            // Arrange
            List<Bank> banks = new List<Bank>()
            {
                new Bank() {Id = 1, Name = "ANZ", CountryId = 1}
            };

            var data = banks.AsQueryable();

            var mockSet = new Mock<DbSet<Bank>>();
            mockSet.As<IQueryable<Bank>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Bank>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Bank>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Bank>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Banks).Returns(mockSet.Object);

            mockContext.Setup(m => m.SaveChanges()).Returns(1);

            // Act
            var controller = new BanksController(mockContext.Object);
            controller.Update(new Bank() {Id = 1, Name = "ANZ Test", CountryId = 2});

            // Assert
            Assert.IsTrue(banks[0].Id == 1 && banks[0].Name.Equals("ANZ Test") && banks[0].CountryId == 2);
        }

        [Test]
        public void New_Bank_ReturnsListOfCountries()
        {
            // Arrange
            List<Country> countries = new List<Country>()
            {
                new Country() {Id = 1, Name = "New Zealand"},
                new Country() {Id = 2, Name = "Australia"},
                new Country() {Id = 3, Name = "United States of America"}
            };

            var data = countries.AsQueryable();

            var mockSet = new Mock<DbSet<Country>>();
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();

            mockContext.Setup(m => m.Countries).Returns(mockSet.Object);

            mockContext.Setup(m => m.SaveChanges()).Returns(1);

            // Act
            var controller = new BanksController(mockContext.Object);
            var result = (ViewResult) controller.New();
            var formViewModel = (FormBanksViewModel) result.Model;
            var resultCountryList = formViewModel.Countries;

            var diff = data.Except(resultCountryList);

            // Assert
            Assert.IsTrue(!diff.Any());

        }

        [Test]
        public void List_Bank_ReturnsListOfBanks()
        {
            List<Bank> banks = new List<Bank>()
            {
                new Bank() {Id = 1, Name = "ANZ", CountryId = 1},
                new Bank() {Id = 2, Name = "BNZ", CountryId = 1},
                new Bank() {Id = 3, Name = "NAB", CountryId = 2}
            };

            var data = banks.AsQueryable();

            var mockSet = new Mock<DbSet<Bank>>();
            mockSet.As<IQueryable<Bank>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Bank>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Bank>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Bank>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(x => x.Include(It.IsAny<String>())).Returns(mockSet.Object);

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Banks).Returns(mockSet.Object);

            mockContext.Setup(m => m.SaveChanges()).Returns(1);

            // Act
            var controller = new BanksController(mockContext.Object);
            ViewResult result = (ViewResult) controller.Index();
            List<Bank> resultBanks = (List<Bank>) result.Model;
            var diff = data.Except(resultBanks);

            // Assert
            Assert.IsTrue(!diff.Any());
        }
    }

}
