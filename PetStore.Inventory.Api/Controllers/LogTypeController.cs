using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using Swashbuckle.AspNetCore.Annotations;

namespace PetStore.Inventory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LogTypeController : ControllerBase
    {
        private readonly ILogTypeServices _service;
        public LogTypeController(ILogTypeServices service)
        {
            _service = service;
        }

        /// <summary>
        /// Endpoint que obtém todos os tipos de log disponíveis.
        /// </summary>
        /// <returns>Retorna uma lista de tipos de log disponíveis.</returns>
        [SwaggerOperation(Summary = "Obtém todos os tipos de log disponíveis", Description = "Retorna uma lista de tipos de log disponíveis.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de tipos de log retornada com sucesso", typeof(IEnumerable<LogTypeModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum tipo de log encontrado para o filtro especificado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao recuperar os tipos de log", typeof(string))]

        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<LogTypeModel> logTypes = await _service.GetAllLogTypes();

                if (logTypes == null || !logTypes.Any())
                    return NotFound("No log types found for the specified filter.");

                return Ok(logTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving log types: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que obtém tipos de log filtrados por uma string de filtro.
        /// </summary>
        /// <param name="filters">Parâmetro de filtro para os tipos de log.</param>
        /// <returns>Retorna uma lista de tipos de log filtrada.</returns>
        [SwaggerOperation(Summary = "Obtém tipos de log filtrados por string", Description = "Retorna uma lista de tipos de log filtrada por uma string de filtro.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de tipos de log filtrada retornada com sucesso", typeof(IEnumerable<LogTypeModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum tipo de log encontrado para o filtro especificado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao recuperar os tipos de log", typeof(string))]
        
        [ApiExplorerSettings(IgnoreApi = true)]
        
        [HttpGet("get-all-filtered-by-string")]
        public async Task<IActionResult> GetLogTypesFilteredByString(string filters)
        {
            try
            {
                IEnumerable<LogTypeModel> logTypes = await _service.GetLogTypesFilteredByString(filters);

                if (logTypes == null || !logTypes.Any())
                    return NotFound("No log types found for the specified filter.");

                return Ok(logTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving log types: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que obtém tipos de log filtrados por um ID de filtro.
        /// </summary>
        /// <param name="filters">Parâmetro de filtro para os tipos de log.</param>
        /// <returns>Retorna uma lista de tipos de log filtrada.</returns>
        [SwaggerOperation(Summary = "Obtém tipos de log filtrados por ID", Description = "Retorna uma lista de tipos de log filtrada por um ID de filtro.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de tipos de log filtrada retornada com sucesso", typeof(IEnumerable<LogTypeModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum tipo de log encontrado para o filtro especificado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao recuperar os tipos de log", typeof(string))]
       
        [ApiExplorerSettings(IgnoreApi = true)]
       
        [HttpGet("get-all-filtered-by-id")]
        public async Task<IActionResult> GetLogTypesFilteredById(int filters)
        {
            try
            {
                IEnumerable<LogTypeModel> logTypes = await _service.GetLogTypesFilteredById(filters);

                if (logTypes == null || !logTypes.Any())
                    return NotFound("No log types found for the specified filter.");

                return Ok(logTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving log types: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que cria um novo tipo de log com base nos dados fornecidos.
        /// </summary>
        /// <param name="data">Parâmetro contendo os dados para criação do tipo de log.</param>
        /// <returns>Retorna o resultado da operação de criação.</returns>
        [SwaggerOperation(Summary = "Cria tipos de log", Description = "Cria tipos de log no sistema")]
        [SwaggerResponse(StatusCodes.Status200OK, "Tipos de log criados com sucesso")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao criar os tipos de log", typeof(string))]
        
        [ApiExplorerSettings(IgnoreApi = true)]
       
        [HttpPost("create-log-type")]
        public async Task<IActionResult> CreateLogTypes(LogTypeDTO data)
        {
            try
            {
                bool success = await _service.CreateLogTypes(data.ToBusinessRequest());
                return success ? Ok(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError, "Failed to create log type.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating log type: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que cria tipos de log padrão pré-definidos.
        /// </summary>
        /// <returns>Retorna o resultado da operação de criação.</returns>
        [SwaggerOperation(Summary = "Cria tipos de log padrão", Description = "Cria tipos de log padrão pré-definidos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Tipos de log padrão criados com sucesso")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao criar os tipos de log padrão", typeof(string))]
        
        [ApiExplorerSettings(IgnoreApi = true)]
        
        [HttpPost("create-pattern-log-types")]
        public async Task<IActionResult> CreatePatternLogTypes()
        {
            try
            {
                bool success = await _service.CreatePatternLogTypes();
                return success ? Ok(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError, "Failed to create pattern log types.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating pattern log types: {ex.Message}");
            }
        }
    }
}