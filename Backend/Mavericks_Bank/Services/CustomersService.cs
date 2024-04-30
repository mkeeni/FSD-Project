using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class CustomersService : ICustomersAdminService
    {
        private readonly IRepository<int, Customers> _customersRepository;
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly ILogger<CustomersService> _loggerCustomersService;

        public CustomersService(IRepository<int, Customers> customersRepository, IRepository<string, Validation> validationRepository, ILogger<CustomersService> loggerCustomersService)
        {
            _customersRepository = customersRepository;
            _validationRepository = validationRepository;
            _loggerCustomersService = loggerCustomersService;
        }

        public async Task<Customers> DeleteCustomer(int customerID)
        {
            var deletedCustomer = await _customersRepository.Delete(customerID);
            if(deletedCustomer == null)
            {
                throw new NoCustomersFoundException($"Customer ID {customerID} not found");
            }
            await _validationRepository.Delete(deletedCustomer.Email);
            _loggerCustomersService.LogInformation($"Successfully Deleted the Customer : {deletedCustomer.CustomerID}");
            return deletedCustomer;
        }

        public async Task<List<Customers>> GetAllCustomers()
        {
            var allCustomers = await _customersRepository.GetAll();
            if(allCustomers == null)
            {
                throw new NoCustomersFoundException("No Available Customer Data");
            }
            return allCustomers;
        }

        public async Task<Customers> GetCustomer(int customerID)
        {
            var foundCustomer = await _customersRepository.Get(customerID);
            if(foundCustomer == null)
            {
                throw new NoCustomersFoundException($"Customer ID {customerID} not found");
            }
            return foundCustomer;
        }

        public async Task<Customers> GetCustomerByEmail(string email)
        {
            var allCustomers = await GetAllCustomers();
            var foundCustomer = allCustomers.FirstOrDefault(customer => customer.Email == email);
            if(foundCustomer == null)
            {
                throw new NoCustomersFoundException($"Customer with email ID {email} not found");
            }
            return foundCustomer;
        }

        public async Task<Customers> UpdateCustomerDetails(UpdateCustomerDTO updateCustomerDTO)
        {
            var foundCustomer = await GetCustomer(updateCustomerDTO.CustomerID);
            foundCustomer.Name = updateCustomerDTO.Name;
            foundCustomer.DOB = updateCustomerDTO.DOB;
            foundCustomer.Age = updateCustomerDTO.Age;
            foundCustomer.PhoneNumber = updateCustomerDTO.PhoneNumber;
            foundCustomer.Address = updateCustomerDTO.Address;
            foundCustomer.AadharNumber = updateCustomerDTO.AadharNumber;
            foundCustomer.PANNumber = updateCustomerDTO.PANNumber;
            foundCustomer.Gender = updateCustomerDTO.Gender;
            var updatedCustomer = await _customersRepository.Update(foundCustomer);
            _loggerCustomersService.LogInformation($"Successfully Updated the Customer Details: {foundCustomer.CustomerID}");
            return updatedCustomer;
        }
    }
}
