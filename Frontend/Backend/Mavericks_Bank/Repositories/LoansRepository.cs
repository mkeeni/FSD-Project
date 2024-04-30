using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class LoansRepository : IRepository<int, Loans>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<LoansRepository> _loggerLoansRepository;

        public LoansRepository(MavericksBankContext mavericksBankContext, ILogger<LoansRepository> loggerLoansRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerLoansRepository = loggerLoansRepository;
        }

        public async Task<Loans> Add(Loans item)
        {
            _mavericksBankContext.Loans.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerLoansRepository.LogInformation($"Added New Loan : {item.LoanID}");
            return item;
        }

        public async Task<Loans?> Delete(int key)
        {
            var foundLoan = await Get(key);
            if (foundLoan == null)
            {
                return null;
            }
            else
            {
                _mavericksBankContext.Loans.Remove(foundLoan);
                await _mavericksBankContext.SaveChangesAsync();
                _loggerLoansRepository.LogInformation($"Deleted Loan : {foundLoan.LoanID}");
                return foundLoan;
            }
        }

        public async Task<Loans?> Get(int key)
        {
            var foundLoan = await _mavericksBankContext.Loans.FirstOrDefaultAsync(loan => loan.LoanID == key);
            if (foundLoan == null)
            {
                return null;
            }
            else
            {
                _loggerLoansRepository.LogInformation($"Founded Loan : {foundLoan.LoanID}");
                return foundLoan;
            }
        }

        public async Task<List<Loans>?> GetAll()
        {
            var allLoans = await _mavericksBankContext.Loans.ToListAsync();
            if (allLoans.Count == 0)
            {
                return null;
            }
            else
            {
                _loggerLoansRepository.LogInformation("All Loans Returned");
                return allLoans;
            }
        }

        public async Task<Loans> Update(Loans item)
        {
            _mavericksBankContext.Entry<Loans>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerLoansRepository.LogInformation($"Updated Loan : {item.LoanID}");
            return item;
        }
    }
}
