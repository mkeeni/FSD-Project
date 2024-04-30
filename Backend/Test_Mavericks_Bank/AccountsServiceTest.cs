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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Test_Mavericks_Bank
{
    [Order(7)]
    public class AccountsServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public void GetAllAccountsNotFoundExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await accountsService.GetAllAccounts());
        }

        [Test, Order(2)]
        public async Task AddAccountTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            AddNewAccountDTO addNewAccountDTO1 = new AddNewAccountDTO
            {
                AccountType = "Current",
                BranchID = 2,
                CustomerID = 2,
            };

            AddNewAccountDTO addNewAccountDTO2 = new AddNewAccountDTO
            {
                AccountType = "Savings",
                BranchID = 2,
                CustomerID = 2,
            };

            AddNewAccountDTO addNewAccountDTO3 = new AddNewAccountDTO
            {
                AccountType = "Business",
                BranchID = 2,
                CustomerID = 2,
            };

            //action
            var addedAccount = await accountsService.AddAccount(addNewAccountDTO1);
            await accountsService.AddAccount(addNewAccountDTO2);
            await accountsService.AddAccount(addNewAccountDTO3);

            //assert
            Assert.That(addedAccount.AccountID, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void AddAccountAlreadyExistsExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            AddNewAccountDTO addNewAccountDTO = new AddNewAccountDTO
            {
                AccountType = "Savings",
                BranchID = 2,
                CustomerID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<AccountAlreadyExistsException>(async () => await accountsService.AddAccount(addNewAccountDTO));
        }

        [Test, Order(4)]
        public async Task GetAllAccountsTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action
            var allAccounts = await accountsService.GetAllAccounts();

            //assert
            Assert.That(allAccounts.Count, Is.Not.EqualTo(0));
        }


        [Test, Order(5)]
        [TestCase(1)]
        public void GetAllCustomerAccountsCustomerNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await accountsService.GetAllCustomerAccounts(customerID));
        }

        [Test, Order(6)]
        [TestCase(3)]
        public void GetAllCustomerAccountsNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await accountsService.GetAllCustomerAccounts(customerID));
        }

        [Test, Order(7)]
        [TestCase(2)]
        public async Task GetAllCustomerAccountsTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action
            var allCustomerAccounts = await accountsService.GetAllCustomerAccounts(customerID);

            //assert
            Assert.That(allCustomerAccounts.Count, Is.Not.EqualTo(0));
        }


        [Test, Order(8)]
        [TestCase(4, "Open Account Request Approved")]
        public void UpdateAccountNotFoundExceptionTest(int accountID, string status)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await accountsService.UpdateAccountStatus(accountID,status));
        }

        [Test, Order(9)]
        [TestCase(1, "Open Account Request Approved")]
        public async Task UpdateAccountStatusTest(int accountID, string status)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action
            var updatedAccount = await accountsService.UpdateAccountStatus(accountID, status);

            //assert
            Assert.That(updatedAccount.Status, Is.EqualTo("Open Account Request Approved"));
        }


        [Test, Order(10)]
        [TestCase(1)]
        public void GetAllCustomerApprovedAccountsCustomerNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await accountsService.GetAllCustomerApprovedAccounts(customerID));
        }

        [Test, Order(11)]
        [TestCase(3)]
        public void GetAllCustomerApprovedAccountsNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await accountsService.GetAllCustomerApprovedAccounts(customerID));
        }

        [Test, Order(12)]
        [TestCase(2)]
        public async Task GetAllCustomerApprovedAccountsTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action
            var allCustomerApprovedAccounts = await accountsService.GetAllCustomerApprovedAccounts(customerID);

            //assert
            Assert.That(allCustomerApprovedAccounts.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(13)]
        [TestCase("Open Account Request Rejected")]
        public void GetAllAccountsStatusAccountsNotFoundExceptionTest(string status)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await accountsService.GetAllAccountsStatus(status));
        }

        [Test, Order(14)]
        [TestCase("Open Account Request Approved")]
        public async Task GetAllAccountsStatusAccountsTest(string status)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action
            var allAccountsStatus = await accountsService.GetAllAccountsStatus(status);

            //assert
            Assert.That(allAccountsStatus.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(15)]
        [TestCase(4)]
        public void GetAccountNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await accountsService.GetAccount(accountID));
        }

        [Test, Order(16)]
        [TestCase(1)]
        public async Task GetAccountTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action
            var foundAccount = await accountsService.GetAccount(accountID);

            //assert
            Assert.That(foundAccount.AccountID, Is.EqualTo(1));
        }


        [Test, Order(17)]
        [TestCase(4)]
        public void CloseAccountNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await accountsService.CloseAccount(accountID));
        }


        [Test, Order(18)]
        [TestCase(1)]
        public async Task CloseAccountTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action
            var closedAccount = await accountsService.CloseAccount(accountID);

            //assert
            Assert.That(closedAccount.Status, Is.EqualTo("Close Account Request Pending"));
        }

        [Test, Order(19)]
        [TestCase(4)]
        public void DeleteAccountNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await accountsService.DeleteAccount(accountID));
        }

        [Test, Order(20)]
        [TestCase(1)]
        public async Task DeleteAccountTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);

            //action
            var deletedAccount = await accountsService.DeleteAccount(accountID);

            //assert
            Assert.That(deletedAccount.AccountID, Is.EqualTo(1));
        }
    }
}
