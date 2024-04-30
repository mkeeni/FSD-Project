using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace Mavericks_Bank.Services
{
    public class TransactionsService : ITransactionsAdminService
    {
        private readonly IRepository<int, Transactions> _transactionsRepository; 

        private readonly ICustomersAdminService _customersService;
        private readonly IAccountsAdminService _accountservice;
        private readonly IBeneficiariesAdminService _beneficiaryService;
        private readonly ILogger<TransactionsService> _loggerTransactionsService;

        public TransactionsService(IRepository<int, Transactions> transactionsRepository, ICustomersAdminService customersService, IAccountsAdminService accountservice, IBeneficiariesAdminService beneficiaryService, ILogger<TransactionsService> loggerTransactionsService)
        {
            _transactionsRepository = transactionsRepository;
            _customersService = customersService;
            _accountservice = accountservice;
            _beneficiaryService = beneficiaryService;
            _loggerTransactionsService = loggerTransactionsService;
        }

        /// <summary>
        /// Initiating Deposit Transaction for Customers
        /// </summary>
        /// <param name="addTransactionDepositDTO">Object of addTransactionDepositDTO</param>
        /// <returns>Async Transactions object</returns>
        /// <exception cref="NoAccountsFoundException"></exception>
        public async Task<Transactions> AddTransactionDeposit(AddTransactionDepositDTO addTransactionDepositDTO)
        {
            var foundAccount = await _accountservice.GetAccount(addTransactionDepositDTO.AccountID);
            if (foundAccount.Status != "Open Account Request Approved")
            {
                throw new NoAccountsFoundException("Your Account is currently Inactive");
            }

            Transactions newTransaction = new ConvertToTransactions(addTransactionDepositDTO).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            await UpdateTransactionStatus(addedTransaction.TransactionID, "Success");
            var updatedBalance = foundAccount.Balance + addTransactionDepositDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionDepositDTO.AccountID, updatedBalance);

            return addedTransaction;
        }

        /// <summary>
        /// Initiating Transfer Transaction for Customers
        /// </summary>
        /// <param name="addTransactionTransferDTO">Object of addTransactionTransferDTO</param>
        /// <returns>Async Transactions object</returns>
        /// <exception cref="NoAccountsFoundException"></exception>
        /// <exception cref="NoBeneficiariesFoundException"></exception>
        /// <exception cref="TransactionAmountExceedsException"></exception>
        public async Task<Transactions> AddTransactionTransfer(AddTransactionTransferDTO addTransactionTransferDTO)
        {
            var foundAccount = await _accountservice.GetAccount(addTransactionTransferDTO.AccountID);
            if (foundAccount.Status != "Open Account Request Approved")
            {
                throw new NoAccountsFoundException("Your Account is currently Inactive");
            }

            var allCustomerBeneficiaries = await _beneficiaryService.GetAllCustomerBeneficiaries(foundAccount.CustomerID);
            var foundBeneficiary = allCustomerBeneficiaries.FirstOrDefault(beneficiary => beneficiary.BeneficiaryID == addTransactionTransferDTO.BeneficiaryID);
            if(foundBeneficiary == null)
            {
                throw new NoBeneficiariesFoundException("Entered Invalid Beneficiary ID");
            }

            Transactions newTransaction = new ConvertToTransactions(addTransactionTransferDTO).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            if (addTransactionTransferDTO.Amount > foundAccount.Balance)
            {
                await UpdateTransactionStatus(addedTransaction.TransactionID, "Failed");
                throw new TransactionAmountExceedsException("Amount entered exceeds the balance of your Account");
            }   

            await UpdateTransactionStatus(addedTransaction.TransactionID,"Success");
            var updatedBalance = foundAccount.Balance - addTransactionTransferDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionTransferDTO.AccountID, updatedBalance);
            try
            {
                var foundBenAccount = await _accountservice.GetAccountByAccountNumber(foundBeneficiary.AccountNumber);
                updatedBalance = foundBenAccount.Balance + addTransactionTransferDTO.Amount;
                await _accountservice.UpdateAccountBalance(foundBenAccount.AccountID, updatedBalance);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            return addedTransaction;
        }

        /// <summary>
        /// Initiating Transfer Transaction with Beneficiary Details for Customers
        /// </summary>
        /// <param name="addTransactionTransferBeneficiaryDTO">Object of addTransactionTransferBeneficiaryDTO</param>
        /// <returns>Async Transactions object</returns>
        /// <exception cref="NoAccountsFoundException"></exception>
        /// <exception cref="BeneficiaryAlreadyExistsException"></exception>
        /// <exception cref="TransactionAmountExceedsException"></exception>
        public async Task<Transactions> AddTransactionTransferBeneficiary(AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO)
        {
            var foundAccount = await _accountservice.GetAccount(addTransactionTransferBeneficiaryDTO.AccountID);
            if(foundAccount.Status != "Open Account Request Approved")
            {
                throw new NoAccountsFoundException("Your Account is currently Inactive");
            }

            Beneficiaries newBeneficiary = new ConvertToBeneficiaries(addTransactionTransferBeneficiaryDTO).GetBeneficiary();
            var addedBeneficiary = await _beneficiaryService.AddBeneficiary(newBeneficiary);

            Transactions newTransaction = new ConvertToTransactions(addTransactionTransferBeneficiaryDTO,addedBeneficiary.BeneficiaryID).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            if (addTransactionTransferBeneficiaryDTO.Amount > foundAccount.Balance)
            {
                await UpdateTransactionStatus(addedTransaction.TransactionID, "Failed");
                throw new TransactionAmountExceedsException("Amount entered exceeds the balance of your Account");
            }

            await UpdateTransactionStatus(addedTransaction.TransactionID, "Success");
            var updatedBalance = foundAccount.Balance - addTransactionTransferBeneficiaryDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionTransferBeneficiaryDTO.AccountID, updatedBalance);

            try
            {
                var foundBenAccount = await _accountservice.GetAccountByAccountNumber(addedBeneficiary.AccountNumber);
                updatedBalance = foundBenAccount.Balance + addTransactionTransferBeneficiaryDTO.Amount;
                await _accountservice.UpdateAccountBalance(foundBenAccount.AccountID, updatedBalance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return addedTransaction;
        }

        /// <summary>
        /// Initiating Withdrawal Transaction for Customers
        /// </summary>
        /// <param name="addTransactionWithdrawalDTO">Object of addTransactionWithdrawalDTO</param>
        /// <returns>Async Transactions object</returns>
        /// <exception cref="NoAccountsFoundException"></exception>
        /// <exception cref="TransactionAmountExceedsException"></exception>
        public async Task<Transactions> AddTransactionWithdrawal(AddTransactionWithdrawalDTO addTransactionWithdrawalDTO)
        {
            var foundAccount = await _accountservice.GetAccount(addTransactionWithdrawalDTO.AccountID);
            if (foundAccount.Status != "Open Account Request Approved")
            {
                throw new NoAccountsFoundException("Your Account is currently Inactive");
            }

            Transactions newTransaction = new ConvertToTransactions(addTransactionWithdrawalDTO).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            if (addTransactionWithdrawalDTO.Amount > foundAccount.Balance)
            {
                await UpdateTransactionStatus(addedTransaction.TransactionID, "Failed");
                throw new TransactionAmountExceedsException("Amount entered exceeds the balance of your Account");
            }

            await UpdateTransactionStatus(addedTransaction.TransactionID, "Success");
            var updatedBalance = foundAccount.Balance - addTransactionWithdrawalDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionWithdrawalDTO.AccountID, updatedBalance);

            return addedTransaction;
        }

        /// <summary>
        /// Deleting a made Transaction from Database
        /// </summary>
        /// <param name="transactionID">transactionID as int</param>
        /// <returns>Async Transactions object</returns>
        /// <exception cref="NoTransactionsFoundException"></exception>
        public async Task<Transactions> DeleteTransaction(int transactionID)
        {
            var deletedTransaction = await _transactionsRepository.Delete(transactionID);
            if(deletedTransaction == null)
            {
                throw new NoTransactionsFoundException($"Transaction ID {transactionID} not found");
            }
            return deletedTransaction;
        }

        /// <summary>
        /// Getting the amount of inbound and outbound transactions done in each account
        /// </summary>
        /// <param name="accountID">accountID as int</param>
        /// <returns>Async Transactions object</returns>
        /// <exception cref="NoTransactionsFoundException"></exception>
        public async Task<InboundAndOutboundTransactions> GetAccountInboundAndOutbooundTransactions(int accountID)
        {
            await _accountservice.GetAccount(accountID);
            var allTransactions = await GetAllTransactions();
            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID && transaction.Status == "Success").ToList();
            if(allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No transactions found for {accountID}");
            }

            double allAccountDepositTransactions = allAccountTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionType == "Deposit").ToList().Count();
            double allAccountWithdrawalTransactions = allAccountTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionType == "Transfer" || transaction.TransactionType == "Withdrawal").ToList().Count();
            double ratio = allAccountDepositTransactions / allAccountWithdrawalTransactions;
            var creditWorthy = ratio >= 1 ? "Yes" : "No";

            InboundAndOutboundTransactions inboundAndOutboundTransactions = new InboundAndOutboundTransactions{ TotalTransactions = allAccountTransactions.Count,InboundTransactions = allAccountDepositTransactions,OutboundTransactions = allAccountWithdrawalTransactions,Ratio = ratio,CreditWorthiness = creditWorthy };
            return inboundAndOutboundTransactions;
        }

        public async Task<AccountStatementDTO> GetAccountStatement(int accountID, DateTime fromDate, DateTime toDate)
        {
            var foundAccount = await _accountservice.GetAccount(accountID);
            var allTransactions = await GetAllTransactions();
            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID && transaction.Status == "Success" && transaction.TransactionDate >= fromDate && transaction.TransactionDate <= toDate).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No transactions found for {accountID}");
            }

            var allAccountDepositTransactions = allAccountTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionType == "Deposit").ToList();
            var allAccountWithdrawalTransactions = allAccountTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionType == "Transfer" || transaction.TransactionType == "Withdrawal").ToList();
            double totalDeposit = TotalDeposit(allAccountDepositTransactions);
            double totalWithdrawal = TotalWithdrawal(allAccountWithdrawalTransactions);

            AccountStatementDTO accountStatementDTO = new AccountStatementDTO { TotalDeposit = totalDeposit, TotalWithdrawal = totalWithdrawal, Balance = foundAccount.Balance };
            return accountStatementDTO;
        }

        private double TotalDeposit(List<Transactions> allDeposits)
        {
            double totalDeposit = 0;
            foreach (var deposit in allDeposits)
            {
                totalDeposit = totalDeposit + deposit.Amount;
            }
            return totalDeposit;
        }

        private double TotalWithdrawal(List<Transactions> allWithdrawals)
        {
            double totalWithdrawal = 0;
            foreach (var withdrawal in allWithdrawals)
            {
                totalWithdrawal = totalWithdrawal + withdrawal.Amount;
            }
            return totalWithdrawal;
        }

        public async Task<List<Transactions>> GetAllAccountTransactions(int accountID)
        {
            await _accountservice.GetAccount(accountID);
            var allTransactions = await GetAllTransactions();
            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Account ID {accountID}");
            }
            return allAccountTransactions;
        }

        public async Task<List<Transactions>> GetAllCustomerTransactions(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allCustomers = await _customersService.GetAllCustomers();
            var allAccounts = await _accountservice.GetAllAccounts();
            var allTransactions = await GetAllTransactions();   
            var allCustomerTransactions = (from customer in allCustomers
                                          join account in allAccounts on customer.CustomerID equals account.CustomerID
                                          join transaction in allTransactions on account.AccountID equals transaction.AccountID
                                          where customer.CustomerID == customerID select transaction).OrderByDescending(transaction => transaction.TransactionID).ToList();
            if(allCustomerTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Customer ID {customerID}");
            }
            return allCustomerTransactions;
        }

        public async Task<List<Transactions>> GetAllTransactions()
        {
            var allTransactions = await _transactionsRepository.GetAll();
            if(allTransactions == null)
            {
                throw new NoTransactionsFoundException("No Available Transactions Data");
            }
            return allTransactions;
        }

        public async Task<InboundAndOutboundTransactions> GetCustomerInboundAndOutbooundTransactions(int customerID)
        {
            var allCustomerTransactions = await GetAllCustomerTransactions(customerID);
            var allSuccessfullCustomerTransactions = allCustomerTransactions.Where(transaction => transaction.Status == "Success").ToList();
            if (allSuccessfullCustomerTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No transactions found for {customerID}");
            }

            double allCustomerDepositTransactions = allSuccessfullCustomerTransactions.Where(transaction => transaction.TransactionType == "Deposit").ToList().Count();
            double allCustomerWithdrawalTransactions = allSuccessfullCustomerTransactions.Where(transaction => transaction.TransactionType == "Transfer" || transaction.TransactionType == "Withdrawal").ToList().Count();
            double ratio = 1;
            if( allCustomerWithdrawalTransactions != 0)
            {
                ratio = allCustomerDepositTransactions / allCustomerWithdrawalTransactions;
                ratio = Math.Round((Double)ratio, 2);
            }
            var creditWorthy = ratio >= 1 ? "Yes" : "No";

            InboundAndOutboundTransactions inboundAndOutboundTransactions = new InboundAndOutboundTransactions { TotalTransactions = allSuccessfullCustomerTransactions.Count, InboundTransactions = allCustomerDepositTransactions, OutboundTransactions = allCustomerWithdrawalTransactions, Ratio = ratio, CreditWorthiness = creditWorthy };
            return inboundAndOutboundTransactions;
        }

        public async Task<List<Transactions>> GetLastMonthAccountTransactions(int accountID)
        {
            await _accountservice.GetAccount(accountID);
            var allTransactions = await GetAllTransactions();

            var lastMonthEndDate = DateTime.Today.AddDays(1);
            var lastMonthStartDate = lastMonthEndDate.AddDays(-29);
            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionDate >= lastMonthStartDate && transaction.TransactionDate <= lastMonthEndDate).OrderByDescending(transaction => transaction.TransactionID).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Last Month Transaction History Found for Account ID {accountID}");
            }
            return allAccountTransactions;
        }

        public async Task<List<Transactions>> GetLastTenAccountTransactions(int accountID)
        {
            var allTransactions = await GetAllTransactions();
            await _accountservice.GetAccount(accountID);
            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID).OrderByDescending(transaction => transaction.TransactionID).Take(10).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Account Number {accountID}");
            }
            return allAccountTransactions;
        }

        public async Task<Transactions> GetTransaction(int transactionID)
        {
            var foundTransaction = await _transactionsRepository.Get(transactionID);
            if(foundTransaction == null)
            {
                throw new NoTransactionsFoundException($"Transaction ID {transactionID} not found");
            }
            return foundTransaction;
        }

        public async Task<List<Transactions>> GetTransactionsBetweenTwoDates(int accountID, DateTime fromDate, DateTime toDate)
        {
            var allTransactions = await GetAllTransactions();
            await _accountservice.GetAccount(accountID);

            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionDate >= fromDate && transaction.TransactionDate <= toDate).OrderByDescending(transaction => transaction.TransactionID).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Account ID {accountID} within {fromDate} to {toDate}");
            }
            return allAccountTransactions;
        }

        public async Task<Transactions> UpdateTransactionStatus(int transactionID, string status)
        {
            var foundTransaction = await GetTransaction(transactionID);
            foundTransaction.Status = status;
            var updatedTransaction = await _transactionsRepository.Update(foundTransaction);
            return updatedTransaction;
        }
    }
}
