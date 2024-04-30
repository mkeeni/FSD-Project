using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ICustomersUserService
    {
        public Task<Customers> GetCustomerByEmail(string email);
        public Task<Customers> UpdateCustomerDetails(UpdateCustomerDTO updateCustomerDTO);
        public Task<Customers> DeleteCustomer(int customerID);
    }
}
