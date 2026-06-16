using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Domain.Interfaces.Services;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessConfigController : ControllerBase
    {
        private readonly IAccessConfigServices _service;
        public AccessConfigController(IAccessConfigServices service)
        {
            _service = service;
        }

        [HttpPost("start-app")]
        public async Task<IActionResult> StartApp()
        {
            try
            {
                bool success = await _service.StartApp();

                return success ? Ok(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError, "Failed to start the application.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while starting the application: {ex.Message}");
            }
        }
    }
}