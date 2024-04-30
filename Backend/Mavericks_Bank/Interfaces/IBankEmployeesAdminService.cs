using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IBankEmployeesAdminService:IBankEmployeesUserService
    {
        public Task<List<BankEmployees>> GetAllBankEmployees();
        public Task<BankEmployees> GetBankEmployee(int employeeID);
    }
}
