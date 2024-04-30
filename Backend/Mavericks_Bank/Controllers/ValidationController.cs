using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class ValidationController : ControllerBase
    {
        private readonly IValidationAdminService _validationService;
        private readonly ILogger<ValidationController> _loggerValidationController;

        public ValidationController(IValidationAdminService validationService, ILogger<ValidationController> loggerValidationController)
        {
            _validationService = validationService;
            _loggerValidationController = loggerValidationController;
        }

        [Authorize(Roles = "Admin")]
        [Route("GetAllValidations")]
        [HttpGet]
        public async Task<ActionResult<List<Validation>>> GetAllValidations()
        {
            try
            {
                return await _validationService.GetAllValidations();
            }
            catch (NoValidationFoundException e)
            {
                _loggerValidationController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<LoginValidationDTO>> Login(LoginValidationDTO loginValidationDTO)
        {
            try
            {
                return await _validationService.Login(loginValidationDTO);
            }
            catch (NoValidationFoundException e)
            {
                _loggerValidationController.LogInformation(e.Message);
                return Unauthorized(e.Message);
            }
        }
        
        
        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<ActionResult<LoginValidationDTO>> ForgotPassword(LoginValidationDTO loginValidationDTO)
        {
            try
            {
                return await _validationService.ForgotPassword(loginValidationDTO);
            }
            catch (NoValidationFoundException e)
            {
                _loggerValidationController.LogInformation(e.Message);
                return Unauthorized(e.Message);
            }
            catch (ValidationAlreadyExistsException e)
            {
                _loggerValidationController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        
        [Route("RegisterAdmin")]
        [HttpPost]
        public async Task<ActionResult<LoginValidationDTO>> RegisterAdmin(RegisterValidationAdminDTO registerValidationAdminDTO)
        {
            try
            {
                return await _validationService.RegisterAdmin(registerValidationAdminDTO);
            }
            catch (ValidationAlreadyExistsException e)
            {
                _loggerValidationController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        
        [Route("RegisterBankEmployees")]
        [HttpPost]
        public async Task<ActionResult<LoginValidationDTO>> RegisterBankEmployees(RegisterValidationBankEmployees registerValidationBankEmployees)
        {
            try
            {
                return await _validationService.RegisterBankEmployees(registerValidationBankEmployees);
            }
            catch (ValidationAlreadyExistsException e)
            {
                _loggerValidationController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        
        [Route("RegisterCustomers")]
        [HttpPost]
        public async Task<ActionResult<LoginValidationDTO>> RegisterCustomers(RegisterValidationCustomersDTO registerValidationCustomersDTO)
        {
            try
            {
                return await _validationService.RegisterCustomers(registerValidationCustomersDTO);
            }
            catch (ValidationAlreadyExistsException e)
            {
                _loggerValidationController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
            catch (CustomerAlreadyExistsException e)
            {
                _loggerValidationController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdateValidationStatus")]
        [HttpPut]
        public async Task<ActionResult<UpdatedValidationDTO>> UpdateValidationStatus(string email)
        {
            try
            {
                return await _validationService.UpdateValidationStatus(email);
            }
            catch (NoValidationFoundException e)
            {
                _loggerValidationController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
