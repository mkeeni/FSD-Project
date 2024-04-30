using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface ILoansUserService
    {
        public Task<List<Loans>> GetAllLoans();
        public Task<Loans> GetLoan(int loanID);
    }
}
