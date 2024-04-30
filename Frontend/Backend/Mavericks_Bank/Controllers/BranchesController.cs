using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchesAdminService _branchesService;
        private readonly ILogger<BranchesController> _loggerBranchesController;

        public BranchesController(IBranchesAdminService branchesService, ILogger<BranchesController> loggerBranchesController)
        {
            _branchesService = branchesService;
            _loggerBranchesController = loggerBranchesController;
        }

        [Authorize]
        [Route("GetAllBranches")]
        [HttpGet]
        public async Task<ActionResult<List<Branches>>> GetAllBranches()
        {
            try
            {
                return await _branchesService.GetAllBranches();
            }
            catch (NoBranchesFoundException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [Route("GetAllSpecificBranches")]
        [HttpGet]
        public async Task<ActionResult<List<Branches>>> GetAllSpecificBranches(int bankID)
        {
            try
            {
                return await _branchesService.GetAllSpecificBranches(bankID);
            }
            catch (NoBranchesFoundException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [Route("GetBranch")]
        [HttpGet]
        public async Task<ActionResult<Branches>> GetBranch(int branchID)
        {
            try
            {
                return await _branchesService.GetBranch(branchID);
            }
            catch (NoBranchesFoundException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("AddBranch")]
        [HttpPost]
        public async Task<ActionResult<Branches>> AddBranch(Branches branch)
        {
            try
            {
                return await _branchesService.AddBranch(branch);
            }
            catch (BranchAlreadyExistsException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdateBranchName")]
        [HttpPut]
        public async Task<ActionResult<Branches>> UpdateBranchName(UpdateBranchNameDTO updateBranchNameDTO)
        {
            try
            {
                return await _branchesService.UpdateBranchName(updateBranchNameDTO);
            }
            catch (NoBranchesFoundException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (BranchAlreadyExistsException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteBranch")]
        [HttpDelete]
        public async Task<ActionResult<Branches>> DeleteBranch(int branchID)
        {
            try
            {
                return await _branchesService.DeleteBranch(branchID);
            }
            catch (NoBranchesFoundException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
