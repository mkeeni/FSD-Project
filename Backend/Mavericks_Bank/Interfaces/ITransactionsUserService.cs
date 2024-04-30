using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ITransactionsUserService
    {
        public Task<List<Transactions>> GetAllCustomerTransactions(int customerID);
        public Task<List<Transactions>> GetLastTenAccountTransactions(int accountID);
        public Task<List<Transactions>> GetLastMonthAccountTransactions(int accountID);
        public Task<List<Transactions>> GetTransactionsBetweenTwoDates(int accountID, DateTime fromDate, DateTime toDate);
        public Task<Transactions> AddTransactionTransfer(AddTransactionTransferDTO addNewTransferTransactionDTO);
        public Task<Transactions> AddTransactionTransferBeneficiary(AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO);
        public Task<Transactions> AddTransactionDeposit(AddTransactionDepositDTO addTransactionDepositDTO);
        public Task<Transactions> AddTransactionWithdrawal(AddTransactionWithdrawalDTO addTransactionWithdrawalDTO);
    }
}
