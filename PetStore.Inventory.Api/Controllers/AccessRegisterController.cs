using Microsoft.AspNetCore.Mvc;
using PetStore.Inventory.Api.ApplicationDTOs.Requests;
using PetStore.Inventory.Application.BusinessDTOs.Results;
using PetStore.Inventory.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

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


        /// <summary>
        /// Endpoint para cadastrar um novo usuário. Recebe os dados do usuário no formato UserFirstRegisterDTO e retorna uma resposta indicando o sucesso ou falha do cadastro.
        /// </summary>
        /// <param name="data">Parâmetro que representa os dados do usuário a ser cadastrado.</param>
        /// <returns>Retorna um status 200 OK se o cadastro for bem-sucedido, caso contrário retorna um status 400 Bad Request ou 500 Internal Server Error.</returns>
        [SwaggerOperation(Summary = "Endpoint para cadastrar um novo usuário", Description = "Recebe os dados do usuário no formato UserFirstRegisterDTO e retorna uma resposta indicando o sucesso ou falha do cadastro.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cadastrado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados do usuário inválidos.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]    
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

        /// <summary>
        /// Endpoint para realizar o login de um usuário. Recebe os dados de login no formato LoginDTO e retorna um DataRegisterResult contendo as informações do usuário logado, ou uma mensagem de erro caso o login falhe.
        /// </summary>
        /// <param name="data">Parâmetro que representa os dados de login do usuário.</param>
        /// <returns>Retorna as informações do usuário logado ou uma mensagem de erro.</returns>
        [SwaggerOperation(Summary = "Endpoint para realizar o login de um usuário", Description = "Recebe os dados de login no formato LoginDTO e retorna um DataRegisterResult contendo as informações do usuário logado, ou uma mensagem de erro caso o login falhe.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Login realizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
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

        /// <summary>
        /// Endpoint para realizar o logoff de um usuário. Recebe o ID do usuário e retorna uma resposta indicando o sucesso ou falha do logoff.
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Resposta indicando o sucesso ou falha do logoff.</returns>
        [SwaggerOperation(Summary = "Endpoint para realizar o logoff de um usuário", Description = "Recebe o ID do usuário e retorna uma resposta indicando o sucesso ou falha do logoff.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Logoff realizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
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

        /// <summary>
        /// Endpoint para deletar a conta de um usuário. Recebe o ID do usuário e retorna uma resposta indicando o sucesso ou falha da operação.
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <returns>Resposta indicando o sucesso ou falha da operação.</returns>
        [SwaggerOperation(Summary = "Endpoint para deletar a conta de um usuário", Description = "Recebe o ID do usuário e retorna uma resposta indicando o sucesso ou falha da operação.")]
        [SwaggerResponse(StatusCodes.Status200OK,"Usuário removido com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
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