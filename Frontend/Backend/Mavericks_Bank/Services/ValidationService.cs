using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using Validation = Mavericks_Bank.Models.Validation;

namespace Mavericks_Bank.Services
{
    public class ValidationService : IValidationAdminService
    {
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly IRepository<int, Customers> _customersRepository;
        private readonly IRepository<int, BankEmployees> _bankEmployeesRepository;
        private readonly IRepository<int, Admin> _adminRepository;
        private readonly ILogger<ValidationService> _loggerValidationService;
        private readonly ITokenService _tokenService;

        public ValidationService(IRepository<string, Validation> validationRepository, IRepository<int, Customers> customersRepository, IRepository<int, BankEmployees> bankEmployeesRepository, IRepository<int, Admin> adminRepository, ILogger<ValidationService> loggerValidationService, ITokenService tokenService)
        {
            _validationRepository = validationRepository;
            _customersRepository = customersRepository;
            _bankEmployeesRepository = bankEmployeesRepository;
            _adminRepository = adminRepository;
            _loggerValidationService = loggerValidationService;
            _tokenService = tokenService;
        }

        public async Task<List<Validation>> GetAllValidations()
        {
            var allValidation = await _validationRepository.GetAll();
            if(allValidation == null)
            {
                throw new NoValidationFoundException("No Validation Data Found");
            }
            return allValidation;
        }

        /// <summary>
        /// Login for Customers,Admin,Employees
        /// </summary>
        /// <param name="loginValidationDTO">Object of LoginValidationDTO</param>
        /// <returns>LoginValidationDTO object</returns>
        /// <exception cref="NoValidationFoundException">throws exception if no airport found</exception>
        public async Task<LoginValidationDTO> Login(LoginValidationDTO loginValidationDTO)
        {
            var foundValidation = await _validationRepository.Get(loginValidationDTO.Email);
            if (foundValidation == null)
            {
                throw new NoValidationFoundException($"Email ID {loginValidationDTO.Email} not Found");
            }

            if(foundValidation.Status == null)
            {
                var convertedPassword = ConvertToEncryptedPassword(loginValidationDTO.Password, foundValidation.Key);
                var passwordMatch = IsPasswordMatches(convertedPassword, foundValidation.Password);
                if (passwordMatch)
                {
                    loginValidationDTO.Password = "";
                    loginValidationDTO.UserType = foundValidation.UserType;
                    loginValidationDTO.Token = _tokenService.GenerateToken(loginValidationDTO);
                    _loggerValidationService.LogInformation($"Successfully Logged in {loginValidationDTO.Email}");
                    return loginValidationDTO;
                }
                else
                {
                    throw new NoValidationFoundException($"Incorrect Password");
                }
            }
            else
            {
                throw new NoValidationFoundException($"Email Account {foundValidation.Email} was deactivated");
            }
        }

        /// <summary>
        /// Converting string to encrypted byte array password
        /// </summary>
        /// <param name="password">password as string</param>
        /// <param name="key">password key as byte</param>
        /// <returns>an encrypted byte[]</returns>
        private byte[] ConvertToEncryptedPassword(string password, byte[] key)
        {
            HMACSHA512 hmac = new HMACSHA512(key);
            var convertedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return convertedPassword;
        }

        /// <summary>
        /// Checking if encrypted password and existing password are similar
        /// </summary>
        /// <param name="convertedPassword">coverted password as byte</param>
        /// <param name="existingPassword">existing password as byte</param>
        /// <returns>true if password matches, else false</returns>
        private bool IsPasswordMatches(byte[] convertedPassword, byte[] existingPassword)
        {
            for (int i = 0; i < existingPassword.Length; i++)
            {
                if (convertedPassword[i] != existingPassword[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Registration of Admin
        /// </summary>
        /// <param name="registerValidationAdminDTO">Object of registerValidationAdminDTO</param>
        /// <returns>LoginValidationDTO object</returns>
        /// <exception cref="ValidationAlreadyExistsException">throws exception if no airport found</exception>
        public async Task<LoginValidationDTO> RegisterAdmin(RegisterValidationAdminDTO registerValidationAdminDTO)
        {
            var foundValidation = await _validationRepository.Get(registerValidationAdminDTO.Email);
            if (foundValidation != null)
            {
                throw new ValidationAlreadyExistsException($"Email ID {registerValidationAdminDTO.Email} already exists");
            }

            Validation newValidation = new ConvertToValidation(registerValidationAdminDTO).GetValidation();
            var addedValidation = await _validationRepository.Add(newValidation);

            Admin newAdmin = new ConvertToAdmin(registerValidationAdminDTO).GetAdmin();
            var addedAdmin = await _adminRepository.Add(newAdmin);

            _loggerValidationService.LogInformation($"Successfully Registered Admin {addedAdmin.Name}");

            LoginValidationDTO loginValidationDTO = new LoginValidationDTO { Email = addedValidation.Email, Password = "", UserType = addedValidation.UserType, Token = "" };
            return loginValidationDTO;
        }

        /// <summary>
        /// Registration of Bank Employees
        /// </summary>
        /// <param name="registerValidationBankEmployees">Object of registerValidationBankEmployees</param>
        /// <returns>LoginValidationDTO object</returns>
        /// <exception cref="ValidationAlreadyExistsException">throws exception if no airport found</exception>
        public async Task<LoginValidationDTO> RegisterBankEmployees(RegisterValidationBankEmployees registerValidationBankEmployees)
        {
            var foundValidation = await _validationRepository.Get(registerValidationBankEmployees.Email);
            if (foundValidation != null)
            {
                throw new ValidationAlreadyExistsException($"Email ID {registerValidationBankEmployees.Email} already exists");
            }

            Validation newValidation = new ConvertToValidation(registerValidationBankEmployees).GetValidation();
            var addedValidation = await _validationRepository.Add(newValidation);

            BankEmployees newBankEmployee = new ConvertToBankEmployees(registerValidationBankEmployees).GetBankEmployees();
            var addedEmployee = await _bankEmployeesRepository.Add(newBankEmployee);

            _loggerValidationService.LogInformation($"Successfully Registered Bank Employee {addedEmployee.Name}");

            LoginValidationDTO loginValidationDTO = new LoginValidationDTO { Email = addedValidation.Email, Password = "", UserType = addedValidation.UserType, Token = ""};
            return loginValidationDTO;
        }

        /// <summary>
        /// Registration of Customers
        /// </summary>
        /// <param name="registerValidationCustomersDTO">Object of registerValidationCustomersDTO</param>
        /// <returns>LoginValidationDTO object</returns>
        /// <exception cref="ValidationAlreadyExistsException">throws exception if no airport found</exception>
        /// <exception cref="CustomerAlreadyExistsException">throws exception if no airport found</exception>
        public async Task<LoginValidationDTO> RegisterCustomers(RegisterValidationCustomersDTO registerValidationCustomersDTO)
        {
            var foundValidation = await _validationRepository.Get(registerValidationCustomersDTO.Email);
            if (foundValidation != null)
            {
                throw new ValidationAlreadyExistsException($"Email ID {registerValidationCustomersDTO.Email} already exists");
            }

            Customers newCustomer = new ConvertToCustomers(registerValidationCustomersDTO).GetCustomer();
            var allCustomers = await _customersRepository.GetAll();
            if(allCustomers != null)
            {
                if(allCustomers.Contains(newCustomer))
                {
                    throw new CustomerAlreadyExistsException($"You already have an existing account");
                }
            }

            Validation newValidation = new ConvertToValidation(registerValidationCustomersDTO).GetValidation();
            var addedValidation = await _validationRepository.Add(newValidation);

            var addedCustomer = await _customersRepository.Add(newCustomer);

            _loggerValidationService.LogInformation($"Successfully Registered Customer {addedCustomer.Name}");

            LoginValidationDTO loginValidationDTO = new LoginValidationDTO { Email = addedValidation.Email, Password = "", UserType = addedValidation.UserType, Token = ""};
            return loginValidationDTO;
        }

        /// <summary>
        /// Forgot Password Method in case Customer,Employee or Admin Forgots Password
        /// </summary>
        /// <param name="loginValidationDTO">Object of loginValidationDTO</param>
        /// <returns>LoginValidationDTO object</returns>
        /// <exception cref="NoValidationFoundException">throws exception if no airport found</exception>
        /// <exception cref="ValidationAlreadyExistsException">throws exception if no airport found</exception>
        public async Task<LoginValidationDTO> ForgotPassword(LoginValidationDTO loginValidationDTO)
        {
            var foundValidation = await _validationRepository.Get(loginValidationDTO.Email);
            if (foundValidation == null)
            {
                throw new NoValidationFoundException($"Email ID {loginValidationDTO.Email} not Found");
            }

            var convertedPassword = ConvertToEncryptedPassword(loginValidationDTO.Password, foundValidation.Key);
            var passwordMatch = IsPasswordMatches(convertedPassword, foundValidation.Password);
            if (passwordMatch)
            {
                throw new ValidationAlreadyExistsException($"Entered existing password, Enter a new Password");
            }
            
            await GenerateEncryptedPassword(loginValidationDTO.Password, foundValidation);

            LoginValidationDTO loginValidation = new LoginValidationDTO { Email = foundValidation.Email, Password = "", UserType = foundValidation.UserType, Token = "" };
            return loginValidation;
        }

        /// <summary>
        /// Generating a new encrypted password
        /// </summary>
        /// <param name="password">password as string</param>
        /// <param name="foundValidation">Object of Validation</param>
        private async Task GenerateEncryptedPassword(string password,Validation foundValidation)
        {
            HMACSHA512 hMACSHA512 = new HMACSHA512();
            foundValidation.Key = hMACSHA512.Key;
            foundValidation.Password = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(password));
            await UpdateValidation(foundValidation);
        }

        /// <summary>
        /// Updating the new key and password
        /// </summary>
        /// <param name="foundValidation">Object of Validation</param>
        private async Task UpdateValidation(Validation foundValidation)
        {
            var updatedValidation = await _validationRepository.Update(foundValidation);
            _loggerValidationService.LogInformation($"Successfully Updated Customer Password {updatedValidation.Email}");
        }

        public async Task<UpdatedValidationDTO> UpdateValidationStatus(string email)
        {
            var allValidations = await GetAllValidations();
            var foundValidation = allValidations.FirstOrDefault(validation =>  validation.Email == email);
            if(foundValidation == null)
            {
                throw new NoValidationFoundException($"Validation email {email} not found");
            }
            foundValidation.Status = "Deleted";
            var updateValidation = await _validationRepository.Update(foundValidation);
            UpdatedValidationDTO updatedValidationDTO = new UpdatedValidationDTO 
            { 
                Email = updateValidation.Email,
                UserType = updateValidation.UserType,
                Status = updateValidation.Status
            };
            return updatedValidationDTO;
        }
    }
}
