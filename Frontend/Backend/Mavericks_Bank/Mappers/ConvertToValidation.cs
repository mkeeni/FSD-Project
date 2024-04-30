using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToValidation
    {
        Validation validation;

        public ConvertToValidation(RegisterValidationCustomersDTO registerValidationCustomersDTO)
        {
            validation = new Validation();
            validation.Email = registerValidationCustomersDTO.Email;
            validation.UserType = "Customer";
            validation.Status = null;
            GenerateEncryptedPassword(registerValidationCustomersDTO.Password);
        }

        public ConvertToValidation(RegisterValidationBankEmployees registerValidationBankEmployees)
        {
            validation = new Validation();
            validation.Email = registerValidationBankEmployees.Email;
            validation.UserType = "Employee";
            validation.Status = null;
            GenerateEncryptedPassword(registerValidationBankEmployees.Password);
        }

        public ConvertToValidation(RegisterValidationAdminDTO registerValidationAdminDTO)
        {
            validation = new Validation();
            validation.Email = registerValidationAdminDTO.Email;
            validation.UserType = "Admin";
            validation.Status = null;
            GenerateEncryptedPassword(registerValidationAdminDTO.Password);
        }

        void GenerateEncryptedPassword(string password)
        {
            HMACSHA512 hMACSHA512 = new HMACSHA512();
            validation.Key = hMACSHA512.Key;
            validation.Password = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public Validation GetValidation()
        {
            return validation;
        }
    }
}
