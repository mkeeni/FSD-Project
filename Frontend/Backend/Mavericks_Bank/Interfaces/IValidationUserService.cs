using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface IValidationUserService
    {
        public Task<LoginValidationDTO> Login(LoginValidationDTO loginValidationDTO);
        public Task<LoginValidationDTO> ForgotPassword(LoginValidationDTO loginValidationDTO);
        public Task<LoginValidationDTO> RegisterCustomers(RegisterValidationCustomersDTO registerValidationCustomersDTO);
        public Task<LoginValidationDTO> RegisterBankEmployees(RegisterValidationBankEmployees registerValidationBankEmployees);
        public Task<LoginValidationDTO> RegisterAdmin(RegisterValidationAdminDTO registerValidationAdminDTO);
    }
}
