using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class ValidationRepository : IRepository<string, Validation>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<ValidationRepository> _loggerValidationRepository;

        public ValidationRepository(MavericksBankContext mavericksBankContext, ILogger<ValidationRepository> loggerValidationRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerValidationRepository = loggerValidationRepository;
        }

        public async Task<Validation> Add(Validation item)
        {
            _mavericksBankContext.Validation.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerValidationRepository.LogInformation($"Added New Validation : {item.Email}");
            return item;
        }

        public async Task<Validation?> Delete(string key)
        {
            var foundValidation = await Get(key);
            if (foundValidation == null)
            {
                _loggerValidationRepository.LogInformation($"Validation Not Found");
                return null;
            }
            else
            {
                _mavericksBankContext.Validation.Remove(foundValidation);
                await _mavericksBankContext.SaveChangesAsync();
                _loggerValidationRepository.LogInformation($"Deleted Validation : {foundValidation.Email}");
                return foundValidation;
            }
        }

        public async Task<Validation?> Get(string key)
        {
            var foundValidation = await _mavericksBankContext.Validation.FirstOrDefaultAsync(validation => validation.Email == key);
            if (foundValidation == null)
            {
                _loggerValidationRepository.LogInformation("Validation Not Found");
                return null;
            }
            else
            {
                _loggerValidationRepository.LogInformation($"Founded Validation : {foundValidation.Email}");
                return foundValidation;
            }
        }

        public async Task<List<Validation>?> GetAll()
        {
            var allValidations = await _mavericksBankContext.Validation.ToListAsync();
            if(allValidations.Count == 0) 
            {
                _loggerValidationRepository.LogInformation("No Validations Returned");
                return null;
            }
            else
            {
                _loggerValidationRepository.LogInformation("All Validations Returned");
                return allValidations;
            }
        }

        public async Task<Validation> Update(Validation item)
        {
            _mavericksBankContext.Entry<Validation>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerValidationRepository.LogInformation($"Updated Validation : {item.Email}");
            return item;
        }
    }
}
