using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class BeneficiariesController : ControllerBase
    {
        private readonly IBeneficiariesAdminService _beneficiariesService;
        private readonly ILogger<BeneficiariesController> _loggerBeneficiariesController;

        public BeneficiariesController(IBeneficiariesAdminService beneficiariesService, ILogger<BeneficiariesController> loggerBeneficiariesController)
        {
            _beneficiariesService = beneficiariesService;
            _loggerBeneficiariesController = loggerBeneficiariesController;
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("GetAllBeneficiaries")]
        [HttpGet]
        public async Task<ActionResult<List<Beneficiaries>>> GetAllBeneficiaries()
        {
            try
            {
                return await _beneficiariesService.GetAllBeneficiaries();
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [Route("GetAllCustomerBeneficiaries")]
        [HttpGet]
        public async Task<ActionResult<List<Beneficiaries>>> GetAllCustomerBeneficiaries(int customerID)
        {
            try
            {
                return await _beneficiariesService.GetAllCustomerBeneficiaries(customerID);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("GetBeneficiary")]
        [HttpGet]
        public async Task<ActionResult<Beneficiaries>> GetBeneficiary(int beneficiaryID)
        {
            try
            {
                return await _beneficiariesService.GetBeneficiary(beneficiaryID);
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [Route("AddBeneficiary")]
        [HttpPost]
        public async Task<ActionResult<Beneficiaries>> AddBeneficiary(Beneficiaries beneficiary)
        {
            try
            {
                return await _beneficiariesService.AddBeneficiary(beneficiary);
            }
            catch (BeneficiaryAlreadyExistsException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("UpdateDeleteBeneficiary")]
        [HttpPut]
        public async Task<ActionResult<Beneficiaries>> UpdateDeleteBeneficiary(int beneficiaryID)
        {
            try
            {
                return await _beneficiariesService.UpdateDeleteBeneficiary(beneficiaryID);
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [Route("DeleteBeneficiary")]
        [HttpDelete]
        public async Task<ActionResult<Beneficiaries>> DeleteBeneficiary(int beneficiaryID)
        {
            try
            {
                return await _beneficiariesService.DeleteBeneficiary(beneficiaryID);
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
