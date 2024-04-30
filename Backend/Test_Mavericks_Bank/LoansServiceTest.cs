using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
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
    [Order(8)]
    public class LoansServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public void GetAllLoansNotFoundExceptionTest()
        {
            //arrange
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoLoansFoundException>(async () => await loansService.GetAllLoans());
        }

        [Test, Order(2)]
        public async Task AddLoanTest()
        {
            //arrange
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);

            Loans loan1 = new Loans
            {
                LoanID = 1,
                LoanAmount = 800000,
                LoanType = "Peronal Loan",
                Interest = 8.1,
                Tenure = 2
            };

            Loans loan2 = new Loans
            {
                LoanID = 2,
                LoanAmount = 1000000,
                LoanType = "Peronal Loan",
                Interest = 8.3,
                Tenure = 3
            };

            Loans loan3 = new Loans
            {
                LoanID = 3,
                LoanAmount = 1400000,
                LoanType = "Business Loan",
                Interest = 7.2,
                Tenure = 4
            };

            //action
            var addedLoan = await loansService.AddLoan(loan1);
            await loansService.AddLoan(loan2);
            await loansService.AddLoan(loan3);

            //assert
            Assert.That(addedLoan.LoanID, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public async Task GetAllLoansTest()
        {
            //arrange
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);

            //action
            var allLoans = await loansService.GetAllLoans();

            //assert
            Assert.That(allLoans.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(4)]
        [TestCase(4)]
        public void GetLoanNotFoundExceptionTest(int loanID)
        {
            //arrange
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoLoansFoundException>(async () => await loansService.GetLoan(loanID));
        }

        [Test, Order(5)]
        [TestCase(1)]
        public async Task GetLoanTest(int loanID)
        {
            //arrange
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);

            //action
            var foundLoan = await loansService.GetLoan(loanID);

            //assert
            Assert.That(foundLoan.LoanID, Is.EqualTo(loanID));
        }

        [Test, Order(6)]
        public void UpdateLoansNotFoundExceptionTest()
        {
            //arrange
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);

            Loans loan = new Loans
            {
                LoanID = 4,
                LoanAmount = 1400000,
                LoanType = "Business Loan",
                Interest = 7.2,
                Tenure = 4
            };

            //action and assert
            Assert.ThrowsAsync<NoLoansFoundException>(async () => await loansService.UpdateLoanDetails(loan));
        }

        [Test, Order(7)]
        public async Task UpdateLoanDetailsTest()
        {
            //arrange
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);

            Loans loan = new Loans
            {
                LoanID = 3,
                LoanAmount = 1500000,
                LoanType = "Business Loan",
                Interest = 7.2,
                Tenure = 4
            };

            //action
            var updatedLoan = await loansService.UpdateLoanDetails(loan);

            //assert
            Assert.That(updatedLoan.LoanAmount, Is.EqualTo(1500000));
        }

        [Test, Order(8)]
        [TestCase(4)]
        public void DeleteLoanNotFoundExceptionTest(int loanID)
        {
            //arrange
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoLoansFoundException>(async () => await loansService.DeleteLoan(loanID));
        }

        [Test, Order(9)]
        [TestCase(1)]
        public async Task DeleteLoanTest(int loanID)
        {
            //arrange
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);

            //action
            var deletedLoan = await loansService.DeleteLoan(loanID);

            //assert
            Assert.That(deletedLoan.LoanID, Is.EqualTo(loanID));
        }
    }
}
