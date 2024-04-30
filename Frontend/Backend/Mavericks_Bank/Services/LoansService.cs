using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class LoansService : ILoansAdminService
    {
        private readonly IRepository<int,Loans> _loansRepository;
        private readonly ILogger<LoansService> _loggerLoanService;

        public LoansService(IRepository<int, Loans> loansRepository, ILogger<LoansService> loggerLoanService)
        {
            _loansRepository = loansRepository;
            _loggerLoanService = loggerLoanService;
        }

        public async Task<Loans> AddLoan(Loans loan)
        {
            return await _loansRepository.Add(loan);
        }

        public async Task<Loans> DeleteLoan(int loanID)
        {
            var deletedLoan = await _loansRepository.Delete(loanID);
            if (deletedLoan == null) 
            {
                throw new NoLoansFoundException($"Loan ID {loanID} not found");
            }
            return deletedLoan;
        }

        public async Task<List<Loans>> GetAllLoans()
        {
            var allLoans = await _loansRepository.GetAll();
            if(allLoans == null)
            {
                throw new NoLoansFoundException("No Available Loans Data");
            }
            return allLoans;
        }

        public async Task<Loans> GetLoan(int loanID)
        {
            var foundLoan = await _loansRepository.Get(loanID);
            if(foundLoan == null)
            {
                throw new NoLoansFoundException($"Loan ID {loanID} not found");
            }
            return foundLoan;
        }

        public async Task<Loans> UpdateLoanDetails(Loans loan)
        {
            var foundLoan = await GetLoan(loan.LoanID);
            foundLoan.LoanAmount = loan.LoanAmount;
            foundLoan.LoanType = loan.LoanType;
            foundLoan.Interest = loan.Interest;
            foundLoan.Tenure = loan.Tenure;
            var updatedLoan = await _loansRepository.Update(foundLoan);
            return updatedLoan;
        }
    }
}
