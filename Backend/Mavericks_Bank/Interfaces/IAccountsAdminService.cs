using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IAccountsAdminService:IAccountsUserService
    {
        public Task<Accounts> GetAccount(int accountID);
        public Task<Accounts> GetAccountByAccountNumber(long accountNumber);
        public Task<List<Accounts>> GetAllAccounts();
        public Task<List<Accounts>> GetAllAccountsStatus(string status);
        public Task<List<Accounts>> GetAllCustomerAccounts(int customerID);
        public Task<Accounts> UpdateAccountBalance(int accountID, double balance);
        public Task<Accounts> UpdateAccountStatus(int accountID, string status);
        public Task<Accounts> DeleteAccount(int accountID);
    }
}
