using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests; 
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using PetStore.Inventory.Domain.Utils.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [AllowAnonymous]
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
        /// Retorna todos os logins cadastrados no sistema. Apenas usuários com a função ADMIN podem acessar este endpoint.
        /// </summary>
        /// <returns>Lista de logins cadastrados.</returns>
        [SwaggerOperation(Summary = "Retorna todos os logins cadastrados", Description = "Apenas usuários com a função ADMIN podem acessar este endpoint")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de logins retornada com sucesso", typeof(IEnumerable<LoginModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Credenciais inválidas")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Acesso negado. Apenas usuários com a função ADMIN podem acessar este endpoint")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpGet("get-all-logins")]
        public async Task<IActionResult> GetAllLogins()
        {
            try
            {
                IEnumerable<LoginModel> logins = await _authService.GetAllLogins();
                return Ok(logins);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}