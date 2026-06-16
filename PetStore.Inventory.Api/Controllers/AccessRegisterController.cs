using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Application.BusinessDTOs.Results;
using PetStore.Inventory.Application.Interfaces.Services;

namespace PetStore.Inventory.Api.Controllers
{
    public class AccessRegisterController : ControllerBase
    {
        private readonly ILogger<AccessRegisterController> _logger;
        private readonly IAccessRegisterServices _service;

        public AccessRegisterController(IAccessRegisterServices service)
        {
            _service = service;
        }

        [HttpPost("user-first-register")]
        public async Task<IActionResult> CreateFirstRegister(UserFirstRegisterDTO data)
        {
            try
            {
                DataRegisterResult result = await _service.CreateAccessRegister(data.ToBusiness());

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while registering the user: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDTO data)
        {
            try
            {
                DataRegisterResult result = await _service.Login(data.ToBusiness());

                if (result is null)
                {
                    return NotFound("User not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while logging in the user: {ex.Message}");
            }
        }

        [HttpPost("logoff")]
        public async Task<IActionResult> Logoff(int userId)
        {
            try
            {
                bool success = await _service.Logoff(userId);

                return success ? Ok(StatusCodes.Status200OK) : NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while logging off the user: {ex.Message}");
            }
        }

        [HttpPost("delete-account")]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            try
            {
                bool success = await _service.RemoveUser(userId);

                return success ? Ok(StatusCodes.Status200OK) : NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting the account: {ex.Message}");
            }
        }
    }
}