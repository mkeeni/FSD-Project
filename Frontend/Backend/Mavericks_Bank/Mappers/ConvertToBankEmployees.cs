using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToBankEmployees
    {
        BankEmployees bankEmployee;

        public ConvertToBankEmployees(RegisterValidationBankEmployees registerValidationBankEmployees)
        {
            bankEmployee = new BankEmployees();
            bankEmployee.Name = registerValidationBankEmployees.Name;
            bankEmployee.Email = registerValidationBankEmployees.Email;
        }

        public BankEmployees GetBankEmployees()
        {
            return bankEmployee;
        }
    }
}
