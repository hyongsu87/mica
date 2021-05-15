using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Mica.Controllers;
using Mica.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Moq;


namespace Mica.Tests
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
            /*ViewResult actionResult = (ViewResult)controller.GetBanks();
            var banks = (List<Bank>)actionResult.Model;*/

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
    }

}
