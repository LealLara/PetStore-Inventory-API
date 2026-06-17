using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices _authService;
        public LoginController(ILoginServices authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Autentica um usuário e retorna um token JWT se as credenciais forem válidas.
        /// </summary>
        /// <param name="dto">Objeto contendo as credenciais do usuário.</param>
        /// <returns>Token JWT se as credenciais forem válidas.</returns>
        [SwaggerOperation(Summary = "Autentica um usuário", Description = "Retorna um token JWT se as credenciais forem válidas")]
        [SwaggerResponse(StatusCodes.Status200OK, "Autenticação bem-sucedida, token JWT retornado")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Credenciais inválidas")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor")]
        
        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            try
            {
                string token = await _authService.Login(dto.ToBusinessRequest());
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("Credenciais inválidas.");
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// Realiza o logoff de um usuário, invalidando seu token JWT.
        /// </summary>
        /// <param name="userId">ID do usuário que deseja realizar o logoff.</param>
        /// <returns>Resultado da operação de logoff.</returns>
        [SwaggerOperation(Summary = "Realiza o logoff de um usuário", Description = "Invalida o token JWT do usuário")]
        [SwaggerResponse(StatusCodes.Status200OK, "Logoff realizado com sucesso")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não foi possível realizar o logoff")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor")]
        
        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpPost("logoff")]
        public async Task<IActionResult> Logoff(int userId)
        {
            try
            {
                bool result = await _authService.Logoff(userId);
                if (!result)
                {
                    return Unauthorized("Não foi possível realizar o logoff.");
                }
                return Ok("Logoff realizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        } 
    }
}