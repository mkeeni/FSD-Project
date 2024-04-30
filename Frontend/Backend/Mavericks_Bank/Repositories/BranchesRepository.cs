using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class BranchesRepository : IRepository<int, Branches>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<BranchesRepository> _loggerBranchesRepository;

        public BranchesRepository(MavericksBankContext mavericksBankContext, ILogger<BranchesRepository> loggerBranchesRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerBranchesRepository = loggerBranchesRepository;
        }

        public async Task<Branches> Add(Branches item)
        {
            _mavericksBankContext.Branches.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerBranchesRepository.LogInformation($"Added New Branch : {item.BranchID}");
            return item;
        }

        public async Task<Branches?> Delete(int key)
        {
            var foundBranch = await Get(key);
            if (foundBranch == null)
            {
                return null;
            }
            else
            {
                _mavericksBankContext.Branches.Remove(foundBranch);
                await _mavericksBankContext.SaveChangesAsync();
                _loggerBranchesRepository.LogInformation($"Deteled Branch : {foundBranch.BranchID}");
                return foundBranch;
            }
        }

        public async Task<Branches?> Get(int key)
        {
            var foundBranch = await _mavericksBankContext.Branches.Include(branch => branch.Banks).FirstOrDefaultAsync(branch => branch.BranchID == key);
            if (foundBranch == null)
            {
                return null;
            }
            else
            {
                _loggerBranchesRepository.LogInformation($"Founded Branch : {foundBranch.BranchID}");
                return foundBranch;
            }
        }

        public async Task<List<Branches>?> GetAll()
        {
            var allBranches = await _mavericksBankContext.Branches.Include(branch => branch.Banks).ToListAsync();
            if (allBranches.Count == 0)
            {
                return null;
            }
            else
            {
                _loggerBranchesRepository.LogInformation($"All Branches Returned");
                return allBranches;
            }
        }

        public async Task<Branches> Update(Branches item)
        {
            _mavericksBankContext.Entry<Branches>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerBranchesRepository.LogInformation($"Updated Branch : {item.BranchID}");
            return item;
        }
    }
}
