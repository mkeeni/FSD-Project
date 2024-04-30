using Castle.Core.Resource;
using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Repositories;
using Mavericks_Bank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Mavericks_Bank
{
    [Order(2)]
    public class CustomersServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public async Task GetAllCustomersTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);

            //action
            var allCustomers = await customersService.GetAllCustomers();

            //assert
            Assert.That(allCustomers.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(2)]
        [TestCase(4)]
        public void GetCustomerNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await customersService.GetCustomer(customerID));
        }

        [Test, Order(2)]
        [TestCase("notexists@gmail.com")]
        public void GetCustomerByEmailNotFoundExceptionTest(string email)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await customersService.GetCustomerByEmail(email));
        }

        [Test, Order(3)]
        [TestCase(1)]
        public async Task GetCustomerTest(int customerID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);

            //action
            var foundCustomer = await customersService.GetCustomer(customerID);

            //assert
            Assert.That(foundCustomer.CustomerID, Is.EqualTo(1));
        }

        [Test, Order(3)]
        [TestCase("mohnish@gmail.com")]
        public async Task GetCustomerByEmailTest(string email)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);

            //action
            var foundCustomer = await customersService.GetCustomerByEmail(email);

            //assert
            Assert.That(foundCustomer.Email, Is.EqualTo(email));
        }

        [Test, Order(4)]
        public void UpdateCustomerNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);


            UpdateCustomerDTO updateCustomerDTO = new UpdateCustomerDTO
            {
                CustomerID = 4,
                Name = "Mohnish",
                DOB = DateTime.Parse("2002-09-06"),
                Age = 23,
                PhoneNumber = 9452676587,
                Address = "Kolkata",
                AadharNumber = 987612344567,
                PANNumber = "IGCVG7843D",
                Gender = "Male"
            };

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await customersService.UpdateCustomerDetails(updateCustomerDTO));
        }

        [Test, Order(5)]
        public async Task UpdateCustomerDetailsTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);


            UpdateCustomerDTO updateCustomerDTO = new UpdateCustomerDTO
            {
                CustomerID = 1,
                Name = "Mohnish Keeni",
                DOB = DateTime.Parse("2000-09-06"),
                Age = 23,
                PhoneNumber = 9452676587,
                Address = "Kolkata",
                AadharNumber = 987612344567,
                PANNumber = "IGCVG7843D",
                Gender = "Male"
            };

            //action
            var updatedCustomer = await customersService.UpdateCustomerDetails(updateCustomerDTO);

            //assert
            Assert.That(updatedCustomer.CustomerID, Is.EqualTo(1));
        }

        [Test, Order(6)]
        [TestCase(4)]
        public void DeleteCustomerNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await customersService.DeleteCustomer(customerID));
        }

        [Test, Order(7)]
        [TestCase(1)]
        public async Task DeleteCustomerTest(int customerID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);

            //action
            var deletedCustomer = await customersService.DeleteCustomer(customerID);

            //assert
            Assert.That(deletedCustomer.CustomerID, Is.EqualTo(1));
        }
    }
}
