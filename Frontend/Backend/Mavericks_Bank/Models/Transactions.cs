using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Transactions:IEquatable<Transactions>
    {
        [Key]
        public int TransactionID { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public int AccountID { get; set; }
        [ForeignKey("AccountID")]
        public Accounts? Accounts { get; set; }
        public int? BeneficiaryID { get; set; }
        [ForeignKey("BeneficiaryID")]
        public Beneficiaries? Beneficiaries { get; set; }

        public Transactions()
        {

        }

        public Transactions(int transactionID, double amount, DateTime transactionDate, string description, string transactionType, string status, int accountID, int? beneficiaryID)
        {
            TransactionID = transactionID;
            Amount = amount;
            TransactionDate = transactionDate;
            Description = description;
            TransactionType = transactionType;
            Status = status;
            AccountID = accountID;
            BeneficiaryID = beneficiaryID;
        }

        public bool Equals(Transactions? other)
        {
            return TransactionID == other.TransactionID;
        }
    }
}
