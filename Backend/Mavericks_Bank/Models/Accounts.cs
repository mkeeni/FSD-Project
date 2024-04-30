using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Accounts:IEquatable<Accounts>
    {
        [Key]
        public int AccountID { get; set; }
        public long AccountNumber { get; set; }
        public double Balance { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; }
        public int BranchID { get; set; }
        [ForeignKey("BranchID")]
        public Branches? Branches { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customers? Customers { get; set; }

        public Accounts()
        {

        }

        public Accounts(int accountID, long accountNumber, double balance, string accountType, string status, int branchID, int customerID)
        {
            AccountID = accountID;
            AccountNumber = accountNumber;
            Balance = balance;
            AccountType = accountType;
            Status = status;
            BranchID = branchID;
            CustomerID = customerID;
        }

        public bool Equals(Accounts? other)
        {
            return AccountNumber == other.AccountNumber;
        }
    }
}
