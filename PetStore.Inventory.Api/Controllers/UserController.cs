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
    public class UserController : ControllerBase
    {
        private readonly IUserServices _services;

        public UserController(IUserServices services)
        {
            _services = services;
        }

        /// <summary>
        /// Endpoint que retorna uma lista de todos os usuários registrados no sistema. Este endpoint é útil para obter uma visão geral de todos os usuários existentes, permitindo que os clientes recuperem informações sobre cada usuário, como nome, email, papel e outros detalhes relevantes. O método não espera nenhum parâmetro e retorna uma lista de objetos UserRegisterModel que representam os usuários registrados. Se a recuperação for bem-sucedida, retorna um status 200 OK com a lista de usuários. Caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint, garantindo que apenas usuários com permissões adequadas possam criar novos usuários no sistema.
        /// </summary>
        /// <returns>Retorna uma lista de todos os usuários registrados.</returns>
        [SwaggerOperation(Summary = "Recupera todos os usuários.", Description = "Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Usuários recuperados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum usuário encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autorizado.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Acesso negado.")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                IEnumerable<UserRegisterModel> users = await _services.GetAllUsers();
                if (users == null || !users.Any())
                    return NotFound("Nenhum usuário encontrado.");

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao recuperar os usuários: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que recebe um ID de papel e retorna uma lista de usuários que correspondem a esse ID de papel. Este endpoint é útil para filtrar os usuários com base em seu papel, permitindo que os clientes recuperem informações específicas sobre os usuários relacionados a um determinado papel. O método espera um parâmetro roleId do tipo inteiro e retorna uma lista de objetos UserRegisterModel que correspondem ao ID de papel fornecido. Se a recuperação for bem-sucedida, retorna um status 200 OK com a lista de usuários. Caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada.Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint, garantindo que apenas usuários com permissões adequadas possam criar novos usuários no sistema.
        /// </summary>
        /// <param name="roleId">O ID do papel a ser filtrado.</param>
        /// <returns>Retorna uma lista de usuários que correspondem ao ID de papel fornecido.</returns>
        [SwaggerOperation(Summary = "Recupera usuários filtrados por ID de papel.", Description = "Recupera uma lista de usuários que correspondem ao ID de papel fornecido. Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Usuários recuperados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum usuário encontrado com o ID de papel fornecido.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autorizado.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Acesso negado.")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]
        [HttpGet("get-all-filtered-by-role-id")]
        public async Task<IActionResult> GetUsersFilteredByRoleId(int roleId)
        {
            try
            {
                IEnumerable<UserRegisterModel> users = await _services.GetUsersFilteredByRoleId(roleId);

                if (users == null || !users.Any())
                {
                    return NotFound($"Nenhum usuário encontrado com o ID de papel {roleId}.");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao recuperar os usuários: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que recebe uma string de filtros e retorna uma lista de usuários que correspondem a esses filtros. Este endpoint é útil para filtrar os usuários com base em critérios personalizados, permitindo que os clientes recuperem informações específicas sobre os usuários relacionados a esses filtros. O método espera um parâmetro filters do tipo string, que pode conter diversos critérios de filtragem, e retorna uma lista de objetos UserRegisterModel que correspondem aos filtros fornecidos. Se a recuperação for bem-sucedida, retorna um status 200 OK com a lista de usuários. Caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada. Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint, garantindo que apenas usuários com permissões adequadas possam criar novos usuários no sistema.
        /// </summary>
        /// <param name="filters">A string de filtros.</param>
        /// <returns>Retorna uma lista de usuários que correspondem aos filtros fornecidos.</returns>
        [SwaggerOperation(Summary = "Recupera usuários filtrados por uma string de filtros.", Description = "Recupera uma lista de usuários que correspondem a uma string de filtros personalizada. Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Usuários recuperados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum usuário encontrado com os filtros aplicados.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autorizado.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Acesso negado.")]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpGet("get-all-filtered-by-string")]
        public async Task<IActionResult> GetUsersFilteredByString(string filters)
        {
            try
            {
                IEnumerable<UserRegisterModel> users = await _services.GetUsersFilteredByString(filters);

                if (users == null || !users.Any())
                {
                    return NotFound($"Nenhum usuário encontrado com os filtros aplicados.");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao recuperar os usuários: {ex.Message}");
            }
        }

        /// <summary>
        /// Endpoint que recebe um ID de usuário e retorna uma lista de usuários que correspondem a esse ID. Este endpoint é útil para filtrar os usuários com base em seu ID, permitindo que os clientes recuperem informações específicas sobre um usuário ou um conjunto de usuários relacionados a esse ID. O método espera um parâmetro userId do tipo inteiro e retorna uma lista de objetos UserRegisterModel que correspondem ao ID fornecido. Se a recuperação for bem-sucedida, retorna um status 200 OK com a lista de usuários. Caso contrário, retorna um status 500 Internal Server Error com uma mensagem de erro detalhada. Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint, garantindo que apenas usuários com permissões adequadas possam criar novos usuários no sistema.
        /// </summary>
        /// <param name="userId">O ID do usuário a ser filtrado.</param>
        /// <returns>Retorna uma lista de usuários que correspondem ao ID fornecido.</returns>
        [SwaggerOperation(Summary = "Recupera usuários filtrados por ID.", Description = "Recupera uma lista de usuários que correspondem ao ID fornecido. Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Usuários recuperados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum usuário encontrado com o ID fornecido.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autorizado.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Acesso negado.")]

        [ApiExplorerSettings(IgnoreApi = true)]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpGet("get-all-filtered-by-user-id")]
        public async Task<IActionResult> GetUsersFilteredById(int userId)
        {
            try
            {
                UserRegisterModel user = await _services.GetUsersFilteredById(userId);

                if (user == null)
                {
                    return NotFound($"Usuário não encontrado com o ID {userId}.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao recuperar os usuário: {ex.Message}");
            }
        }



        /// <summary>
        /// Endpoint que recebe os dados de um usuário e tenta criar um novo usuário no sistema. O método espera um objeto do tipo UserRegisterRequest contendo as informações necessárias para a criação do usuário, como nome, email, senha e papel. Se a criação for bem-sucedida, retorna um status 200 OK com uma mensagem de sucesso. Caso contrário, retorna um status 400 Bad Request ou 500 Internal Server Error dependendo do tipo de falha ocorrida. Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint, garantindo que apenas usuários com permissões adequadas possam criar novos usuários no sistema.
        /// </summary>
        /// <param name="userRequest">Parâmetro que representa os dados do usuário a ser criado.</param>
        /// <returns>Retorna um status 200 OK se a criação for bem-sucedida, caso contrário retorna um status 400 Bad Request ou 500 Internal Server Error.</returns>
        [SwaggerOperation(Summary = "Cria um novo usuário no sistema.", Description = "Recebe os dados de um usuário e tenta criar um novo usuário no sistema. O método espera um objeto do tipo UserRegisterRequest contendo as informações necessárias para a criação do usuário, como nome, email, senha e papel. Se a criação for bem-sucedida, retorna um status 200 OK com uma mensagem de sucesso. Caso contrário, retorna um status 400 Bad Request ou 500 Internal Server Error dependendo do tipo de falha ocorrida. Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint, garantindo que apenas usuários com permissões adequadas possam criar novos usuários no sistema.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Usuário criado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados do usuário inválidos.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autorizado.")]

        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserFirstRegisterDTO userRequest)
        {
            try
            {

                UserRegisterModel result = await _services.CreateUser(userRequest.ToBusinessRequest());
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Não foi possível criar o usuário.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao criar o usuário: {ex.Message}");
            }
        }


        /// <summary>
        /// Endpoint que cria os usuários padrões no banco de dados, caso eles não existam. Este endpoint é útil para inicializar o sistema com os papéis necessários para o funcionamento adequado da aplicação. Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint, garantindo que apenas usuários com permissões adequadas possam criar novos usuários no sistema.
        /// </summary>
        /// <returns>Retorna um status 200 OK se a criação for bem-sucedida, caso contrário retorna um status 500 Internal Server Error.</returns>
        [Obsolete("Este endpoint está privado, servindo apenas para uso interno")]
        [SwaggerOperation(Summary = "Cria os usuários padrões no banco de dados, caso eles não existam.", Description = "Este endpoint é útil para inicializar o sistema com os usuários necessários para o funcionamento adequado da aplicação. Este endpoint está privado, servindo apenas para uso interno. Apenas usuários com os papéis de ADMIN ou SYSTEM_OPERATOR estão autorizados a acessar este endpoint.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Usuários padrões criados com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Falha ao criar os usuários padrões.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autorizado.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Acesso negado.")]

        [ApiExplorerSettings(IgnoreApi = true)]

        [Authorize(Roles = nameof(EUserRoles.ADMIN) + "," + nameof(EUserRoles.SYSTEM_OPERATOR))]

        [HttpPost("create-pattern-users")]
        public async Task<IActionResult> CreatePatternUsers()
        {
            try
            {
                bool result = await _services.CreatePatternUsers();
                if (result)
                {
                    return Ok("Usuários padrão criados com sucesso.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Não foi possível criar os usuários padrão.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao criar os usuários padrão: {ex.Message}");
            }
        }
    }
}