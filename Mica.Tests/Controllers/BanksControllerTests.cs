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
        public void Test_Test1()
        {
            // Arrange
            var banksData = new List<Bank>
            {
                new Bank {  Id = 1, Name = "ANZ", CountryId = 1 },
                new Bank {  Id = 2, Name = "BNZ", CountryId = 1 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Bank>>();
            mockSet.As<IQueryable<Bank>>().Setup(m => m.Provider).Returns(banksData.Provider);
            mockSet.As<IQueryable<Bank>>().Setup(m => m.Expression).Returns(banksData.Expression);
            mockSet.As<IQueryable<Bank>>().Setup(m => m.ElementType).Returns(banksData.ElementType);
            mockSet.As<IQueryable<Bank>>().Setup(m => m.GetEnumerator()).Returns(banksData.GetEnumerator());
            mockSet.Setup(m => m.Add(It.Is(Bank)));

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Banks).Returns(mockSet.Object);

            // Act
            var controller = new BanksController(mockContext.Object);
            controller.Create(new Bank { Id = 3, Name = "Westpac", CountryId = 1 });
            ViewResult actionResult = (ViewResult)controller.GetBanks();
            var banks = (List<Bank>)actionResult.Model;

            // Assert
            Assert.AreEqual(3, banks.Count);
            Assert.AreEqual("ANZ", banks[0].Name);
            Assert.AreEqual("BNZ", banks[1].Name);
            Assert.AreEqual("Westpac", banks[2].Name);
        }
        //[Test]
        public void Test_Test2()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(true);
        }
    }

}
