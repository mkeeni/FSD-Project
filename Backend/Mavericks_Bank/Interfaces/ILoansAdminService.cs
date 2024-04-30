using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ILoansAdminService:ILoansUserService
    {
        public Task<Loans> AddLoan(Loans loan);
        public Task<Loans> UpdateLoanDetails(Loans loans);
        public Task<Loans> DeleteLoan(int loanID);
    }
}
