using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(LoginValidationDTO loginValidationDTO);
    }
}
