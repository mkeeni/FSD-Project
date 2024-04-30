using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface IBranchesAdminService:IBranchesUserService
    {
        public Task<Branches> AddBranch(Branches branch);
        public Task<Branches> UpdateBranchName(UpdateBranchNameDTO updateBranchNameDTO);
        public Task<Branches> DeleteBranch(int branchID);
    }
}
