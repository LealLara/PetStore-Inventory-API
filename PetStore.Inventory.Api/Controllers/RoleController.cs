using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Application.Interfaces.Services;
using PetStore.Inventory.Domain.BusinessModel;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Endpoint que retorna uma lista de papéis (roles) disponíveis no sistema. O serviço de papéis é chamado para processar a solicitação e recuperar os papéis do banco de dados. Se a operação for bem-sucedida, o endpoint retorna um status 200 OK com a lista de papéis; caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.
        /// </summary>
        /// <returns>Retorna uma lista de papéis disponíveis.</returns>
        [SwaggerOperation(Summary = "Retorna uma lista de papéis (roles) disponíveis no sistema.", Description = "O serviço de papéis é chamado para processar a solicitação e recuperar os papéis do banco de dados. Se a operação for bem-sucedida, o endpoint retorna um status 200 OK com a lista de papéis; caso contrário, ou 404 se nenhum for encontrado; Caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Papéis retornados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum papel encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Falha ao recuperar os papéis.")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<RoleModel> roles = await _service.GetAllRoles();

                if (roles == null || !roles.Any())
                    return NotFound("Nenhum papel encontrado.");

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao recuperar os papéis: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que retorna uma lista de papéis (roles) filtrados por um valor string. O filtro é aplicado com base em um critério específico, como o nome do papel ou outro atributo relevante. O serviço de papéis é chamado para processar a solicitação e retornar os papéis que correspondem ao filtro fornecido. Se a operação for bem-sucedida, o endpoint retorna um status 200 OK com a lista de papéis filtrados; caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.
        /// </summary>
        /// <param name="filters">O valor string usado para filtrar os papéis.</param>
        /// <returns>Retorna uma lista de papéis filtrados.</returns>
        [SwaggerOperation(Summary = "Retorna uma lista de papéis (roles) filtrados por um valor string.", Description = "O filtro é aplicado com base em um critério específico, como o nome do papel ou outro atributo relevante. O serviço de papéis é chamado para processar a solicitação e retornar os papéis que correspondem ao filtro fornecido. Se a operação for bem-sucedida, o endpoint retorna um status 200 OK com a lista de papéis filtrados, ou 404 se nenhum for encontrado; Caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Papéis filtrados retornados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum papel encontrado para o filtro especificado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Falha ao recuperar os papéis filtrados.")]

        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpGet("get-all-filtered-by-string")]
        public async Task<IActionResult> GetRolesFilteredByString(string filters)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _service.GetRolesFilteredByString(filters);

                if (roles == null || !roles.Any())
                    return NotFound("Nenhum papel encontrado para o filtro especificado.");

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao recuperar os papéis filtrados: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que retorna uma lista de papéis (roles) filtrados por um valor inteiro. O filtro é aplicado com base em um critério específico, como o ID do papel ou outro atributo numérico relevante. O serviço de papéis é chamado para processar a solicitação e retornar os papéis que correspondem ao filtro fornecido. Se a operação for bem-sucedida, o endpoint retorna um status 200 OK com a lista de papéis filtrados; caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.
        /// </summary>
        /// <param name="filters">O valor inteiro usado para filtrar os papéis.</param>
        /// <returns>Retorna uma lista de papéis filtrados.</returns>
        [SwaggerOperation(Summary = "Retorna uma lista de papéis (roles) filtrados por um valor inteiro.", Description = "O filtro é aplicado com base em um critério específico, como o ID do papel ou outro atributo numérico relevante. O serviço de papéis é chamado para processar a solicitação e retornar os papéis que correspondem ao filtro fornecido. Se a operação for bem-sucedida, o endpoint retorna um status 200 OK com a lista de papéis filtrados, ou 404 se nenhum for encontrado; Caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Papéis filtrados retornados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum papel encontrado para o filtro especificado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Falha ao recuperar os papéis filtrados.")]

        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpGet("get-all-filtered-by-id")]
        public async Task<IActionResult> GetRolesFilteredById(int filters)
        {
            try
            {
                IEnumerable<RoleModel> roles = await _service.GetRolesFilteredById(filters);
               
                if (roles == null || !roles.Any())
                    return NotFound("Nenhum papel encontrado para o filtro especificado.");
                
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao recuperar os papéis filtrados: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que cria um novo papel (role) no sistema. O papel é uma entidade que representa um conjunto de permissões ou responsabilidades dentro da aplicação. Este endpoint recebe um objeto RoleDTO contendo as informações necessárias para criar o papel, como o nome e a descrição do papel. O serviço de criação de papéis é chamado para processar a solicitação e criar o novo papel no banco de dados. Se a criação for bem-sucedida, o endpoint retorna um status 200 OK; caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.
        /// </summary>
        /// <param name="data">O objeto RoleDTO contendo as informações para criar o papel. </param>
        /// <returns>Retorna um status 200 OK se a criação for bem-sucedida, caso contrário retorna um status 500 Internal Server Error. </returns>
        [SwaggerOperation(Summary = "Cria um novo papel (role) no sistema.", Description = "O papel é uma entidade que representa um conjunto de permissões ou responsabilidades dentro da aplicação. Este endpoint recebe um objeto RoleDTO contendo as informações necessárias para criar o papel, como o nome e a descrição do papel. O serviço de criação de papéis é chamado para processar a solicitação e criar o novo papel no banco de dados. Se a criação for bem-sucedida, o endpoint retorna um status 200 OK; Caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Papel criado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Falha ao criar o papel.")]

        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRoles(RoleDTO data)
        {
            try
            {
                bool success = await _service.CreateRole(data.ToBusinessRequest());
                return success ? Ok(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError, "Falha ao criar o papel.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao criar o papel: {ex.Message}");
            }
        }
        /// <summary>
        /// Endpoint que cria os papéis padrões no banco de dados, caso eles não existam. Este endpoint é útil para inicializar o sistema com os papéis necessários para o funcionamento adequado da aplicação.
        /// </summary>
        /// <returns>Retorna um status 200 OK se a criação for bem-sucedida, caso contrário retorna um status 500 Internal Server Error.</returns>
        [Obsolete("Este endpoint está privado, servindo apenas para uso interno")]
        [SwaggerOperation(Summary = "Cria os papéis padrões no banco de dados, caso eles não existam.", Description = "Este endpoint é útil para inicializar o sistema com os papéis necessários para o funcionamento adequado da aplicação. Este endpoint está privado, servindo apenas para uso interno.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Papéis padrões criados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Falha ao criar os papéis padrões.")]
        [ApiExplorerSettings(IgnoreApi = true)]
      
        [HttpPost("create-pattern-roles")]
        public async Task<IActionResult> CreatePatternRoles()
        {
            try
            {
                bool success = await _service.CreatePatternRoles();
                return success ? Ok(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError, "Falha ao criar os papéis padrões.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao criar os papéis padrões: {ex.Message}");
            }
        }
    }
}