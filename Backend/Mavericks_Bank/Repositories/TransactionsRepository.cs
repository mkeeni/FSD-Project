using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class TransactionsRepository : IRepository<int, Transactions>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<TransactionsRepository> _loggerTransactionsRepository;

        public TransactionsRepository(MavericksBankContext mavericksBankContext, ILogger<TransactionsRepository> loggerTransactionsRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerTransactionsRepository = loggerTransactionsRepository;
        }

        public async Task<Transactions> Add(Transactions item)
        {
            _mavericksBankContext.Transactions.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerTransactionsRepository.LogInformation($"Added New Transaction : {item.TransactionID}");
            return item;
        }

        public async Task<Transactions?> Delete(int key)
        {
            var foundTransaction = await Get(key);
            if (foundTransaction == null)
            {
                return null;
            }
            else
            {
                _mavericksBankContext.Transactions.Remove(foundTransaction);
                await _mavericksBankContext.SaveChangesAsync();
                _loggerTransactionsRepository.LogInformation($"Deleted Transaction : {foundTransaction.TransactionID}");
                return foundTransaction;
            }
        }

        public async Task<Transactions?> Get(int key)
        {
            var foundTransaction = await _mavericksBankContext.Transactions
                .Include(transaction => transaction.Accounts).ThenInclude(account => account!.Branches).ThenInclude(branch => branch!.Banks)
                .Include(transaction => transaction.Accounts).ThenInclude(account => account!.Customers)
                .Include(transaction => transaction.Beneficiaries).ThenInclude(beneficiary => beneficiary!.Branches).ThenInclude(branch => branch!.Banks)
                .Include(transaction => transaction.Beneficiaries).ThenInclude(beneficiary => beneficiary!.Customers).FirstOrDefaultAsync(transaction => transaction.TransactionID == key);
            if (foundTransaction == null)
            {
                return null;
            }
            else
            {
                _loggerTransactionsRepository.LogInformation($"Founded Transaction : {foundTransaction.TransactionID}");
                return foundTransaction;
            }
        }

        public async Task<List<Transactions>?> GetAll()
        {
            var allTransactions = await _mavericksBankContext.Transactions
                .Include(transaction => transaction.Accounts).ThenInclude(account => account!.Branches).ThenInclude(branch => branch!.Banks)
                .Include(transaction => transaction.Accounts).ThenInclude(account => account!.Customers)
                .Include(transaction => transaction.Beneficiaries).ThenInclude(beneficiary => beneficiary!.Branches).ThenInclude(branch => branch!.Banks)
                .Include(transaction => transaction.Beneficiaries).ThenInclude(beneficiary => beneficiary!.Customers).ToListAsync();
            if (allTransactions.Count == 0)
            {
                return null;
            }
            else
            {
                _loggerTransactionsRepository.LogInformation("All Transactions Returned");
                return allTransactions;
            }
        }

        public async Task<Transactions> Update(Transactions item)
        {
            _mavericksBankContext.Entry<Transactions>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerTransactionsRepository.LogInformation($"Updated Transaction : {item.TransactionID}");
            return item;
        }
    }
}
