using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccessConfigController : ControllerBase
    {
        private readonly IAccessConfigServices _service;
        public AccessConfigController(IAccessConfigServices service)
        {
            _service = service;
        }

        /// <summary>
        /// Endpoint que inicia a aplicação, criando os registros iniciais de papéis, tipos de log.
        /// </summary>
        /// <returns>Resultado da operação</returns
        [SwaggerOperation(Summary = "Inicia a aplicação", Description = "Cria os registros iniciais de papéis, tipos de log e outras configurações necessárias para o funcionamento da aplicação.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A aplicação foi iniciada com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao iniciar a aplicação.")]
        [AllowAnonymous]
        [HttpPost("start-app")]
        public async Task<IActionResult> StartApp()
        {
            try
            {
                bool success = await _service.StartApp();

                return success ? Ok(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError, "Inicialização da aplicação falhou.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao iniciar a aplicação: {ex.Message}");
            }
        }
    }
}