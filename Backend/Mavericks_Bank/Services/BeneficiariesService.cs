using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;

namespace Mavericks_Bank.Services
{
    public class BeneficiariesService : IBeneficiariesAdminService
    {
        private readonly IRepository<int, Beneficiaries> _beneficiariesRepository;
        private readonly ICustomersAdminService _customersService;
        private readonly ILogger<BeneficiariesService> _loggerBeneficiariesService;

        public BeneficiariesService(IRepository<int, Beneficiaries> beneficiariesRepository, ICustomersAdminService customersService, ILogger<BeneficiariesService> loggerBeneficiariesService)
        {
            _beneficiariesRepository = beneficiariesRepository;
            _customersService = customersService;
            _loggerBeneficiariesService = loggerBeneficiariesService;
        }

        public async Task<Beneficiaries> AddBeneficiary(Beneficiaries beneficiary)
        {
            var allBeneficiaries = await _beneficiariesRepository.GetAll();
            var foundBeneficiary = allBeneficiaries?.FirstOrDefault(beneficiaries => beneficiaries.AccountNumber == beneficiary.AccountNumber && beneficiaries.CustomerID == beneficiary.CustomerID);
            if(foundBeneficiary != null && foundBeneficiary.Status != "Deleted")
            {
                throw new BeneficiaryAlreadyExistsException($"Beneficiary Account Number {beneficiary.AccountNumber} already exists");
            }
            else if (foundBeneficiary != null && foundBeneficiary.Status == "Deleted")
            {
                foundBeneficiary.Name = beneficiary.Name;
                foundBeneficiary.Status = null;
                var updateBeneficiary = await _beneficiariesRepository.Update(foundBeneficiary);
                return updateBeneficiary;
            }
            else
            {
                return await _beneficiariesRepository.Add(beneficiary);
            }
        }

        public async Task<Beneficiaries> DeleteBeneficiary(int beneficiaryID)
        {
            var deletedBeneficiary = await _beneficiariesRepository.Delete(beneficiaryID);
            if(deletedBeneficiary == null)
            {
                throw new NoBeneficiariesFoundException($"Beneficiary ID {beneficiaryID} not found");
            }
            return deletedBeneficiary;
        }

        public async Task<List<Beneficiaries>> GetAllBeneficiaries()
        {
            var allBeneficiaries = await _beneficiariesRepository.GetAll();
            if(allBeneficiaries == null)
            {
                throw new NoBeneficiariesFoundException("No Available Beneficiaries Data");
            }
            return allBeneficiaries;
        }

        public async Task<List<Beneficiaries>> GetAllCustomerBeneficiaries(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allBeneficiaries = await GetAllBeneficiaries();
            var allCustomerBeneficiaries = allBeneficiaries.Where(beneficiary => beneficiary.CustomerID ==  customerID && beneficiary.Status == null).ToList();
            if( allCustomerBeneficiaries.Count == 0)
            {
                throw new NoBeneficiariesFoundException($"No Beneficiaries found for Customer ID {customerID}");
            }
            return allCustomerBeneficiaries;
        }

        public async Task<Beneficiaries> GetBeneficiary(int beneficiaryID)
        {
            var foundBeneficiary = await _beneficiariesRepository.Get(beneficiaryID);
            if(foundBeneficiary == null)
            {
                throw new NoBeneficiariesFoundException($"Beneficiary ID {beneficiaryID} not found");
            }
            return foundBeneficiary;
        }

        public async Task<Beneficiaries> UpdateDeleteBeneficiary(int beneficiaryID)
        {
            var foundBeneficiary = await GetBeneficiary(beneficiaryID);
            foundBeneficiary.Status = "Deleted";
            var updateBeneficiary = await _beneficiariesRepository.Update(foundBeneficiary);
            return updateBeneficiary;
        }
    }
}
