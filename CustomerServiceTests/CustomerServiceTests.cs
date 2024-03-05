using AssesmentEpsilon;
using AssesmentEpsilon.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Entity.Infrastructure;
using System.Reflection.Metadata;

namespace CustomerServiceTests
{
    [TestClass]
    public class CustomerServiceTests
    {       
       

        [TestMethod]
        public async Task CreateCustomer_saves_a_customer_via_context()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var customerService = new CustomerService(mockContext.Object);
            Customer customer = new()
            {
                ContactName = "customer",
                Phone = "4343",
                Address = "Address",
                City = "City",
                CompanyName = "CompanyName",
                Country = "Country",
                PostalCode = "PostalCode",
                Region = "Region"
            };

            await customerService.CreateAsync(customer);

            mockContext.Verify(m => m.AddAsync(customer,default), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]        
        public async Task CustomerService_Create_ThenCountAsync()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            var mockContext = new Mock<DatabaseContext>();
             mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var customerService = new CustomerService(mockContext.Object);

            await customerService.CreateAsync(new Customer
            {
                ContactName = "customer 1",
                Phone = "4343"
            });
            

            var customersCount =  customerService.GetCustomerCount();
            Assert.IsTrue(1== customersCount);
        }

        [TestMethod]
        public async Task GetAllBlogsAsync_orders_by_name()
        {

            var data = new List<Customer>
            {
                new Customer {  ContactName = "BBB" },
                new Customer { ContactName = "ZZZ" },
                new Customer { ContactName = "AAA" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(data.GetEnumerator()));

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(data.Provider));

            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var service = new CustomerService(mockContext.Object);
            var blogs = await service.GetAllAsync();

            Assert.AreEqual(3, blogs.Count);
            Assert.AreEqual("AAA", blogs[0].ContactName);
            Assert.AreEqual("BBB", blogs[1].ContactName);
            Assert.AreEqual("ZZZ", blogs[2].ContactName);
        }

    }
}