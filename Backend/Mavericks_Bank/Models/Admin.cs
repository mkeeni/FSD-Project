using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mavericks_Bank.Models
{
    public class Admin:IEquatable<Admin>
    {
        [Key]
        public int AdminID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [ForeignKey("Email")]
        public Validation? Validation { get; set; }

        public Admin()
        {

        }

        public Admin(int adminID, string name, string email)
        {
            AdminID = adminID;
            Name = name;
            Email = email;
        }

        public bool Equals(Admin? other)
        {
            return AdminID == this.AdminID;
        }
    }
}
