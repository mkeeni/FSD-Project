using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class AppliedLoans:IEquatable<AppliedLoans>
    {
        [Key]
        public int LoanApplicationID { get; set; }
        public double Amount { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; } = DateTime.Now;
        public int LoanID { get; set; }
        [ForeignKey("LoanID")]
        public Loans? Loans { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customers? Customers { get; set; }

        public AppliedLoans()
        {

        }

        public AppliedLoans(int loanApplicationID, double amount, string purpose, string status, DateTime appliedDate, int loanID, int customerID)
        {
            LoanApplicationID = loanApplicationID;
            Amount = amount;
            Purpose = purpose;
            Status = status;
            AppliedDate = appliedDate;
            LoanID = loanID;
            CustomerID = customerID;
        }

        public bool Equals(AppliedLoans? other)
        {
            if(LoanID == other.LoanID && CustomerID == other.CustomerID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
