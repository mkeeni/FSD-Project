using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToTransactions
    {
        Transactions transaction;

        public ConvertToTransactions(AddTransactionTransferDTO addTransactionTransferDTO)
        {
            transaction = new Transactions();
            transaction.Amount = addTransactionTransferDTO.Amount;
            transaction.Description = addTransactionTransferDTO.Description;
            transaction.TransactionType = "Transfer";
            transaction.Status = "Pending";
            transaction.AccountID = addTransactionTransferDTO.AccountID;
            transaction.BeneficiaryID = addTransactionTransferDTO.BeneficiaryID;
        }

        public ConvertToTransactions(AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO,int beneficiaryID)
        {
            transaction = new Transactions();
            transaction.Amount = addTransactionTransferBeneficiaryDTO.Amount;
            transaction.Description = addTransactionTransferBeneficiaryDTO.Description;
            transaction.TransactionType = "Transfer";
            transaction.Status = "Pending";
            transaction.AccountID = addTransactionTransferBeneficiaryDTO.AccountID;
            transaction.BeneficiaryID = beneficiaryID;
        }

        public ConvertToTransactions(AddTransactionDepositDTO addTransactionDepositDTO)
        {
            transaction = new Transactions();
            transaction.Amount = addTransactionDepositDTO.Amount;
            transaction.Description = addTransactionDepositDTO.Description;
            transaction.TransactionType = "Deposit";
            transaction.Status = "Pending";
            transaction.AccountID = addTransactionDepositDTO.AccountID;
            transaction.BeneficiaryID = null;
        }

        public ConvertToTransactions(AddTransactionWithdrawalDTO addTransactionWithdrawalDTO)
        {
            transaction = new Transactions();
            transaction.Amount = addTransactionWithdrawalDTO.Amount;
            transaction.Description = addTransactionWithdrawalDTO.Description;
            transaction.TransactionType = "Withdrawal";
            transaction.Status = "Pending";
            transaction.AccountID = addTransactionWithdrawalDTO.AccountID;
            transaction.BeneficiaryID = null;
        }

        public Transactions GetTransaction()
        {
            return transaction;
        }
    }
}
