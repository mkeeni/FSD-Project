using System.ComponentModel.DataAnnotations;

namespace Mavericks_Bank.Models
{
    public class Validation:IEquatable<Validation>
    {
        [Key]
        public string Email { get; set; } 
        public byte[] Password { get; set; }
        public string UserType { get; set; }
        public byte[] Key { get; set; }
        public string? Status { get; set; } = null;

        public Validation()
        {

        }

        public Validation(string email, byte[] password, string userType, byte[] key, string? status)
        {
            Email = email;
            Password = password;
            UserType = userType;
            Key = key;
            Status = status;
        }

        public bool Equals(Validation? other)
        {
            return Email == other.Email;
        }
    }
}
