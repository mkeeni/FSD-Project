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
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Mavericks_Bank
{
    [Order(5)]
    public class BanksServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public void GetAllBanksNotFoundExceptionTest()
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBanksFoundException>(async () => await banksService.GetAllBanks());
        }

        [Test, Order(2)]
        public async Task AddBankTest()
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            Banks bank1 = new Banks
            {
                BankID = 1,
                BankName = "SBI"
            };

            Banks bank2 = new Banks
            {
                BankID = 2,
                BankName = "Mavericks Bank Private Limited"
            };

            Banks bank3 = new Banks
            {
                BankID = 3,
                BankName = "HDFC Bank Private Limited"
            };

            //action
            var addedBank = await banksService.AddBank(bank1);
            await banksService.AddBank(bank2);
            await banksService.AddBank(bank3);

            //assert
            Assert.That(addedBank.BankID, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void AddBankNameAlreadyExistsExceptionTest()
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            Banks bank = new Banks
            {
                BankID = 1,
                BankName = "SBI"
            };

            //action and assert
            Assert.ThrowsAsync<BankNameAlreadyExistsException>(async () => await banksService.AddBank(bank));
        }

        [Test, Order(4)]
        public async Task GetAllBanksTest()
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            //action
            var allBanks = await banksService.GetAllBanks();

            //assert
            Assert.That(allBanks.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(5)]
        [TestCase(4)]
        public void GetBankNotFoundExceptionTest(int bankID)
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBanksFoundException>(async () => await banksService.GetBank(bankID));
        }

        [Test, Order(6)]
        [TestCase(1)]
        public async Task GetBankTest(int bankID)
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            //action
            var foundBank = await banksService.GetBank(bankID);

            //assert
            Assert.That(foundBank.BankID, Is.EqualTo(1));
        }

        [Test, Order(7)]
        public void UpdateBankNotFoundExceptionTest()
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            UpdateBankNameDTO updateBankNameDTO = new UpdateBankNameDTO
            {
                BankID = 4,
                BankName = "SBI"
            };

            //action and assert
            Assert.ThrowsAsync<NoBanksFoundException>(async () => await banksService.UpdateBankName(updateBankNameDTO));
        }

        [Test, Order(8)]
        public void UpdateBankNameAlreadyExistsExceptionTest()
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            UpdateBankNameDTO updateBankNameDTO = new UpdateBankNameDTO
            {
                BankID = 1,
                BankName = "SBI"
            };

            //action and assert
            Assert.ThrowsAsync<BankNameAlreadyExistsException>(async () => await banksService.UpdateBankName(updateBankNameDTO));
        }

        [Test, Order(9)]
        public async Task UpdateBankNameTest()
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            UpdateBankNameDTO updateBankNameDTO = new UpdateBankNameDTO
            {
                BankID = 1,
                BankName = "State Bank of India"
            };

            //action
            var updatedBank = await banksService.UpdateBankName(updateBankNameDTO);

            //assert
            Assert.That(updatedBank.BankID, Is.EqualTo(1));
        }

        [Test, Order(10)]
        [TestCase(4)]
        public void DeleteBankNotFoundExceptionTest(int bankID)
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBanksFoundException>(async () => await banksService.DeleteBank(bankID));
        }

        [Test, Order(11)]
        [TestCase(1)]
        public async Task DeleteBankTest(int bankID)
        {
            //arrange
            var mockBanksRepositoryLogger = new Mock<ILogger<BanksRepository>>();
            var mockBanksServiceLogger = new Mock<ILogger<BanksService>>();
            IRepository<int, Banks> banksRepository = new BanksRepository(mavericksBankContext, mockBanksRepositoryLogger.Object);
            IBanksAdminService banksService = new BanksService(banksRepository, mockBanksServiceLogger.Object);

            //action
            var deletedBank = await banksService.DeleteBank(bankID);

            //assert
            Assert.That(deletedBank.BankID, Is.EqualTo(1));
        }
    }
}
