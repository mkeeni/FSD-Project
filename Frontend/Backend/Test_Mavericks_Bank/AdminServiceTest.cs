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
    [Order(4)]
    public class AdminServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public async Task GetAllAdminTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository,validationRepository,mockAdminServiceServiceLogger.Object);

            //action
            var allAdmins = await adminService.GetAllAdmins();

            //assert
            Assert.That(allAdmins.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(2)]
        [TestCase(2)]
        public void GetAdminNotFoundExceptionTest(int adminID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAdminFoundException>(async () => await adminService.GetAdmin(adminID));
        }

        [Test, Order(2)]
        [TestCase("admin2@gmail.com")]
        public void GetAdminByEmailNotFoundExceptionTest(string email)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAdminFoundException>(async () => await adminService.GetAdminByEmail(email));
        }

        [Test, Order(3)]
        [TestCase(1)]
        public async Task GetAdminTest(int adminID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action 
            var foundAdmin = await adminService.GetAdmin(adminID);

            //assert
            Assert.That(foundAdmin.AdminID, Is.EqualTo(1));
        }

        [Test, Order(3)]
        [TestCase("admin@gmail.com")]
        public async Task GetAdminByEmailTest(string email)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action 
            var foundAdmin = await adminService.GetAdminByEmail(email);

            //assert
            Assert.That(foundAdmin.Email, Is.EqualTo(email));
        }

        [Test, Order(4)]
        public void UpdateAdminNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            UpdateAdminNameDTO updateAdminNameDTO = new UpdateAdminNameDTO
            {
                AdminID = 2,
                Name = "Admin 2"
            };

            //action and assert
            Assert.ThrowsAsync<NoAdminFoundException>(async () => await adminService.UpdateAdminName(updateAdminNameDTO));
        }

        [Test, Order(5)]
        public async Task UpdateAdminNameTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            UpdateAdminNameDTO updateAdminNameDTO = new UpdateAdminNameDTO
            {
                AdminID = 1,
                Name = "Admin 1"
            };

            //action
            var updatedAdmin = await adminService.UpdateAdminName(updateAdminNameDTO);

            //assert
            Assert.That(updatedAdmin.AdminID, Is.EqualTo(1));
        }

        [Test, Order(6)]
        [TestCase(2)]
        public void DeleteAdminNotFoundExceptionTest(int adminID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAdminFoundException>(async () => await adminService.DeleteAdmin(adminID));
        }

        [Test, Order(7)]
        [TestCase(1)]
        public async Task DeleteAdminTest(int adminID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action 
            var foundAdmin = await adminService.DeleteAdmin(adminID);

            //assert
            Assert.That(foundAdmin.AdminID, Is.EqualTo(1));
        }
    }
}
