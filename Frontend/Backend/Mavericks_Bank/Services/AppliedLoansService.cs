using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class AppliedLoansService : IAppliedLoansAdminService
    {
        private readonly IRepository<int,AppliedLoans> _appliedLoansRepository;
        private readonly ICustomersAdminService _customersService;
        private readonly ILoansAdminService _loansService;
        private readonly IAccountsAdminService _accountsService;
        private readonly ILogger<AppliedLoansService> _loggerAppliedLoansService;

        public AppliedLoansService(IRepository<int, AppliedLoans> appliedLoansRepository, ICustomersAdminService customersService, ILoansAdminService loansService, IAccountsAdminService accountsService, ILogger<AppliedLoansService> loggerAppliedLoansService)
        {
            _appliedLoansRepository = appliedLoansRepository;
            _customersService = customersService;
            _loansService = loansService;
            _accountsService = accountsService;
            _loggerAppliedLoansService = loggerAppliedLoansService;
        }

        public async Task<AppliedLoans> AddAppliedLoan(ApplyLoanDTO applyLoanDTO)
        {
            var foundLoan = await _loansService.GetLoan(applyLoanDTO.LoanID);
            if(applyLoanDTO.Amount > foundLoan.LoanAmount)
            {
                throw new LoanAmountExceedsException("Entered Amount Exceeds the Available Loan Amount");
            }

            var allAccounts = await _accountsService.GetAllAccounts();
            var foundSavingsAccount = allAccounts.FirstOrDefault(account => account.CustomerID == applyLoanDTO.CustomerID && account.AccountType == "Savings" && account.Status != "Close Account Request Approved" && account.Status != "Open Account Request Disapproved");
            if(foundSavingsAccount == null) 
            {
                throw new NoAccountsFoundException("You do not have a Savings Account, Create a Savings account as your amount will be dispersed in that Account");
            }
            else if(foundSavingsAccount.Status == "Close Account Request Pending")
            {
                throw new NoAccountsFoundException("Your Savings Account has been sent for closure, please wait while we close it or restore it");
            }
            else if (foundSavingsAccount.Status == "Open Account Request Pending")
            {
                throw new NoAccountsFoundException("Your Savings Account is not yet approved");
            }
            
            var newAppliedLoan = new ConvertToAppliedLoans(applyLoanDTO).GetAppliedLoan();
            var allAppliedLoans = await _appliedLoansRepository.GetAll();
            if(allAppliedLoans != null)
            {
                if (allAppliedLoans.Contains(newAppliedLoan))
                {
                    throw new AppliedLoanAlreadyExistsException("You have already applied for this Loan");
                }
            }

            return await _appliedLoansRepository.Add(newAppliedLoan);
        }

        public async Task<AppliedLoans> DeleteAppliedLoan(int loanApplicationID)
        {
            var deletedAppliedLoan = await _appliedLoansRepository.Delete(loanApplicationID);
            if(deletedAppliedLoan == null)
            {
                throw new NoAppliedLoansFoundException($"Loan Application ID {loanApplicationID} not found");
            }
            return deletedAppliedLoan;
        }

        public async Task<List<AppliedLoans>> GetAllAppliedLoans()
        {
            var allAppliedLoans = await _appliedLoansRepository.GetAll();
            if(allAppliedLoans == null )
            {
                throw new NoAppliedLoansFoundException("No Applied Loans Data Found");
            }
            return allAppliedLoans;
        }

        public async Task<List<AppliedLoans>> GetAllCustomerAppliedLoans(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allAppliedLoans = await GetAllAppliedLoans();
            var allCustomerAppliedLoans = allAppliedLoans.Where(appliedLoan => appliedLoan.CustomerID == customerID).ToList();
            if(allCustomerAppliedLoans.Count == 0)
            {
                throw new NoAppliedLoansFoundException($"No Applied Loans found for Customer ID {customerID}");
            }
            return allCustomerAppliedLoans;
        }

        public async Task<List<AppliedLoans>> GetAllCustomerAvailedLoans(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allAppliedLoans = await GetAllAppliedLoans();
            var allCustomerAppliedLoans = allAppliedLoans.Where(appliedLoan => appliedLoan.CustomerID == customerID && appliedLoan.Status == "Approved").ToList();
            if (allCustomerAppliedLoans.Count == 0)
            {
                throw new NoAppliedLoansFoundException($"No Availed Loans found for Customer ID {customerID}");
            }
            return allCustomerAppliedLoans;
        }

        public async Task<List<AppliedLoans>> GetAllAppliedLoansStatus(string status)
        {
            var allAppliedLoans = await GetAllAppliedLoans();
            var allPendingAppliedLoans = allAppliedLoans.Where(appliedLoan => appliedLoan.Status == status).ToList();
            if (allPendingAppliedLoans.Count == 0)
            {
                throw new NoAppliedLoansFoundException($"No {status} Applied Loans Data Found");
            }
            return allPendingAppliedLoans;
        }

        public async Task<AppliedLoans> GetAppliedLoan(int loanApplicationID)
        {
            var foundAppliedLoan = await _appliedLoansRepository.Get(loanApplicationID);
            if(foundAppliedLoan == null)
            {
                throw new NoAppliedLoansFoundException($"Loan Application ID {loanApplicationID} not found");
            }
            return foundAppliedLoan;
        }

        public async Task<AppliedLoans> UpdateAppliedLoanStatus(int loanApplicationID, string status)
        {
            var foundAppliedLoan = await GetAppliedLoan(loanApplicationID);
            foundAppliedLoan.Status = status;
            var updatedAppliedLoan = await _appliedLoansRepository.Update(foundAppliedLoan);

            if(status == "Approved")
            {
                var allAccounts = await _accountsService.GetAllAccounts();
                var foundSavingsAccount = allAccounts.FirstOrDefault(account => account.CustomerID == foundAppliedLoan.CustomerID && account.AccountType == "Savings" && account.Status == "Open Account Request Approved");
                if(foundSavingsAccount != null)
                {
                    var updatedBalance = foundSavingsAccount.Balance + foundAppliedLoan.Amount;
                    await _accountsService.UpdateAccountBalance(foundSavingsAccount.AccountID, updatedBalance);
                }
            }
            return updatedAppliedLoan;
        }
    }
}
