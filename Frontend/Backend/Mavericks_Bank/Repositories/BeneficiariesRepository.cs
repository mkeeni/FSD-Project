using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class BeneficiariesRepository : IRepository<int, Beneficiaries>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<BeneficiariesRepository> _loggerBeneficiariesRepository;

        public BeneficiariesRepository(MavericksBankContext mavericksBankContext, ILogger<BeneficiariesRepository> loggerBeneficiariesRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerBeneficiariesRepository = loggerBeneficiariesRepository;
        }

        public async Task<Beneficiaries> Add(Beneficiaries item)
        {
            _mavericksBankContext.Beneficiaries.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerBeneficiariesRepository.LogInformation($"Added New Beneficiary : {item.BeneficiaryID}");
            return item;
        }

        public async Task<Beneficiaries?> Delete(int key)
        {
            var foundBeneficiary = await Get(key);
            if (foundBeneficiary == null)
            {
                return null;
            }
            else
            {
                _mavericksBankContext.Beneficiaries.Remove(foundBeneficiary);
                await _mavericksBankContext.SaveChangesAsync();
                _loggerBeneficiariesRepository.LogInformation($"Deleted Beneficiary : {foundBeneficiary.BeneficiaryID}");
                return foundBeneficiary;
            }
        }

        public async Task<Beneficiaries?> Get(int key)
        {
            var foundBeneficiary = await _mavericksBankContext.Beneficiaries.Include(beneficiary => beneficiary.Branches).ThenInclude(branches => branches!.Banks).Include(beneficiary => beneficiary.Customers).FirstOrDefaultAsync(beneficiary => beneficiary.BeneficiaryID == key);
            if (foundBeneficiary == null)
            {
                return null;
            }
            else
            {
                _loggerBeneficiariesRepository.LogInformation($"Founded Beneficiary : {foundBeneficiary.BeneficiaryID}");
                return foundBeneficiary;
            }
        }

        public async Task<List<Beneficiaries>?> GetAll()
        {
            var allBeneficiaries = await _mavericksBankContext.Beneficiaries.Include(beneficiary => beneficiary.Branches).ThenInclude(branches => branches!.Banks).Include(beneficiary => beneficiary.Customers).ToListAsync();
            if (allBeneficiaries.Count == 0)
            {
                return null;
            }
            else
            {
                _loggerBeneficiariesRepository.LogInformation("All Beneficiaries Returned");
                return allBeneficiaries;
            }
        }

        public async Task<Beneficiaries> Update(Beneficiaries item)
        {
            _mavericksBankContext.Entry<Beneficiaries>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerBeneficiariesRepository.LogInformation($"Updated Beneficiary : {item.BeneficiaryID}");
            return item;
        }
    }
}
