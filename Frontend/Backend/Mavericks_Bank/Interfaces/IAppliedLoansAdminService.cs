using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IAppliedLoansAdminService:IAppliedLoansUserService
    {
        public Task<List<AppliedLoans>> GetAllAppliedLoans();
        public Task<List<AppliedLoans>> GetAllAppliedLoansStatus(string status);
        public Task<AppliedLoans> GetAppliedLoan(int loanApplicationID);
        public Task<AppliedLoans> UpdateAppliedLoanStatus(int loanApplicationID,string status);
        public Task<AppliedLoans> DeleteAppliedLoan(int loanApplicationID);
    }
}
