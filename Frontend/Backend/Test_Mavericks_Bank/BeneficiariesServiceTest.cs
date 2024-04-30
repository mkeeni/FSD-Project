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
    [Order(9)]
    public class BeneficiariesServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public void GetAllBeneficiariesNotFoundExceptionTest()
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBeneficiariesFoundException>(async () => await beneficiariesService.GetAllBeneficiaries());
        }

        [Test, Order(2)]
        public async Task AddBeneficiaryTest()
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            Beneficiaries beneficiary1 = new Beneficiaries
            {
                BeneficiaryID = 1,
                AccountNumber = 98345214567824,
                Name = "Ben",
                BranchID = 3,
                CustomerID = 2,
            };

            Beneficiaries beneficiary2 = new Beneficiaries
            {
                BeneficiaryID = 2,
                AccountNumber = 98345214567825,
                Name = "Ben2",
                BranchID = 3,
                CustomerID = 2,
            };

            Beneficiaries beneficiary3 = new Beneficiaries
            {
                BeneficiaryID = 3,
                AccountNumber = 98345214567826,
                Name = "Ben3",
                BranchID = 2,
                CustomerID = 2,
            };

            //action
            var addedBeneficiary = await beneficiariesService.AddBeneficiary(beneficiary1);
            await beneficiariesService.AddBeneficiary(beneficiary2);
            await beneficiariesService.AddBeneficiary(beneficiary3);

            //assert
            Assert.That(addedBeneficiary.BeneficiaryID, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void AddBeneficiaryAlreadyExistsExceptionTest()
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            Beneficiaries beneficiary = new Beneficiaries
            {
                BeneficiaryID = 1,
                AccountNumber = 98345214567824,
                Name = "Ben",
                BranchID = 3,
                CustomerID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<BeneficiaryAlreadyExistsException>(async () => await beneficiariesService.AddBeneficiary(beneficiary));
        }

        [Test, Order(4)]
        public async Task GetAllBeneficiariesTest()
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            //action
            var allBeneficiaries = await beneficiariesService.GetAllBeneficiaries();

            //assert
            Assert.That(allBeneficiaries.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(5)]
        [TestCase(1)]
        public void GetAllCustomerBeneficiariesCustomerNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await beneficiariesService.GetAllCustomerBeneficiaries(customerID));
        }

        [Test, Order(6)]
        [TestCase(3)]
        public void GetAllCustomerBeneficiariesNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBeneficiariesFoundException>(async () => await beneficiariesService.GetAllCustomerBeneficiaries(customerID));
        }

        [Test, Order(7)]
        [TestCase(2)]
        public async Task GetAllCustomerBeneficiariesTest(int customerID)
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            //action
            var allCustomerBeneficiaries = await beneficiariesService.GetAllCustomerBeneficiaries(customerID);

            //assert
            Assert.That(allCustomerBeneficiaries.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(8)]
        [TestCase(4)]
        public void GetBeneficiaryNotFoundExceptionTest(int beneficiaryID)
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBeneficiariesFoundException>(async () => await beneficiariesService.GetBeneficiary(beneficiaryID));
        }

        [Test, Order(9)]
        [TestCase(1)]
        public async Task GetBeneficiaryTest(int beneficiaryID)
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            //action
            var foundBeneficiary = await beneficiariesService.GetBeneficiary(beneficiaryID);

            //assert
            Assert.That(foundBeneficiary.BeneficiaryID, Is.EqualTo(1));
        }

        [Test, Order(10)]
        [TestCase(4)]
        public void DeleteBeneficiaryNotFoundExceptionTest(int beneficiaryID)
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBeneficiariesFoundException>(async () => await beneficiariesService.DeleteBeneficiary(beneficiaryID));
        }

        [Test, Order(11)]
        [TestCase(1)]
        public async Task DeleteBeneficiaryTest(int beneficiaryID)
        {
            //arrange
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();

            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);

            //action
            var deletedBeneficiary = await beneficiariesService.DeleteBeneficiary(beneficiaryID);

            //assert
            Assert.That(deletedBeneficiary.BeneficiaryID, Is.EqualTo(1));
        }
    }
}
