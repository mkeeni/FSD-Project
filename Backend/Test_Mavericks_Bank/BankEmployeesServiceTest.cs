using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Repositories;
using Mavericks_Bank.Services;
using Microsoft.AspNetCore.Mvc;
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
    [Order(3)]
    public class BankEmployeesServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public async Task GetAllBankEmployeesTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            //action
            var allBankEmployees = await bankEmployeesService.GetAllBankEmployees();

            //assert
            Assert.That(allBankEmployees.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(2)]
        [TestCase(2)]
        public void GetBankEmployeesNotFoundExceptionTest(int employeeID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBankEmployeesFoundException>(async () => await bankEmployeesService.GetBankEmployee(employeeID));
        }

        [Test, Order(2)]
        [TestCase("employee2@maverick.in")]
        public void GetBankEmployeesByEmailNotFoundExceptionTest(string email)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBankEmployeesFoundException>(async () => await bankEmployeesService.GetEmployeeByEmail(email));
        }

        [Test, Order(3)]
        [TestCase(1)]
        public async Task GetBankEmployeesTest(int employeeID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            //action 
            var foundEmployee = await bankEmployeesService.GetBankEmployee(employeeID);

            //assert
            Assert.That(foundEmployee.EmployeeID, Is.EqualTo(1));
        }

        [Test, Order(3)]
        [TestCase("employee@maverick.in")]
        public async Task GetBankEmployeeByEmailTest(string email)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            //action 
            var foundEmployee = await bankEmployeesService.GetEmployeeByEmail(email);

            //assert
            Assert.That(foundEmployee.Email, Is.EqualTo(email));
        }

        [Test, Order(4)]
        public void UpdateBankEmployeeNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            UpdateBankEmployeeNameDTO updateBankEmployeeNameDTO = new UpdateBankEmployeeNameDTO
            {
                EmployeeID = 2,
                Name = "Employee 2"
            };

            //action and assert
            Assert.ThrowsAsync<NoBankEmployeesFoundException>(async () => await bankEmployeesService.UpdateBankEmployeeName(updateBankEmployeeNameDTO));
        }

        [Test, Order(5)]
        public async Task UpdateBankEmployeeNameTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            UpdateBankEmployeeNameDTO updateBankEmployeeNameDTO = new UpdateBankEmployeeNameDTO
            {
                EmployeeID = 1,
                Name = "Employee 1"
            };

            //action
            var updatedEmployee = await bankEmployeesService.UpdateBankEmployeeName(updateBankEmployeeNameDTO);

            //assert
            Assert.That(updatedEmployee.EmployeeID, Is.EqualTo(1));
        }

        [Test, Order(6)]
        [TestCase(2)]
        public void DeleteBankEmployeesNotFoundExceptionTest(int employeeID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBankEmployeesFoundException>(async () => await bankEmployeesService.DeleteBankEmployee(employeeID));
        }

        [Test, Order(7)]
        [TestCase(1)]
        public async Task DeleteBankEmployeesTest(int employeeID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            //action 
            var deletedEmployee = await bankEmployeesService.DeleteBankEmployee(employeeID);

            //assert
            Assert.That(deletedEmployee.EmployeeID, Is.EqualTo(1));
        }
    }
}
