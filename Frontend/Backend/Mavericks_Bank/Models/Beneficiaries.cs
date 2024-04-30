using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Beneficiaries: IEquatable<Beneficiaries>
    {
        [Key]
        public int BeneficiaryID { get; set; }
        public long AccountNumber { get; set; }
        public string Name { get; set; }
        public string? Status { get; set; } = null;
        public int BranchID { get; set; }
        [ForeignKey("BranchID")]
        public Branches? Branches { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customers? Customers { get; set; }

        public Beneficiaries()
        {

        }

        public Beneficiaries(int beneficiaryID, long accountNumber, string name, string? status, int branchID, int customerID)
        {
            BeneficiaryID = beneficiaryID;
            AccountNumber = accountNumber;
            Name = name;
            Status = status;
            BranchID = branchID;
            CustomerID = customerID;
        }

        public bool Equals(Beneficiaries? other)
        {
            return AccountNumber == other.AccountNumber;
        }
    }
}
