using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class CustomersRepository : IRepository<int, Customers>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<CustomersRepository> _loggerCustomersRepository;

        public CustomersRepository(MavericksBankContext mavericksBankContext, ILogger<CustomersRepository> loggerCustomersRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerCustomersRepository = loggerCustomersRepository;
        }

        public async Task<Customers> Add(Customers item)
        {
            _mavericksBankContext.Customers.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerCustomersRepository.LogInformation($"Added New Customer : {item.CustomerID}");
            return item;
        }

        public async Task<Customers?> Delete(int key)
        {
            var foundCustomer = await Get(key);
            if (foundCustomer == null)
            {
                _loggerCustomersRepository.LogInformation("Customer Not Found");
                return null;
            }
            else
            {
                _mavericksBankContext.Customers.Remove(foundCustomer);
                await _mavericksBankContext.SaveChangesAsync();
                _loggerCustomersRepository.LogInformation($"Deleted Customer : {foundCustomer.CustomerID}");
                return foundCustomer;
            }
        }

        public async Task<Customers?> Get(int key)
        {
            var foundCustomer = await _mavericksBankContext.Customers.FirstOrDefaultAsync(customer => customer.CustomerID == key);
            if (foundCustomer == null)
            {
                _loggerCustomersRepository.LogInformation("Customer Not Found");
                return null;
            }
            else
            {
                _loggerCustomersRepository.LogInformation($"Founded Customer : {foundCustomer.CustomerID}");
                return foundCustomer;
            }
        }

        public async Task<List<Customers>?> GetAll()
        {
            var allCustomers = await _mavericksBankContext.Customers.ToListAsync();
            if (allCustomers.Count == 0)
            {
                _loggerCustomersRepository.LogInformation("No Customers Returned");
                return null;
            }
            else
            {
                _loggerCustomersRepository.LogInformation("All Customers Returned");
                return allCustomers;
            }
        }

        public async Task<Customers> Update(Customers item)
        {
            _mavericksBankContext.Entry<Customers>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerCustomersRepository.LogInformation($"Updated Customer : {item.CustomerID}");
            return item;
        }
    }
}
