using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IBranchesUserService
    {
        public Task<List<Branches>> GetAllSpecificBranches(int bankID);
        public Task<List<Branches>> GetAllBranches();
        public Task<Branches> GetBranch(int branchID);
    }
}
