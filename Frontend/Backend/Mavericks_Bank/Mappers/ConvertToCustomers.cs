using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToCustomers
    {
        Customers customer;

        public ConvertToCustomers(RegisterValidationCustomersDTO registerValidationCustomersDTO)
        {
            customer = new Customers();
            customer.Name = registerValidationCustomersDTO.Name;
            customer.DOB = registerValidationCustomersDTO.DOB;
            customer.Age = registerValidationCustomersDTO.Age;
            customer.PhoneNumber = registerValidationCustomersDTO.PhoneNumber;
            customer.Address = registerValidationCustomersDTO.Address;
            customer.AadharNumber = registerValidationCustomersDTO.AadharNumber;
            customer.PANNumber = registerValidationCustomersDTO.PANNumber;
            customer.Gender = registerValidationCustomersDTO.Gender;
            customer.Email = registerValidationCustomersDTO.Email;
        }

        public Customers GetCustomer()
        {
            return customer;
        }
    }
}
