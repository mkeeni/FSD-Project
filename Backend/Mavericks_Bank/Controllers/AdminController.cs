using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _loggerAdminController;

        public AdminController(IAdminService adminService, ILogger<AdminController> loggerAdminController)
        {
            _adminService = adminService;
            _loggerAdminController = loggerAdminController;
        }

        [Authorize(Roles = "Admin")]
        [Route("GetAllAdmins")]
        [HttpGet]
        public async Task<ActionResult<List<Admin>>> GetAllAdmins()
        {
            try
            {
                return await _adminService.GetAllAdmins();
            }
            catch (NoAdminFoundException e)
            {
                _loggerAdminController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("GetAdmin")]
        [HttpGet]
        public async Task<ActionResult<Admin>> GetAdmin(int adminID)
        {
            try
            {
                return await _adminService.GetAdmin(adminID);
            }
            catch (NoAdminFoundException e)
            {
                _loggerAdminController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetAdminByEmail")]
        [HttpGet]
        public async Task<ActionResult<Admin>> GetAdminByEmail(string email)
        {
            try
            {
                return await _adminService.GetAdminByEmail(email);
            }
            catch (NoAdminFoundException e)
            {
                _loggerAdminController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdateAdminName")]
        [HttpPut]
        public async Task<ActionResult<Admin>> UpdateAdminName(UpdateAdminNameDTO updateAdminNameDTO)
        {
            try
            {
                return await _adminService.UpdateAdminName(updateAdminNameDTO);
            }
            catch (NoAdminFoundException e)
            {
                _loggerAdminController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
