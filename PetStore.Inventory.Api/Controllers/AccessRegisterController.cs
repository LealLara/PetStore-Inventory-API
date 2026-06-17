using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Application.BusinessDTOs.Results;
using PetStore.Inventory.Application.Interfaces.Services;

namespace PetStore.Inventory.Application.Services
{
    [ApiController]
    [Route("[controller]")]
    public class AccessRegisterController : ControllerBase
    { 
        private readonly IAccessRegisterServices _service;

        public AccessRegisterController(IAccessRegisterServices service)
        {
            _service = service;
        }

        [HttpPost("user-register")]
        public async Task<IActionResult> CreateUserRegister(UserFirstRegisterDTO data)
        {
            try
            {
                bool success = await _service.CreateAccessRegister(data.ToBusinessRequest());

                if (!success)
                {
                    return BadRequest("O cadastro do usuário falhou.");
                }

                return Ok("Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao cadastrar o usuário: {ex.Message}");
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
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao fazer o login do usuário: {ex.Message}");
            }
        }

        [HttpPost("logoff")]
        public async Task<IActionResult> Logoff(int userId)
        {
            try
            {
                bool success = await _service.Logoff(userId);

                return success ? Ok(StatusCodes.Status200OK) : NotFound("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao fazer o logoff do usuário: {ex.Message}");
            }
        }

        [HttpPost("delete-account")]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            try
            {
                bool success = await _service.RemoveUser(userId);

                return success ? Ok(StatusCodes.Status200OK) : NotFound("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao deletar a conta: {ex.Message}");
            }
        }
    }
}