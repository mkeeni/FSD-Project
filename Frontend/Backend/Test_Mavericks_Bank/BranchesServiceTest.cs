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
    [Order(6)]
    public class BranchesServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public void GetAllBranchesNotFoundExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBranchesFoundException>(async () => await branchesService.GetAllBranches());
        }

        [Test, Order(2)]
        public async Task AddBranchTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            Branches branch1 = new Branches
            {
                BranchID = 1,
                IFSCNumber = "Ifsc1",
                BranchName = "Brach1",
                BankID = 2,
            };

            Branches branch2 = new Branches
            {
                BranchID = 2,
                IFSCNumber = "Ifsc2",
                BranchName = "Branch2",
                BankID = 2,
            };

            Branches branch3 = new Branches
            {
                BranchID = 3,
                IFSCNumber = "Ifsc3",
                BranchName = "Branch3",
                BankID = 3,
            };

            //action
            var addedBranch = await branchesService.AddBranch(branch1);
            await branchesService.AddBranch(branch2);
            await branchesService.AddBranch(branch3);

            //assert
            Assert.That(addedBranch.BranchID, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void AddBranchIFSCAlreadyExistsExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            Branches branch = new Branches
            {
                BranchID = 4,
                IFSCNumber = "Ifsc2",
                BranchName = "Branch4",
                BankID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<BranchAlreadyExistsException>(async () => await branchesService.AddBranch(branch));
        }

        [Test, Order(4)]
        public void AddBranchNameAlreadyExistsExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            Branches branch = new Branches
            {
                BranchID = 4,
                IFSCNumber = "Ifsc4",
                BranchName = "Branch2",
                BankID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<BranchAlreadyExistsException>(async () => await branchesService.AddBranch(branch));
        }

        [Test, Order(5)]
        public async Task GetAllBranchesTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action
            var allBranches = await branchesService.GetAllBranches();

            //assert
            Assert.That(allBranches.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(6)]
        [TestCase(4)]
        public void GetBranchesNotFoundExceptionTest(int branchID)
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBranchesFoundException>(async () => await branchesService.GetBranch(branchID));
        }

        [Test, Order(7)]
        [TestCase(1)]
        public async Task GetBranchesTest(int branchID)
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action
            var foundBranch = await branchesService.GetBranch(branchID);

            //assert
            Assert.That(foundBranch.BranchID, Is.EqualTo(1));
        }

        [Test, Order(8)]
        public void UpdateBranchNotFoundExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            UpdateBranchNameDTO updateBranchNameDTO = new UpdateBranchNameDTO
            {
                branchID = 4,
                BranchName = "Branch4"
            };

            //action and assert
            Assert.ThrowsAsync<NoBranchesFoundException>(async () => await branchesService.UpdateBranchName(updateBranchNameDTO));
        }

        [Test, Order(9)]
        public void UpdateBranchNameAlreadyExistsExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            UpdateBranchNameDTO updateBranchNameDTO = new UpdateBranchNameDTO
            {
                branchID = 1,
                BranchName = "Branch2"
            };

            //action and assert
            Assert.ThrowsAsync<BranchAlreadyExistsException>(async () => await branchesService.UpdateBranchName(updateBranchNameDTO));
        }

        [Test, Order(10)]
        public async Task UpdateBranchNameTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            UpdateBranchNameDTO updateBranchNameDTO = new UpdateBranchNameDTO
            {
                branchID = 1,
                BranchName = "Branch10"
            };

            //action
            var updatedBranch = await branchesService.UpdateBranchName(updateBranchNameDTO);

            //assert
            Assert.That(updatedBranch.BranchID, Is.EqualTo(1));
        }

        [Test, Order(11)]
        [TestCase(4)]
        public void DeleteBranchesNotFoundExceptionTest(int branchID)
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBranchesFoundException>(async () => await branchesService.DeleteBranch(branchID));
        }

        [Test, Order(12)]
        [TestCase(1)]
        public async Task DeleteBranchesTest(int branchID)
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action
            var deletedBranch = await branchesService.DeleteBranch(branchID);

            //assert
            Assert.That(deletedBranch.BranchID, Is.EqualTo(1));
        }
    }
}
