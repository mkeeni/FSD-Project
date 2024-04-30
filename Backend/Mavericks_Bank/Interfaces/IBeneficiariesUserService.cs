using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IBeneficiariesUserService
    {
        public Task<List<Beneficiaries>> GetAllCustomerBeneficiaries(int customerID);
        public Task<Beneficiaries> AddBeneficiary(Beneficiaries beneficiary);
        public Task<Beneficiaries> UpdateDeleteBeneficiary(int beneficiaryID);
        public Task<Beneficiaries> DeleteBeneficiary(int beneficiaryID);
    }
}
