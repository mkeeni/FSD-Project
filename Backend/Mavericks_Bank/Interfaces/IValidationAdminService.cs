using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface IValidationAdminService:IValidationUserService
    {
        public Task<UpdatedValidationDTO> UpdateValidationStatus(string email);
        public Task<List<Validation>> GetAllValidations();
    }
}
