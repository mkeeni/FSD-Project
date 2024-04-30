using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ITransactionsAdminService:ITransactionsUserService
    {
        public Task<List<Transactions>> GetAllTransactions();
        public Task<List<Transactions>> GetAllAccountTransactions(int accountID);
        public Task<InboundAndOutboundTransactions> GetAccountInboundAndOutbooundTransactions(int accountID);
        public Task<AccountStatementDTO> GetAccountStatement(int accountID, DateTime fromDate, DateTime toDate);
        public Task<InboundAndOutboundTransactions> GetCustomerInboundAndOutbooundTransactions(int customerID);
        public Task<Transactions> GetTransaction(int transactionID);
        public Task<Transactions> DeleteTransaction(int transactionID);
        public Task<Transactions> UpdateTransactionStatus(int transactionID, string status);
    }
}
