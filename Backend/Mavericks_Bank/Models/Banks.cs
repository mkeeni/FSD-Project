using System.ComponentModel.DataAnnotations;

namespace Mavericks_Bank.Models
{
    public class Banks : IEquatable<Banks>
    {
        [Key]
        public int BankID { get; set; }
        public string BankName { get; set; }

        public Banks()
        {
        }

        public Banks(int bankID, string bankName)
        {
            BankID = bankID;
            BankName = bankName;
        }

        public bool Equals(Banks? other)
        {
            return BankName == other.BankName;
        }
    }
}
