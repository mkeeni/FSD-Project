using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface IAccountsUserService
    {
        public Task<List<Accounts>> GetAllCustomerApprovedAccounts(int customerID);
        public Task<Accounts> AddAccount(AddNewAccountDTO addNewAccountDTO);
        public Task<Accounts> CloseAccount(int accountID);
    }
}
