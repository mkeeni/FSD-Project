using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface IBankEmployeesUserService
    {
        public Task<BankEmployees> GetEmployeeByEmail(string email);
        public Task<BankEmployees> UpdateBankEmployeeName(UpdateBankEmployeeNameDTO updateBankEmployeeNameDTO);
        public Task<BankEmployees> DeleteBankEmployee(int employeeID);
    }
}
