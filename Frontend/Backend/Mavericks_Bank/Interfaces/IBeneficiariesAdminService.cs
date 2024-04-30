using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IBeneficiariesAdminService:IBeneficiariesUserService
    {
        public Task<List<Beneficiaries>> GetAllBeneficiaries();
        public Task<Beneficiaries> GetBeneficiary(int beneficiaryID);
    }
}
