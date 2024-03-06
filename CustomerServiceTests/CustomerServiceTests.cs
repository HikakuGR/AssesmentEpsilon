using AssesmentEpsilon;
using AssesmentEpsilon.Services;
using Castle.Core.Resource;
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

        //[TestMethod]        
        //public async Task Create_Customer_Then_Delete()
        //{

        //    var mockSet = new Mock<DbSet<Customer>>();
        //    var mockContext = new Mock<DatabaseContext>();
        //    mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
        //    var customerService = new CustomerService(mockContext.Object);
        //    Customer customer = new()
        //    {
        //        ContactName = "customer",
        //        Phone = "4343",
        //        Address = "Address",
        //        City = "City",
        //        CompanyName = "CompanyName",
        //        Country = "Country",
        //        PostalCode = "PostalCode",
        //        Region = "Region"
        //    };

        //    await customerService.CreateAsync(customer);
        //    var customerReturned = mockContext.Object.Customers.SingleOrDefault();
        //    Assert.IsNotNull(customerReturned);
        //    Assert.AreEqual(customer.ContactName, customerReturned.ContactName);

        //    await customerService.Remove(customer.Id);
        //    mockContext.Verify(m => m.Remove(customer), Times.Once());
        //    int countCustomers = await mockContext.Object.Customers.CountAsync();
        //    Assert.AreEqual(countCustomers, 0);
        //}

        //[TestMethod]
        //public async Task Create_Three_Customers_ThenGetAllAsync()
        //{
        //    var mockSet = new Mock<DbSet<Customer>>();
        //    var mockContext = new Mock<DatabaseContext>();
        //    mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
        //    var customerService = new CustomerService(mockContext.Object);

            
        //    await customerService.CreateAsync(new Customer { ContactName = "AAA" });
        //    await customerService.CreateAsync(new Customer { ContactName = "BBB" });
        //    await customerService.CreateAsync(new Customer { ContactName = "CCC" });

        //    var customers = await customerService.GetAllAsync();

        //    Assert.AreEqual(3, customers.Count);
        //    Assert.AreEqual("AAA", customers[0].ContactName);
        //    Assert.AreEqual("BBB", customers[1].ContactName);
        //    Assert.AreEqual("ZZZ", customers[2].ContactName);
        //}
        
        [TestMethod]
        public void TestMethod2()
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

            //mockContext.Setup(p => p.Customers.FindAsync(customer.Id)).Returns(Task.FromResult(mockSet.Object));
            //HomeController home = new HomeController(mock.Object);
            //mockSet.Setup(p => p.FindAsync(It.IsAny<Guid>())).Returns();
            //var result = await customerService.Get(Guid.Parse("1"));
            //Assert.AreEqual(customer, result);
        }
    }
}