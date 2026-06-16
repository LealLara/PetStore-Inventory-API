using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _service;
        public RoleController(IRoleServices service)
        {
            _service = service;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var roles = await _service.GetAllRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving roles: {ex.Message}");
            }
        }

        [HttpGet("get-all-filtered-by-string")]
        public async Task<IActionResult> GetRolesFilteredByString(string filters)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _service.GetRolesFilteredByString(filters);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving roles: {ex.Message}");
            }
        }

        [HttpGet("get-all-filtered-by-id")]
        public async Task<IActionResult> GetRolesFilteredById(int filters)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _service.GetRolesFilteredById(filters);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving roles: {ex.Message}");
            }
        }

        [HttpPost("create-pattern-roles")]
        public async Task<IActionResult> CreatePatternRoles()
        {
            try
            {
                bool success = await _service.CreatePatternRoles();
                return success ? Ok(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError, "Failed to create pattern roles.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating pattern roles: {ex.Message}");
            }
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRoles(RoleDTO data)
        {
            try
            {
                bool success = await _service.CreateRole(data.ToBusinessRequest());
                return success ? Ok(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError, "Failed to create role.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating role: {ex.Message}");
            }
        }
    }
}