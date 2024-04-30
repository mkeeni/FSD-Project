using Castle.Core.Configuration;
using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Repositories;
using Mavericks_Bank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Test_Mavericks_Bank
{
    [Order(1)]
    public class ValidationServiceTest
    {
        MavericksBankContext mavericksBankContext;
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public void GetAllCustomersNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await customersService.GetAllCustomers());
        }

        [Test, Order(2)]
        public void GetAllBankEmployeesNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBankEmployeesFoundException>(async () => await bankEmployeesService.GetAllBankEmployees());
        }

        [Test, Order(3)]
        public void GetAllAdminNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAdminFoundException>(async () => await adminService.GetAllAdmins());
        }

        [Test,Order(4)]
        public async Task RegisterCustomersTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            RegisterValidationCustomersDTO registerValidationCustomersDTO = new RegisterValidationCustomersDTO 
            { 
                Email = "mohnish@gmail.com",
                Password = "password",
                Name = "Mohnish",
                DOB = DateTime.Parse("2000-09-06"),
                Age = 23,
                PhoneNumber = 9452676587,
                Address = "Kolkata",
                AadharNumber = 987612344567,
                PANNumber = "IGCVG7843D",
                Gender = "Male"
            };

            RegisterValidationCustomersDTO registerValidationCustomersDTO2 = new RegisterValidationCustomersDTO
            {
                Email = "debjit@gmail.com",
                Password = "password",
                Name = "Debjit",
                DOB = DateTime.Parse("2000-10-10"),
                Age = 23,
                PhoneNumber = 9234567234,
                Address = "Asanasol",
                AadharNumber = 563489239012,
                PANNumber = "DVBHY3421H",
                Gender = "Male"
            };

            RegisterValidationCustomersDTO registerValidationCustomersDTO3 = new RegisterValidationCustomersDTO
            {
                Email = "sayani@gmail.com",
                Password = "password",
                Name = "Sayani",
                DOB = DateTime.Parse("2000-10-15"),
                Age = 23,
                PhoneNumber = 9343568745,
                Address = "Behrampore",
                AadharNumber = 987734562334,
                PANNumber = "WFVGT4532B",
                Gender = "Female"
            };

            //action
            LoginValidationDTO loginValidationDTO = await validationService.RegisterCustomers(registerValidationCustomersDTO);
            await validationService.RegisterCustomers(registerValidationCustomersDTO2);
            await validationService.RegisterCustomers(registerValidationCustomersDTO3);

            //assert
            Assert.That(loginValidationDTO.Email, Is.EqualTo("mohnish@gmail.com"));
        }

        [Test, Order(5)]
        public void RegisterCustomersEmailExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            RegisterValidationCustomersDTO registerValidationCustomersDTO = new RegisterValidationCustomersDTO
            {
                Email = "mohnish@gmail.com",
                Password = "password",
                Name = "Mohnish",
                DOB = DateTime.Parse("2000-09-06"),
                Age = 21,
                PhoneNumber = 9452676587,
                Address = "Kolkata",
                AadharNumber = 987612344567,
                PANNumber = "IGCVG7843D",
                Gender = "Male"
            };

            //action and assert
            Assert.ThrowsAsync<ValidationAlreadyExistsException>(async () => await validationService.RegisterCustomers(registerValidationCustomersDTO));
        }

        [Test, Order(6)]
        public void RegisterCustomersAccountExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            RegisterValidationCustomersDTO registerValidationCustomersDTO = new RegisterValidationCustomersDTO
            {
                Email = "mohnish2@gmail.com",
                Password = "passwordchanged",
                Name = "Mohnish",
                DOB = DateTime.Parse("2000-09-06"),
                Age = 23,
                PhoneNumber = 9452676587,
                Address = "Kolkata",
                AadharNumber = 987612344567,
                PANNumber = "IGCVG7843D",
                Gender = "Male"
            };

            //action and assert
            Assert.ThrowsAsync<CustomerAlreadyExistsException>(async () => await validationService.RegisterCustomers(registerValidationCustomersDTO));
        }


        [Test, Order(7)]
        public async Task RegisterBankEmployeesTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            RegisterValidationBankEmployees registerValidationBankEmployees = new RegisterValidationBankEmployees
            {
                Email = "employee@maverick.in",
                Password = "password",
                Name = "Employee"
            };

            //action
            LoginValidationDTO loginValidationDTO = await validationService.RegisterBankEmployees(registerValidationBankEmployees);

            //assert
            Assert.That(loginValidationDTO.Email, Is.EqualTo("employee@maverick.in"));
        }

        [Test, Order(8)]
        public void RegisterBankEmployeesEmailExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            RegisterValidationBankEmployees registerValidationBankEmployees = new RegisterValidationBankEmployees
            {
                Email = "employee@maverick.in",
                Password = "password",
                Name = "Employee"
            };

            //action and assert
            Assert.ThrowsAsync<ValidationAlreadyExistsException>(async () => await validationService.RegisterBankEmployees(registerValidationBankEmployees));
        }

        [Test, Order(9)]
        public async Task RegisterAdminTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            RegisterValidationAdminDTO registerValidationAdminDTO = new RegisterValidationAdminDTO
            {
                Email = "admin@gmail.com",
                Password = "password",
                Name = "Admin"
            };

            //action
            LoginValidationDTO loginValidationDTO = await validationService.RegisterAdmin(registerValidationAdminDTO);

            //assert
            Assert.That(loginValidationDTO.Email, Is.EqualTo("admin@gmail.com"));
        }

        [Test, Order(10)]
        public void RegisterAdminEmailExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            RegisterValidationAdminDTO registerValidationAdminDTO = new RegisterValidationAdminDTO
            {
                Email = "admin@gmail.com",
                Password = "password",
                Name = "Admin"
            };

            //action and assert
            Assert.ThrowsAsync<ValidationAlreadyExistsException>(async () => await validationService.RegisterAdmin(registerValidationAdminDTO));
        }

        [Test,Order(11)]
        public void LoginEmailNotExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "misspelt@gmail.com", Password = "password", UserType = "", Token = "" };

            //action and assert
            Assert.ThrowsAsync<NoValidationFoundException>(async () => await validationService.Login(validationDTO));
        }

        [Test, Order(12)]
        public void LoginIncorrectPasswordExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "mohnish@gmail.com", Password = "wrongpassword", UserType = "", Token = "" };

            //action and assert
            Assert.ThrowsAsync<NoValidationFoundException>(async () => await validationService.Login(validationDTO));
        }

        [Test, Order(13)]
        public async Task LoginTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "mohnish@gmail.com", Password = "password", UserType = "", Token = "" };

            //action
            var loginValidation = await validationService.Login(validationDTO);

            //assert
            Assert.That(loginValidation.Email, Is.EqualTo("mohnish@gmail.com"));
        }

        [Test, Order(14)]
        public void ForgotPasswordEmailNotExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "misspelt@gmail.com", Password = "password", UserType = "", Token = "" };

            //action and assert
            Assert.ThrowsAsync<NoValidationFoundException>(async () => await validationService.ForgotPassword(validationDTO));
        }

        [Test, Order(15)]
        public void ForgotPasswordExistsPasswordExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "mohnish@gmail.com", Password = "password", UserType = "", Token = "" };

            //action and assert
            Assert.ThrowsAsync<ValidationAlreadyExistsException>(async () => await validationService.ForgotPassword(validationDTO));
        }

        [Test, Order(16)]
        public async Task ForgotPasswordTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();
            var mockTokenService = new Mock<ITokenService>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object, mockTokenService.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "mohnish@gmail.com", Password = "newpassword", UserType = "", Token = "" };

            //action
            var loginValidation = await validationService.ForgotPassword(validationDTO);

            //assert
            Assert.That(loginValidation.Email, Is.EqualTo("mohnish@gmail.com"));
        }
    }
}