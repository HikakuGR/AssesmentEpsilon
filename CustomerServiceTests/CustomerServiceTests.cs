using AssesmentEpsilon;
using AssesmentEpsilon.Services;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Data.Entity.Infrastructure;
using System.Net.Sockets;
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

            mockContext.Verify(m => m.AddAsync(customer, default), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }


        [TestMethod]
        public async Task Find_Customer_Async()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var customerService = new CustomerService(mockContext.Object);
            Guid guid = Guid.NewGuid();
            Customer customer = new()
            {
                Id = guid,
                ContactName = "customer",
                Phone = "4343",
                Address = "Address",
                City = "City",
                CompanyName = "CompanyName",
                Country = "Country",
                PostalCode = "PostalCode",
                Region = "Region"
            };

            mockSet.Setup(p => p.FindAsync(It.IsAny<Guid>())).ReturnsAsync(customer);
            var result = await customerService.Get(guid);
            Assert.AreEqual(customer, result);
        }


        [TestMethod]
        public async Task Find_Then_Remove_Customer()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var customerService = new CustomerService(mockContext.Object);
            Guid guid = Guid.NewGuid();
            Customer customer = new()
            {
                Id = guid,
                ContactName = "customer",
                Phone = "4343",
                Address = "Address",
                City = "City",
                CompanyName = "CompanyName",
                Country = "Country",
                PostalCode = "PostalCode",
                Region = "Region"
            };

            mockSet.Setup(p => p.FindAsync(It.IsAny<Guid>())).ReturnsAsync(customer);
            var result = await customerService.Get(guid);
            Assert.AreEqual(customer, result);

            await customerService.Remove(guid);

            mockContext.Verify(m => m.Remove(customer), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Update_Customer_Async()
        {
            var mockSet = new Mock<DbSet<Customer>>();
            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var customerService = new CustomerService(mockContext.Object);
            Guid guid = Guid.NewGuid();
            Customer customer = new()
            {
                Id = guid,
                ContactName = "customer",
                Phone = "4343",
                Address = "Address",
                City = "City",
                CompanyName = "CompanyName",
                Country = "Country",
                PostalCode = "PostalCode",
                Region = "Region"
            };

            mockSet.Setup(p => p.FindAsync(It.IsAny<Guid>())).ReturnsAsync(customer);

            Microsoft.AspNetCore.Mvc.ActionResult<Customer> result = await customerService.Update(guid, new Customer { ContactName = "Updated Name" });

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.AreEqual("Updated Name", result?.Value?.ContactName);
        }
    }
}