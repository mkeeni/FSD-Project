using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToAdmin
    {
        Admin admin;

        public ConvertToAdmin(RegisterValidationAdminDTO registerValidationAdminDTO)
        {
            admin = new Admin();
            admin.Name = registerValidationAdminDTO.Name;
            admin.Email = registerValidationAdminDTO.Email;
        }

        public Admin GetAdmin()
        {
            return admin;
        }
    }
}
