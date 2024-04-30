using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Loans:IEquatable<Loans>
    {
        [Key]
        public int LoanID { get; set; }
        public double LoanAmount { get; set; }
        public string LoanType { get; set; }
        public double Interest { get; set; }
        public int Tenure { get; set; }
       
        public Loans()
        {

        }

        public Loans(int loanID, double loanAmount, string loanType, double interest, int tenure)
        {
            LoanID = loanID;
            LoanAmount = loanAmount;
            LoanType = loanType;
            Interest = interest;
            Tenure = tenure;
        }

        public bool Equals(Loans? other)
        {
            return LoanID == other.LoanID;
        }
    }
}
