using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface IAdminService
    {
        public Task<List<Admin>> GetAllAdmins();
        public Task<Admin> GetAdmin(int adminID);
        public Task<Admin> GetAdminByEmail(string email);
        public Task<Admin> UpdateAdminName(UpdateAdminNameDTO updateAdminNameDTO);
        public Task<Admin> DeleteAdmin(int adminID);
    }
}
