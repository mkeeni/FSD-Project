using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using System.Security.Principal;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToAccounts
    {
        Accounts account;

        public ConvertToAccounts(AddNewAccountDTO addNewAccountDTO)
        {
            account = new Accounts();
            Random rand = new Random();
            long number = 123999999999;
            long randomNumber = rand.Next(100000000, 999999999);
            number = number - randomNumber;
            account.AccountNumber = number;
            account.Balance = 0;
            account.AccountType = addNewAccountDTO.AccountType;
            account.Status = "Open Account Request Pending";
            account.BranchID = addNewAccountDTO.BranchID;
            account.CustomerID = addNewAccountDTO.CustomerID;
        }

        public Accounts GetAccount()
        {
            return account;
        }
    }
}
