using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class BankEmployeesRepository : IRepository<int, BankEmployees>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<BankEmployeesRepository> _loggerBankEmployeesRepository;

        public BankEmployeesRepository(MavericksBankContext mavericksBankContext, ILogger<BankEmployeesRepository> loggerBankEmployeesRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerBankEmployeesRepository = loggerBankEmployeesRepository;
        }

        public async Task<BankEmployees> Add(BankEmployees item)
        {
            _mavericksBankContext.BankEmployees.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerBankEmployeesRepository.LogInformation($"Added New Employee : {item.EmployeeID}");
            return item;
        }

        public async Task<BankEmployees?> Delete(int key)
        {
            var foundEmployee = await Get(key);
            if (foundEmployee == null)
            {
                return null;
            }
            else
            {
                _mavericksBankContext.BankEmployees.Remove(foundEmployee);
                await _mavericksBankContext.SaveChangesAsync();
                _loggerBankEmployeesRepository.LogInformation($"Deleted Employee : {foundEmployee.EmployeeID}");
                return foundEmployee;
            }
        }

        public async Task<BankEmployees?> Get(int key)
        {
            var foundEmployee = await _mavericksBankContext.BankEmployees.FirstOrDefaultAsync(employee => employee.EmployeeID == key);
            if (foundEmployee == null)
            {
                return null;
            }
            else
            {
                _loggerBankEmployeesRepository.LogInformation($"Founded Employee : {foundEmployee.EmployeeID}");
                return foundEmployee;
            }
        }

        public async Task<List<BankEmployees>?> GetAll()
        {
            var allEmployees = await _mavericksBankContext.BankEmployees.ToListAsync();
            if (allEmployees.Count == 0)
            {
                return null;
            }
            else
            {
                _loggerBankEmployeesRepository.LogInformation("All Employees Returned ");
                return allEmployees;
            }
        }

        public async Task<BankEmployees> Update(BankEmployees item)
        {
            _mavericksBankContext.Entry<BankEmployees>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerBankEmployeesRepository.LogInformation($"Updated Employee : {item.EmployeeID}");
            return item;
        }
    }
}
