using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Customers:IEquatable<Customers>
    {
        [Key]
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public long AadharNumber { get; set; }
        public string PANNumber { get; set; }
        public string Gender { get; set;}
        public string Email { get; set; }
        [ForeignKey("Email")]
        public Validation? Validation { get; set; }

        public Customers()
        {

        }

        public Customers(int customerID, string name, DateTime dOB, int age, long phoneNumber, string address, long aadharNumber, string pANNumber, string gender, string email)
        {
            CustomerID = customerID;
            Name = name;
            DOB = dOB;
            Age = age;
            PhoneNumber = phoneNumber;
            Address = address;
            AadharNumber = aadharNumber;
            PANNumber = pANNumber;
            Gender = gender;
            Email = email;
        }

        public bool Equals(Customers? other)
        {
            if(AadharNumber == other.AadharNumber && PANNumber == other.PANNumber)
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
