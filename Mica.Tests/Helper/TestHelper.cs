using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mica.Tests.Helper
{
    class TestHelper
    {

        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {

            var data = sourceList.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(x => x.Include(It.IsAny<String>())).Returns(mockSet.Object);

            return mockSet.Object;
        }
    }
}
