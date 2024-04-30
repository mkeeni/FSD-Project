using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToBeneficiaries
    {
        Beneficiaries beneficiary;

        public ConvertToBeneficiaries(AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO)
        {
            beneficiary = new Beneficiaries();
            beneficiary.AccountNumber = addTransactionTransferBeneficiaryDTO.BeneficiaryAccountNumber;
            beneficiary.Name = addTransactionTransferBeneficiaryDTO.BeneficiaryName;
            beneficiary.Status = null;
            beneficiary.BranchID = addTransactionTransferBeneficiaryDTO.BranchID;
            beneficiary.CustomerID = addTransactionTransferBeneficiaryDTO .CustomerID;
        }

        public Beneficiaries GetBeneficiary()
        {
            return beneficiary;
        }
    }
}
