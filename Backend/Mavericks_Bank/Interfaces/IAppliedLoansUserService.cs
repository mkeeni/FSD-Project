using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface IAppliedLoansUserService
    {
        public Task<List<AppliedLoans>> GetAllCustomerAvailedLoans(int customerID);
        public Task<List<AppliedLoans>> GetAllCustomerAppliedLoans(int customerID);
        public Task<AppliedLoans> AddAppliedLoan(ApplyLoanDTO applyLoanDTO);
    }
}
