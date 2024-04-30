using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToAppliedLoans
    {
        AppliedLoans appliedLoan;

        public ConvertToAppliedLoans(ApplyLoanDTO applyLoanDTO)
        {
            appliedLoan = new AppliedLoans();
            appliedLoan.Amount = applyLoanDTO.Amount;
            appliedLoan.Purpose = applyLoanDTO.Purpose;
            appliedLoan.Status = "Pending";
            appliedLoan.LoanID = applyLoanDTO.LoanID;
            appliedLoan.CustomerID = applyLoanDTO.CustomerID;
        }

        public AppliedLoans GetAppliedLoan()
        {
            return appliedLoan;
        }
    }
}
