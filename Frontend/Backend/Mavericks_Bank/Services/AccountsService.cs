using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Repositories;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Mavericks_Bank.Services
{
    public class AccountsService : IAccountsAdminService
    {
        private readonly IRepository<int,Accounts> _accountsRepository;
        private readonly ICustomersAdminService _customersService;
        private readonly ILogger<AccountsService> _loggerAccountsService;

        public AccountsService(IRepository<int, Accounts> accountsRepository, ICustomersAdminService customersService, ILogger<AccountsService> loggerAccountsService)
        {
            _accountsRepository = accountsRepository;
            _customersService = customersService;
            _loggerAccountsService = loggerAccountsService;
        }

        public async Task<Accounts> AddAccount(AddNewAccountDTO addNewAccountDTO)
        {
            var allAccounts = await _accountsRepository.GetAll();
            var foundAccount = allAccounts?.FirstOrDefault(account => account.AccountType == addNewAccountDTO.AccountType && account.CustomerID == addNewAccountDTO.CustomerID && account.Status != "Close Account Request Approved" && account.Status != "Open Account Request Disapproved");
            if (foundAccount != null)
            {
                throw new AccountAlreadyExistsException($"Account Type {addNewAccountDTO.AccountType} already exists");
            }
            restart:
            Accounts newAccount = new ConvertToAccounts(addNewAccountDTO).GetAccount();
            if (allAccounts != null)
            {
                
                if (allAccounts.Contains(newAccount))
                {
                    goto restart;
                }
            }
            return await _accountsRepository.Add(newAccount);
        }

        public async Task<Accounts> DeleteAccount(int accountID)
        {
            var deletedAccount = await _accountsRepository.Delete(accountID);
            if(deletedAccount == null) 
            {
                throw new NoAccountsFoundException($"Account ID {accountID} not found");
            }
            return deletedAccount;
        }

        public async Task<Accounts> GetAccount(int accountID)
        {
            var foundAccount = await _accountsRepository.Get(accountID);
            if (foundAccount == null)
            {
                throw new NoAccountsFoundException($"Account ID {accountID} not found");
            }
            return foundAccount;
        }

        public async Task<List<Accounts>> GetAllAccounts()
        {
            var allAccounts = await _accountsRepository.GetAll();
            if(allAccounts == null)
            {
                throw new NoAccountsFoundException("No Available Accounts Data");
            }
            return allAccounts;
        }

        public async Task<List<Accounts>> GetAllCustomerAccounts(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allAccounts = await GetAllAccounts();
            var allCustomerAccounts = allAccounts.Where(account => account.CustomerID == customerID).ToList();
            if(allCustomerAccounts.Count == 0)
            {
                throw new NoAccountsFoundException($"No Accounts Available for Customer ID {customerID}");
            }
            return allCustomerAccounts;
        }

        public async Task<List<Accounts>> GetAllCustomerApprovedAccounts(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allAccounts = await GetAllAccounts();
            var allCustomerAccounts = allAccounts.Where(account => account.CustomerID == customerID && account.Status == "Open Account Request Approved").ToList();
            if (allCustomerAccounts.Count == 0)
            {
                throw new NoAccountsFoundException($"No Approved Accounts Available for Customer ID {customerID}");
            }
            return allCustomerAccounts;
        }

        public async Task<List<Accounts>> GetAllAccountsStatus(string status)
        {
            var allAccounts = await GetAllAccounts();
            var allPendingAccounts = allAccounts.Where(account => account.Status == status).ToList();
            if(allPendingAccounts.Count == 0)
            {
                throw new NoAccountsFoundException($"No {status} Accounts Available");
            }
            return allPendingAccounts;
        }

        public async Task<Accounts> UpdateAccountBalance(int accountID, double balance)
        {
            var foundAccount = await GetAccount(accountID);
            foundAccount.Balance = balance;
            var updatedAccount = await _accountsRepository.Update(foundAccount);
            return updatedAccount;
        }

        public async Task<Accounts> UpdateAccountStatus(int accountID, string status)
        {
            var foundAccount = await GetAccount(accountID);
            foundAccount.Status = status;
            var updatedAccount = await _accountsRepository.Update(foundAccount);
            return updatedAccount;
        }

        public async Task<Accounts> CloseAccount(int accountID)
        {
            var foundAccount = await GetAccount(accountID);
            if(foundAccount.Balance != 0)
            {
                throw new NoAccountsFoundException($"Withdraw your Balance amount {foundAccount.Balance}");
            }
            foundAccount.Status = "Close Account Request Pending";
            var updatedAccount = await _accountsRepository.Update(foundAccount);
            return updatedAccount;
        }

        public async Task<Accounts> GetAccountByAccountNumber(long accountNumber)
        {
            var allAccounts = await GetAllAccounts();
            var foundAccount = allAccounts.FirstOrDefault(account => account.AccountNumber == accountNumber);
            if(foundAccount == null)
            {
                throw new NoAccountsFoundException($"Account Number {accountNumber} not found");
            }
            return foundAccount;
        }
    }
}
